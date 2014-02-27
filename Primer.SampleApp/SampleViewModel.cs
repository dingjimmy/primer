using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Primer;
using Primer.SmartProperties;
using Primer.Validation;

namespace Primer.SampleApp
{

    public class SampleCustomerViewModel : ViewModel
    {

        public DataProperty<int> ID { get; set; }
        public DataProperty<string> FirstName { get; set; }
        public DataProperty<string> FamilyName { get; set; }
        public DataProperty<DateTime> StartDate { get; set; }
        public DataProperty<DateTime?> EndDate { get; set; }


        public ICommand Ok { get; set; }
        public ICommand Cancel { get; set; }



        public SampleCustomerViewModel() : base() { }


        public override void InitialiseDataProperties(DataPropertyInitialiser pi)
        {
            ID = pi.Initialise<int>("ID", this).WithValue(1280571);
            FirstName = pi.Initialise<string>("FirstName", this).WithValue("Joeseph");
            FamilyName = pi.Initialise<string>("FamilyName", this).WithValue("Bloggs");
            StartDate = pi.Initialise<DateTime>("StartDate", this).WithValue("2014-02-27");
            EndDate = pi.Initialise<DateTime?>("EndDate", this).WithValue("2018-09-03");         
        }


        public override void InitialiseActionProperties(ActionPropertyInitialiser pi)
        {
            
        }



        #region CommandMethods


        public void Save(object parameter)
        {

            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }
        }



        public void CancelThis(object parameter)
        {

        }


        #endregion

    }

}
