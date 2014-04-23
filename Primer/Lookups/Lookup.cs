using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer.Lookups
{
    public class Lookup<TEntity> : ObservableCollection<ILookupItem<string, TEntity, string>> { }
}
