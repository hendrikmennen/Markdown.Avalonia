using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering;
using Avalonia.Threading;
using Avalonia.VisualTree;
using ColorTextBlock.Avalonia;
using Markdown.Avalonia;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using UnitTest.Base;
using UnitTest.Base.Apps;
using UnitTest.Base.Utils;
using UnitTest.CTxt.Utils;
using UnitTest.CTxt.Xamls;

namespace UnitTest.CTxt
{
    //[UseReporter(typeof(DiffReporter))]
    public class UnitTestCTxt : UnitTestBase
    {
        public UnitTestCTxt()
        {
            Approvals.RegisterDefaultApprover((w, n, c) => new ImageFileApprover(w, n, c));
        }

        [Test]
        [RunOnUI]
        public void GivenTest1_generatesExpectedResult()
        {
            var tst1 = new Test1();
            var ctxt = (CTextBlock)tst1.Content;

            var info = new MetryHolder(ctxt, 360, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest2_generatesExpectedResult()
        {
            var tst2 = new Test2();
            var ctxt = (CTextBlock)tst2.Content;

            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest3_generatesExpectedResult_sub0()
        {
            var tst3 = new Test3();
            var spnl = (StackPanel)tst3.Content;

            var ctxt = (CTextBlock)spnl.Children[0];
            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest3_generatesExpectedResult_sub1()
        {
            var tst3 = new Test3();
            var spnl = (StackPanel)tst3.Content;

            var ctxt = (CTextBlock)spnl.Children[1];
            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest3_generatesExpectedResult_sub2()
        {
            var tst3 = new Test3();
            var spnl = (StackPanel)tst3.Content;

            var ctxt = (CTextBlock)spnl.Children[2];
            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest3_generatesExpectedResult_sub3()
        {
            var tst3 = new Test3();
            var spnl = (StackPanel)tst3.Content;

            var ctxt = (CTextBlock)spnl.Children[3];
            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest_drawableSomeMds()
        {
            foreach (var mdname in Util.GetTextNames().Where(nm => nm.EndsWith(".md")))
            {
                var text = Util.LoadText(mdname);
                var markdown = new Markdown.Avalonia.Markdown();
                var control = markdown.Transform(text);

                var theme = new Avalonia.Themes.Simple.SimpleTheme();
                control.Styles.Add(theme);

                control.Styles.Add(MarkdownStyle.SimpleTheme);
                control.Resources.Add("FontSizeNormal", 16d);

                var umefont = new FontFamily(new Uri("avares://UnitTest.CTxt/Assets/Fonts/ume-ugo4.ttf"), "Ume UI Gothic");
                TextElement.SetFontFamily(control, umefont);

                var info = new MetryHolder(control, 500, 10000);
            }
        }

        /*
         * On Github Action, this test don't pass.
         * But on my environment, this test pass.
         * Because of environment dependent, I erase this test case.
         */
        //[Test]
        [RunOnUI]
        public void GivenTestXXX_generatesExpectedResult()
        {
            var text = Util.LoadText("MainWindow.md");

            var markdown = new Markdown.Avalonia.Markdown();
            var control = markdown.Transform(text);

            var theme = new Avalonia.Themes.Simple.SimpleTheme();
            control.Styles.Add(theme);

            control.Styles.Add(MarkdownStyle.SimpleTheme);
            control.Resources.Add("FontSizeNormal", 16d);

            var umefont = new FontFamily(new Uri("avares://UnitTest.CTxt/Assets/Fonts/ume-ugo4.ttf"), "Ume UI Gothic");
            TextElement.SetFontFamily(control, umefont);

            var info = new MetryHolder(control, 500, 10000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest4_generatesExpectedResult()
        {
            var tst4 = new Test4();
            var ctxt = (CTextBlock)tst4.Content;

            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest5_generatesExpectedResult()
        {
            var tst5 = new Test5();
            var ctxt = (CTextBlock)tst5.Content;

            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest6_generatesExpectedResult()
        {
            var tst6 = new Test6();
            var ctxt = (CTextBlock)tst6.Content;

            var info = new MetryHolder(ctxt, 1000, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [RunOnUI]
        public void GivenTest7_generatesExpectedResult()
        {
            var tst6 = new Test7();
            var ctxt = (StackPanel)tst6.Content;

            var info = new MetryHolder(ctxt, 480, 1000);

            Approvals.Verify(
                new ApprovalImageWriter(info.Image),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        [Test]
        [TestCase(500)]
        [TestCase(510)]
        [TestCase(520)]
        [TestCase(530)]
        [TestCase(540)]
        [TestCase(550)]
        [TestCase(560)]
        [TestCase(570)]
        [TestCase(580)]
        [TestCase(590)]
        [TestCase(600)]
        [TestCase(610)]
        [TestCase(620)]
        [TestCase(630)]
        [TestCase(640)]
        [TestCase(650)]
        [TestCase(660)]
        [TestCase(670)]
        [TestCase(680)]
        [TestCase(690)]
        [TestCase(700)]
        [TestCase(710)]
        [TestCase(720)]
        [TestCase(730)]
        [TestCase(740)]
        [TestCase(750)]
        [TestCase(760)]
        [TestCase(770)]
        [TestCase(780)]
        [TestCase(790)]
        [TestCase(800)]
        [TestCase(810)]
        [TestCase(820)]
        [TestCase(830)]
        [TestCase(840)]
        [TestCase(850)]
        [TestCase(860)]
        [TestCase(870)]
        [TestCase(880)]
        [TestCase(890)]
        [TestCase(900)]
        [TestCase(910)]
        [TestCase(920)]
        [TestCase(930)]
        [TestCase(940)]
        [TestCase(950)]
        [TestCase(960)]
        [TestCase(970)]
        [TestCase(980)]
        [TestCase(990)]
        [TestCase(1000)]
        [RunOnUI]
        public void GivenTest99_generatesExpectedResult(int width)
        {
            var tst99 = new Test99();
            var ctxt = (CTextBlock)tst99.Content;

            var info = new MetryHolder(ctxt, width, 1000);

            Approvals.Verify(
                new ApprovalImageWriter("99", info.Image, width.ToString()),
                Approvals.GetDefaultNamer(),
                new DiffToolReporter(DiffEngine.DiffTool.WinMerge));
        }

        #region virtualization

        private static string BigMarkdown(int paragraphs)
            => string.Join(
                "\n\n",
                Enumerable.Range(0, paragraphs)
                          .Select(i => $"Paragraph number {i} with some words to wrap and fill width."));

        private string RichMarkdown(int repeats)
        {
            var path = System.IO.Path.Combine(AssetPath, "Texts", "MainWindow.md");
            var doc = System.IO.File.ReadAllText(path);
            return string.Join("\n\n", Enumerable.Repeat(doc, repeats));
        }

        private static int CountRealizedTextBlocks(Control root)
            => root.GetVisualDescendants().OfType<CTextBlock>().Count();

        private static void RunVirtualizationLayout(Window win)
        {
            for (int i = 0; i < 6; ++i)
            {
                win.Measure(new Size(400, 300));
                win.Arrange(new Rect(0, 0, 400, 300));
                Dispatcher.UIThread.RunJobs();
            }
        }

        [Test]
        [RunOnUI]
        public void Virtualization_realizes_only_visible_blocks()
        {
            var viewer = new MarkdownScrollViewer
            {
                EnableVirtualization = true,
                Markdown = BigMarkdown(2000)
            };

            var win = new Window { Width = 400, Height = 300, Content = viewer };
            win.Show();
            RunVirtualizationLayout(win);

            int realized = CountRealizedTextBlocks(viewer);
            TestContext.Progress.WriteLine($"Virtualized realized CTextBlocks = {realized} (of 2000)");

            Assert.That(realized, Is.GreaterThan(0), "Some blocks should be realized.");
            Assert.That(realized, Is.LessThan(200), "Virtualization should realize only a small subset.");

            win.Close();
        }

        [Test]
        [RunOnUI]
        public void Virtualization_rich_content_scroll_render_does_not_crash()
        {
            // Regression test for the silent crash (InvalidCastException Color->IBrush)
            // that occurred while scrolling a virtualized document up and down when
            // block controls were hosted via a DataTemplate. The blocks must be used
            // as their own item containers instead.
            var viewer = new MarkdownScrollViewer
            {
                EnableVirtualization = true,
                SelectionEnabled = true,
                Markdown = RichMarkdown(60)
            };

            var win = new Window { Width = 500, Height = 400, Content = viewer };
            win.Show();
            RunVirtualizationLayout(win);

            var inner = viewer.GetVisualDescendants().OfType<ScrollViewer>().First();

            void RenderOnce()
            {
                var rtb = new RenderTargetBitmap(new PixelSize(500, 400), new Vector(96, 96));
                win.Measure(new Size(500, 400));
                win.Arrange(new Rect(0, 0, 500, 400));
                rtb.Render(viewer);
                rtb.Dispose();
            }

            // Scroll all the way down and back up, rendering at each step. This
            // repeatedly un-realizes and re-realizes block controls.
            for (double y = 0; y < inner.Extent.Height; y += 350)
            {
                inner.Offset = new Vector(0, y);
                RunVirtualizationLayout(win);
                RenderOnce();
            }
            for (double y = inner.Extent.Height; y >= 0; y -= 350)
            {
                inner.Offset = new Vector(0, y);
                RunVirtualizationLayout(win);
                RenderOnce();
            }

            win.Close();
        }

        [Test]
        [RunOnUI]
        public void Virtualization_scroll_re_realizes_blocks()
        {
            var viewer = new MarkdownScrollViewer
            {
                EnableVirtualization = true,
                Markdown = BigMarkdown(2000)
            };

            var win = new Window { Width = 400, Height = 300, Content = viewer };
            win.Show();
            RunVirtualizationLayout(win);

            var inner = viewer.GetVisualDescendants().OfType<ScrollViewer>().First();

            string FirstRealizedText() =>
                viewer.GetVisualDescendants().OfType<CTextBlock>()
                      .Select(t => t.Text).FirstOrDefault(t => t is not null) ?? "";

            var top = FirstRealizedText();
            inner.Offset = new Vector(0, 10000);
            RunVirtualizationLayout(win);
            var scrolled = FirstRealizedText();

            Assert.That(scrolled, Is.Not.EqualTo(top), "Scrolling should realize a different set of blocks.");

            // Scroll back to the top and ensure previously seen blocks re-realize cleanly.
            Assert.DoesNotThrow(() =>
            {
                inner.Offset = new Vector(0, 0);
                RunVirtualizationLayout(win);
            });

            win.Close();
        }

        [Test]
        [RunOnUI]
        public void NonVirtualized_realizes_all_blocks()
        {
            var viewer = new MarkdownScrollViewer
            {
                EnableVirtualization = false,
                Markdown = BigMarkdown(2000)
            };

            var win = new Window { Width = 400, Height = 300, Content = viewer };
            win.Show();
            RunVirtualizationLayout(win);

            int realized = CountRealizedTextBlocks(viewer);
            TestContext.Progress.WriteLine($"Non-virtualized realized CTextBlocks = {realized} (of 2000)");

            Assert.That(realized, Is.EqualTo(2000), "Without virtualization, all blocks are realized.");

            win.Close();
        }

        [Test]
        [RunOnUI]
        public void Selection_does_not_throw_when_virtualized()
        {
            var engine = new Markdown.Avalonia.Markdown();
            var doc = (ColorDocument.Avalonia.DocumentElements.DocumentRootElement)
                engine.TransformElement(BigMarkdown(2000));
            doc.Virtualize = true;

            var win = new Window
            {
                Width = 400,
                Height = 300,
                Content = new ScrollViewer { Content = doc.Control }
            };
            win.Show();
            RunVirtualizationLayout(win);

            // Drag-select across the whole (mostly virtualized) document.
            Assert.DoesNotThrow(() =>
            {
                doc.Select(new Point(0, 0), new Point(400, 100000));
                var _ = doc.GetSelectedText();
            });

            win.Close();
        }

        #endregion
    }

    class MetryHolder : AvaloniaObject
    {
        private static readonly Vector Dpi = new(250, 250);

        public Bitmap Image { get; set; }

        //public MetryHolder(CTextBlock ctxt, int width = 400, int height = 1000)
        //{
        //    var reqSz = new Size(width, height);
        //
        //    ctxt.Measure(reqSz);
        //    ctxt.Arrange(new Rect(0, 0, width, ctxt.DesiredSize.Height == 0 ? height : ctxt.DesiredSize.Height));
        //    ctxt.Measure(reqSz);
        //
        //    var newReqSz = new Size(
        //        ctxt.DesiredSize.Width == 0 ? reqSz.Width : ctxt.DesiredSize.Width,
        //        ctxt.DesiredSize.Height == 0 ? reqSz.Height : ctxt.DesiredSize.Height);
        //    ctxt.Arrange(new Rect(0, 0, newReqSz.Width, newReqSz.Height));
        //
        //    var bitmap = new RenderTargetBitmap(PixelSize.FromSizeWithDpi(newReqSz, Dpi), Dpi);
        //
        //    using (var icontext = bitmap.CreateDrawingContext(null))
        //    using (var context = new DrawingContext(icontext))
        //    {
        //        ctxt.Render(context);
        //    }
        //
        //    Image = bitmap;
        //}

        public MetryHolder(Control ctxt, int width = 400, int height = 1000)
        {
            var reqSz = new Size(width, height);
            ctxt.Measure(reqSz);
            ctxt.Arrange(new Rect(0, 0, width, ctxt.DesiredSize.Height == 0 ? height : ctxt.DesiredSize.Height));
            ctxt.Measure(reqSz);

            var newReqSz = new Size(
                ctxt.DesiredSize.Width == 0 ? reqSz.Width : ctxt.DesiredSize.Width,
                ctxt.DesiredSize.Height == 0 ? reqSz.Height : ctxt.DesiredSize.Height);
            ctxt.Arrange(new Rect(0, 0, newReqSz.Width, newReqSz.Height));

            var bitmap = new RenderTargetBitmap(PixelSize.FromSizeWithDpi(newReqSz, Dpi), Dpi);

            using (var context = bitmap.CreateDrawingContext())
            {
                RenderHelper(ctxt, context);
            }
            Image = bitmap;
        }

        private void RenderHelper(Visual vis, DrawingContext ctx)
        {
            var sz = new Rect(vis.Bounds.Size);
            var bnd = vis.Bounds;

            using (ctx.PushTransform(Matrix.CreateTranslation(vis.Bounds.Position)))
            //using (ctx.PushOpacity(vis.Opacity))
            using (vis.OpacityMask != null ? ctx.PushOpacityMask(vis.OpacityMask, sz) : default)
            {
                vis.Render(ctx);

                var childrenProp = typeof(Visual).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                                            .Where(fld => fld.Name == "VisualChildren")
                                            .First();

                var children = (IAvaloniaList<Visual>)childrenProp.GetValue(vis);
                foreach (var child in children)
                    RenderHelper(child, ctx);
            }
        }
    }
}