using Primer;
using System;
using System.Windows.Input;
using System.Linq;

namespace Primer.SampleApp
{

    public class SampleCustomerViewModel : ViewModel
    {

        // Dependancies
        DataContext _Context;


        // Data Properties
        public Field<int> ID { get; set; }
        public Field<string> FirstName { get; set; }
        public Field<string> FamilyName { get; set; }
        public Field<DateTime> StartDate { get; set; }
        public Field<DateTime?> EndDate { get; set; }


        public ViewModelCollection<DetailViewModel> Details { get; set; }

        public ViewModelCollection MoreDetails { get; set; }

        public Lookup<Supplier> AvailableSuppliers { get; set; }


        public Command Ok { get; set; }
        public Command Cancel { get; set; }



        public SampleCustomerViewModel(DataContext ctx)
        {

            // Set dependancies
            _Context = ctx;


            // build linq query
            var query = from d in _Context.Details select d;


            // This call is required for the ViewModel to function correctly. 
            Initialise(query);

        }

        
        

        protected override void Initialise(object source, ViewModelInitialiser initialise)
        {

            var query = source as IQueryable<OrderDetail>;


            ID = initialise.Field<int>("ID").WithValue(1280571);
            FirstName = initialise.Field<string>("FirstName").WithValue("Joeseph");
            FamilyName = initialise.Field<string>("FamilyName").WithValue("Bloggs");
            StartDate = initialise.Field<DateTime>("StartDate").WithValue("2014-02-27");
            EndDate = initialise.Field<DateTime?>("EndDate").WithValue("2018-09-03");


            Details = initialise.Collection<DetailViewModel, OrderDetail>(query, (cfi, item, vm) =>
                {
                    vm.ID = cfi.Field<int>("ID").WithValue(item.ID);
                    vm.Description = cfi.Field<string>("Description").WithValue(item.Description);
                });


            MoreDetails = initialise.Collection<DetailViewModel, OrderDetail>(query);


            AvailableSuppliers = initialise.Lookup<Supplier>(suppliers, (supplier, item) =>
                {
                    item.Key = supplier.ID;
                    item.Description = String.Format("{0} - {1}", supplier.Name, supplier.Branch);
                    item.Entity = supplier;
                });


            this.Ok = new Command { Action = SaveThis, IsEnabled = true };
            this.Cancel = new Command { Action = CancelThis, IsEnabled = true };

        }


        #region CommandMethods


        public void SaveThis()
        {

            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }
        }



        public void CancelThis()
        {

        }


        #endregion


        public void DoSomethingSpecial()
        {

        }
    }

}
