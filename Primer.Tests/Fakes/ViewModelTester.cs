using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primer;

namespace Primer.Tests.ProtectedTesters
{

    ///// <summary>
    ///// This class is to be used specificaly for the purpose of allowing of protected methods to be 
    ///// unit tested; All the methods in the class are wrappers for protected methods in the base class.
    ///// </summary>
    //public class ViewModelTester : ViewModel
    //{
    //    public bool TestUpdateProperty<T>(string propertyName, ref T currentValue, T proposedValue, bool forceUpdate)
    //    {
    //        return UpdateProperty<T>(propertyName, ref currentValue, proposedValue, forceUpdate );
    //    }

    //    protected override void Initialise(ViewModelInitialiser initialise, object primaryDataSource, params object[] secondaryDataSources)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}

}
