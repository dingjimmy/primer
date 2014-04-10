using Primer.SmartProperties;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Primer.SampleApp
{
    class DetailViewModel : ViewModel
    {

        // Dependancies
        OrderDetail _Detail;


        // Data Properties
        public DataProperty<int> ID;
        public DataProperty<string> Description;


        // Constructor
        public DetailViewModel() : base() { }


        // Init Data Properties
        public override void InitialiseDataProperties(DataPropertyInitialiser pi)
        {
            pi.Initialise<int>("ID").WithValue(_Detail.ID);
            pi.Initialise<string>("Description").WithValue(_Detail.Description);
        }


        // Init Action Properties
        public override void InitialiseActionProperties(ActionPropertyInitialiser ap)
        {
            throw new System.NotImplementedException();
        }
    }
}
