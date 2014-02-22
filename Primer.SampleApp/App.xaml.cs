using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Primer.Windows;

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

            //IWindowingService svc = new WindowingService();
            //svc.Initialize();
            //svc.ShowWindow("MyWindow", new SampleViewModel(), null);

            var win = new Window1();
            var vm = new SampleCustomerViewModel();
            win.DataContext = vm;
            win.Show();

        }
    }
}
