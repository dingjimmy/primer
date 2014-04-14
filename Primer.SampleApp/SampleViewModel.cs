using Primer.SmartProperties;
using System;
using System.Collections.ObjectModel;
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


        public ObservableCollection<DetailViewModel> Details { get; set; }


        public Command Ok { get; set; }
        public Command Cancel { get; set; }



        public SampleCustomerViewModel(DataContext ctx)
        {

            // Set dependancies
            _Context = ctx;


            // This call is required for the ViewModel to function correctly. 
            Initialise();

        }

        protected override void InitialiseFields(FieldInitialiser fi)
        {

            var query = from d in _Context.Details select d;


            ID = fi.Initialise<int>("ID").WithValue(1280571);
            FirstName = fi.Initialise<string>("FirstName").WithValue("Joeseph");
            FamilyName = fi.Initialise<string>("FamilyName").WithValue("Bloggs");
            StartDate = fi.Initialise<DateTime>("StartDate").WithValue("2014-02-27");
            EndDate = fi.Initialise<DateTime?>("EndDate").WithValue("2018-09-03");


            Details = fi.InitialiseCollection<DetailViewModel, OrderDetail>(query, (cfi, item, vm) =>
            {
                vm.ID = cfi.Initialise<int>("ID").WithValue(item.ID);
                vm.Description = cfi.Initialise<string>("Description").WithValue(item.Description);
            });

        }


        protected override void InitialiseCommands(CommandInitialiser ci)
        {
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
