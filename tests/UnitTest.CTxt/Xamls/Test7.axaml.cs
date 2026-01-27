using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnitTest.CTxt.Xamls
{
<<<<<<<< HEAD:demos/Markdown.AvaloniaDemo/Views/MainWindow.axaml.cs
    public partial class MainWindow : Window
========
    public partial class Test7 : UserControl
>>>>>>>> refs/remotes/upstream/master:tests/UnitTest.CTxt/Xamls/Test7.axaml.cs
    {
        public Test7()
        {
<<<<<<<< HEAD:demos/Markdown.AvaloniaDemo/Views/MainWindow.axaml.cs
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
# endif
========
            this.InitializeComponent();
>>>>>>>> refs/remotes/upstream/master:tests/UnitTest.CTxt/Xamls/Test7.axaml.cs
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
