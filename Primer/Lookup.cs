// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer
{
    public class Lookup<TEntity> : ObservableCollection<ILookupItem<string, TEntity, string>> 
    {

        private IEnumerable<ILookupItem<string, TEntity, string>> FilteredItems { get; set; }
        
        public void FilterOut(Func<ILookupItem<string, TEntity, string>, bool> criteria)
        {

            // get a copy of all items we want to filter-out
            FilteredItems = null;
            FilteredItems = this.Where(criteria);

            // remove these items from this Lookup
            foreach (var item, FilteredItems)
            {

            }

        }
    }
}
