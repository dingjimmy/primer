using Primer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Primer.SampleApp
{
    public class DetailViewModel : ViewModel
    {

        // Dependancies



        // Data Properties
        public Field<int> ID;
        public Field<string> Description;


        // Constructor
        public DetailViewModel() : base() 
        {

            // This call is required for the ViewModel to function correctly. 
            Initialise();

        }


        // Init Data Properties
        protected override void Initialise(object source, ViewModelInitialiser fi)
        {
            var detail = source as OrderDetail;

            ID = fi.Field<int>("ID").WithValue(detail.ID);
            Description = fi.Field<string>("Description").WithValue(detail.Description);
        }

    }
}
