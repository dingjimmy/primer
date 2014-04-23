using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primer.SmartProperties;

namespace Primer.Lookups
{
    public interface ILookupItem<TKey, TEntity, TDescription> : IDisposable
    {
        TKey Key { get; set; }
        TEntity Entity { get; set; }
        TDescription Description { get; set; }
    }
}
