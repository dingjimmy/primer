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
        public Command Ok { get; set; }
        public Command Cancel { get; set; }




        public SampleCustomerViewModel(DataContext ctx)
        {

            // Set dependancies
            _Context = ctx;
            Channel = new MessagingChannel();


            // build queries
            var cusQuery = from c in _Context.Customers where c.ID == 1876309338 select c;
            var dtlQuery = from d in _Context.Details select d;
            var splQuery = from s in _Context.Suppliers select s;


            Model = new CustomerFacade(cusQuery.First());
            Channel = new MessagingChannel();
            //Validator = new CustomerValidator();

            
            // This call is required for the ViewModel to function correctly. 
            Initialise(dtlQuery, splQuery);


        }


        protected override void Initialise(ViewModelInitialiser initialise, object primaryDataSource, params object[] secondaryDataSources)
        {

            // Verify dependacies
            var details = primaryDataSource as IQueryable<OrderDetail>;
            var suppliers = secondaryDataSources[0] as IQueryable<Supplier>;



            // Init a collection of ViewModels using a specific initialisation method.
            Details = initialise.Collection<DetailViewModel, OrderDetail>(details, (cfi, item, vm) =>
                {
                    vm.ID = cfi.Field<int>("ID").WithValue(item.ID);
                    vm.Description = cfi.Field<string>("Description").WithValue(item.Description);
                    vm.IsLoaded = true;
                });



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
            this.Ok = new Command { Action = SaveThis, IsEnabled = true };
            this.Cancel = new Command { Action = CancelThis, IsEnabled = true };
            


            // Listen for FieldChanged messages
            Listen<FieldChanged>(m => System.Diagnostics.Debug.WriteLine("A 'FieldChanged' message was broadcast by '{0}' at '{1}'. Field Name: {2}", m.Sender.GetType().ToString(), m.BroadcastOn, m.Name));


            // Change a fields value; this will broadcast the FieldChanged message which should cause the above listener method to be executed!
            Details[0].Description.Data = "Testing! 1,2,3 testing.....";



            // Init validators
            var v = initialise.Validator<EmptyStringValidatorAttribute>().OnField("FamilyName").WithNoParameters();


            //throw new Exception("Test Exception");

            //IsLoaded = true;

 

        }

        private void Listen<T1>(Action<FieldChanged> action)
        {
            throw new NotImplementedException();
        }



        #region CommandMethods


        public void SaveThis()
        {


            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }

            Model.FirstName = "This is a test!";
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
    }

}
