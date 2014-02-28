using System.Windows;

namespace Primer.SampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var win = new Window1();
            var vm = new SampleCustomerViewModel();
            win.DataContext = vm;
            win.Show();

        }
    }
}
