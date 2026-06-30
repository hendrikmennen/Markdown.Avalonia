using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorDocument.Avalonia.DocumentElements
{
    /// <summary>
    /// The top document element.
    /// </summary>
    public class DocumentRootElement : DocumentElement
    {
        private Lazy<Control> _block;
        private EnumerableEx<DocumentElement> _children;
        private SelectionList? _prevSelection;
        private bool _virtualize;

        public override Control Control => _block.Value;
        public override IEnumerable<DocumentElement> Children => _children;

        /// <summary>
        /// When set, the document blocks are hosted in a virtualizing panel so
        /// only the visible blocks are laid out. Must be set before <see cref="Control"/>
        /// is first accessed; otherwise it has no effect.
        /// </summary>
        public bool Virtualize
        {
            get => _virtualize;
            set
            {
                if (_block.IsValueCreated)
                    throw new InvalidOperationException(
                        $"{nameof(Virtualize)} must be set before {nameof(Control)} is accessed.");
                _virtualize = value;
            }
        }

        public DocumentRootElement(IEnumerable<DocumentElement> child)
            : this(child, false)
        {
        }

        public DocumentRootElement(IEnumerable<DocumentElement> child, bool virtualize)
        {
            _virtualize = virtualize;
            _block = new Lazy<Control>(Create);
            _children = child.ToEnumerable();
        }

        private Control Create()
        {
            if (_virtualize)
                return CreateVirtualized();

            var panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            foreach (var child in _children)
                panel.Children.Add(child.Control);

            return panel;
        }

        private Control CreateVirtualized()
        {
            // Only the realized (visible) block controls are measured/arranged,
            // which keeps layout cheap during scrolling and window resizing for
            // very large documents. Note: text selection and header tracking can
            // only see realized blocks while virtualization is enabled.
            //
            // Each DocumentElement is mapped to its own (stable, lazily-built)
            // Control via the item template. Recycling is disabled so a realized
            // container is never reused for a different element.
            var items = new ItemsControl
            {
                ItemsPanel = new FuncTemplate<Panel?>(() => new VirtualizingStackPanel()),
                ItemsSource = _children.ToList(),
                ItemTemplate = new FuncDataTemplate<DocumentElement>(
                    (element, _) => element?.Control,
                    supportsRecycling: false),
            };

            return items;
        }

        public override void Select(Point from, Point to)
        {
            var selection = SelectionUtil.SelectVertical(Control, _children, from, to);

            if (_prevSelection is not null)
            {
                foreach (var ps in _prevSelection)
                {
                    if (!selection.Any(cs => ReferenceEquals(cs, ps)))
                    {
                        ps.UnSelect();
                    }
                }
            }

            _prevSelection = selection;
        }

        public override void UnSelect()
        {
            foreach (var child in _children)
                child.UnSelect();
        }

        public override void ConstructSelectedText(StringBuilder builder)
        {
            if (_prevSelection is null)
                return;

            var preLen = builder.Length;

            foreach (var para in _prevSelection)
            {
                para.ConstructSelectedText(builder);

                if (preLen == builder.Length)
                    continue;

                if (builder[builder.Length - 1] != '\n')
                    builder.Append('\n');
            }
        }
    }
}
