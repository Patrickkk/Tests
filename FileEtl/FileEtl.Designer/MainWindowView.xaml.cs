using System.Windows;

namespace FileEtl.Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            PreviewWindow a = new PreviewWindow();
            //a.Owner = this;
            a.Show();
        }
    }
}