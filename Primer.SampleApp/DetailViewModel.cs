using Primer.SmartProperties;
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
        protected override void InitialiseFields(object source, FieldInitialiser fi)
        {
            var detail = source as OrderDetail;

            ID = fi.Initialise<int>("ID").WithValue(detail.ID);
            Description = fi.Initialise<string>("Description").WithValue(detail.Description);
        }


        // Init Action Properties
        protected override void InitialiseCommands(CommandInitialiser ap)
        {
            
        }
    }
}
