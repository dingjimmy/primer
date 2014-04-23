// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer
{
    public interface ILookupItem<TKey, TEntity, TDescription>
    {
        TKey Key { get; set; }
        TEntity Entity { get; set; }
        TDescription Description { get; set; }
    }
}
