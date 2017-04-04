using System.Windows;

namespace FileEtl.Designer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PreviewWindow a = new PreviewWindow();
            //a.Owner = this;
            a.Show();
        }
    }
}