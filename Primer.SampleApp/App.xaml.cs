using System;
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
            var ctx = new DataContext();

            ctx.Customers.Add(new Customer() { ID = 1876309338, FirstName = "Rober", FamilyName = "Mugabe", StartDate = DateTime.Today.AddYears(-4), TestProperty = "This is a test property" });

            ctx.Details.Add(new OrderDetail() { ID = 2345, Description = "Hammer", Quantity = 1, Value = 12.50M });
            ctx.Details.Add(new OrderDetail() { ID = 9276, Description = "Saw", Quantity = 1, Value = 25.20M });
            ctx.Details.Add(new OrderDetail() { ID = 1754, Description = "Screwdriver", Quantity = 1, Value = 4.50M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "Box of Nails", Quantity = 1, Value = 2.50M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "Box of Screws", Quantity = 1, Value = 5.0M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "8x2\" MDF", Quantity = 5, Value = 246.00M });

            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "Wicks", Branch = "Longwell Green" });
            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "B&Q", Branch = "Longwell Green" });
            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "Homebase", Branch = "Longwell Green" });

            try
            {

                var vm = new SampleCustomerViewModel(ctx);

                win.DataContext = vm;
                win.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
          

        }
    }
}
