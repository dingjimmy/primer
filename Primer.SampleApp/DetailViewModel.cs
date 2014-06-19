using Primer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Primer.SampleApp
{
    public class DetailViewModel : ViewModel<OrderDetailFacade>
    {
        protected override void Initialise(ViewModelInitialiser initialise, params object[] dataSources)
        {
            this.Model = new OrderDetailFacade(dataSources[0] as OrderDetail, this.Channel);
        }
    }
}
