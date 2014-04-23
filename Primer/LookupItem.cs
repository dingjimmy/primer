// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer
{
    public class LookupItem<TKey, TEntity, TDescription>: ILookupItem<TKey, TEntity, TDescription>
    {

        public TKey Key { get; set; }

        public TEntity Entity { get; set; }

        public TDescription Description { get; set; }

    }
}
