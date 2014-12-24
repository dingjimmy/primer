using System.Windows;

namespace Primer.SampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new HomeViewModel(new MessagingChannel(), null, null);   
            this.Content = vm;
        }
    }
}
