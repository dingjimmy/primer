using System;
using System.Windows;
using System.Linq;

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

            SetupDemoData(ctx);

            try
            {


                // Build queries
                var cusQuery = from c in ctx.Customers where c.ID == 1876309338 select c;
                var dtlQuery = from d in ctx.Details select d;
                var splQuery = from s in ctx.Suppliers select s;


                // Create ViewModel 
                var vm = new SampleCustomerViewModel(ctx, null); 


                // Load it with data
                vm.Load(cusQuery, dtlQuery, splQuery);


                // Show Window
                win.DataContext = vm;
                win.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                Shutdown();
            }
          

        }


        void SetupDemoData(DataContext ctx)
        {

            ctx.Customers.Add(new Customer() { ID = 1876309338, FirstName = "Robert", FamilyName = "Mugabe", StartDate = DateTime.Today.AddYears(-4), Description = String.Empty, TestProperty = "This is a test property" });

            ctx.Details.Add(new OrderDetail() { ID = 2345, Description = "Hammer", Quantity = 1, Value = 12.50M });
            ctx.Details.Add(new OrderDetail() { ID = 9276, Description = "Saw", Quantity = 1, Value = 25.20M });
            ctx.Details.Add(new OrderDetail() { ID = 1754, Description = "Screwdriver", Quantity = 1, Value = 4.50M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "Box of Nails", Quantity = 1, Value = 2.50M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "Box of Screws", Quantity = 1, Value = 5.0M });
            ctx.Details.Add(new OrderDetail() { ID = 2985, Description = "8x2\" MDF", Quantity = 5, Value = 246.00M });

            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "Wicks", Branch = "Longwell Green" });
            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "B&Q", Branch = "Longwell Green" });
            ctx.Suppliers.Add(new Supplier() { ID = 123456, Name = "Homebase", Branch = "Longwell Green" });

        }

    }
}
