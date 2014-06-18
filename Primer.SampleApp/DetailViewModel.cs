using Primer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Primer.SampleApp
{
    public class DetailViewModel : ViewModel<OrderDetail>
    {


        // Constructor
        public DetailViewModel() : base() 
        {

            // This call is required for the ViewModel to function correctly. 
            Initialise();

        }


        // Init Data Properties
        protected override void Initialise(ViewModelInitialiser initialise, object primaryDataSource, params object[] secondaryDataSources)
        {

        }

    }
}
