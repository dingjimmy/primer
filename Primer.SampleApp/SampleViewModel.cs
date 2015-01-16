using Primer;
using System;
using System.Windows.Input;
using System.Linq;
using Primer.Messages;

namespace Primer.SampleApp
{

    public class SampleCustomerViewModel : ViewModel<CustomerFacade>
    {

        // Dependancies
        DataContext _Context;


        // Sub-ViewModel collections
        public ViewModelCollection<DetailViewModel> Details { get; set; }
        public ViewModelCollection MoreDetails { get; set; }



        // Lookups
        public Lookup<Supplier> AvailableSuppliers { get; set; }



        // Commands
        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }





        public SampleCustomerViewModel(DataContext ctx)
        {

            // Set dependancies
            _Context = ctx;
            Channel = new MessagingChannel();
            //Validator = new CustomerValidator();


            // build queries
            var cusQuery = from c in _Context.Customers where c.ID == 1876309338 select c;
            var dtlQuery = from d in _Context.Details select d;
            var splQuery = from s in _Context.Suppliers select s;         


            Initialise(cusQuery, dtlQuery, splQuery);

        }


        protected override void Initialise(ViewModelInitialiser initialise, params object[] dataSources)
        {

            // Verify dependacies
            var customers = dataSources[0] as IQueryable<Customer>;
            var details = dataSources[1] as IQueryable<OrderDetail>;
            var suppliers = dataSources[2] as IQueryable<Supplier>;


            // Set the model
            Model = new CustomerFacade(customers.First(), this.Channel, new CustomerValidator());

           


            // Init a collection of ViewModels using a specific initialisation method.
            //Details = initialise.Collection<DetailViewModel, OrderDetail>(details, (init, item, vm) =>
            //    {
            //        vm.Model = new OrderDetailFacade(item, this.Channel);
            //    });

            Details = initialise.Collection<DetailViewModel, OrderDetail>(details, InitDetails, new { Data1 = "Foo", Data2 = "Bar" });



            // Init collection of ViewModels using the ViewModel's default initialisation method.
            MoreDetails = initialise.Collection<DetailViewModel, OrderDetail>(details);

            

            // Init Lookups
            AvailableSuppliers = initialise.Lookup<Supplier>(suppliers, (supplier, item) =>
                {
                    item.Key = supplier.ID.ToString();
                    item.Description = String.Format("{0} - {1}", supplier.Name, supplier.Branch);
                    item.Entity = supplier;
                });



            // Init Commands
            this.OkCommand = new Command { Action = SaveThis, IsEnabled = true };
            this.CancelCommand = new Command { Action = CancelThis, IsEnabled = true };



            // Listen for FieldChanged messages
            Listen<PropertyChanged>(m =>
                {
                    System.Diagnostics.Debug.WriteLine("A 'PropertyChanged' message was broadcast by '{0}' at '{1}'. Property Name: {2}", m.Sender.GetType().ToString(), m.BroadcastOn, m.Name);
                });


            // Change a fields value; this will broadcast the PropertyChanged message which should cause the above listener method to be executed!
            Model.SetFirstName("Dicky", true, false);
            Model.SetFamilyName("Wee Wee", false, true);
            Details[0].Model.Description = "This field has changed!!!";


            //var nameOne = GetMethodName(() => this.TestSubRoutine());
            //var nameTwo = GetMethodName(() => this.TestSubRoutineWithParameter(true));

            //var nameThree = GetMethodName(() => this.TestFunction());
            //var nameFour = GetMethodName(() => this.TestFunctionWithParameter(true));
            //var nameFive = GetMethodName(() => this.TestFunctionWithTwoParameters(true, true));


            //HandleException(() => this.TestSubRoutine(), new ApplicationException("Test SubRoutine"));
            //HandleException(() => this.TestFunction(), new ApplicationException("Test Function"));

        }



        #region CommandMethods


        public void SaveThis()
        {


            Validate("ID", "FirstName", "FamilyName", "TestProperty", "StartDate");

            if (!InError("ID", "FirstName", "FamilyName", "TestProperty", "StartDate"))
            {
                // save me to a database!
            }
            
            

            //if (!string.IsNullOrWhiteSpace(this.Model.Error))
            //{
            //    System.Windows.MessageBox.Show(this.Model.Error);
            //}


            if (this.Model.Errors.Count > 0)
            {
                var builder = new System.Text.StringBuilder();

                foreach (var err in this.Model.Errors)
                {
                    builder.AppendFormat("{0}\n", err);
                }

                System.Windows.MessageBox.Show(builder.ToString());
            }

            //Model.FirstName = "This is a test!";
        }



        public void CancelThis()
        {
            
        }


        #endregion


        public void DoSomethingSpecial()
        {
            AvailableSuppliers.ClearFilter();
            AvailableSuppliers.ApplyFilter((item) => item.Entity.ID <= 6);
            //AvailableSuppliers.FilterIn((item) => item.Entity > 3 && item.Key < 10);
        }


        public void InitDetails(ViewModelInitialiser init, OrderDetail order, DetailViewModel vm, dynamic data)
        {
            try
            {
                vm.Model = new OrderDetailFacade(order, vm.Channel);
                //Console.Clear();
                Console.WriteLine("ID:{0}, Desc:{1}, Data1:{2}, Data2:{3}", order.ID, order.Description, data.Data1, data.Data2);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(string.Format("{0}: {1}", ex.GetType().ToString(), ex.Message));
            }
        }


        public void TestSubRoutine() { }

        public void TestSubRoutineWithParameter(bool isTest) { }

        public bool TestFunction() { return true; }

        public bool TestFunctionWithParameter(bool isTest) { return isTest; }

        public bool TestFunctionWithTwoParameters(bool isTest, bool thisIsGay) { return isTest; }
    }

}
