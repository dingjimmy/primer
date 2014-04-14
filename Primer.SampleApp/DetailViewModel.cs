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
        protected override void InitialiseFields(FieldInitialiser pi)
        {

        }


        // Init Action Properties
        protected override void InitialiseCommands(CommandInitialiser ap)
        {
            
        }
    }
}
