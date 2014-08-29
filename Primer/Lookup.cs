// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Primer
{
    public class Lookup<TEntity> : ObservableCollection<ILookupItem<string, TEntity, string>> 
    {


        private IList<ILookupItem<string, TEntity, string>> _HiddenItems;


        /// <summary>
        /// Primary Constructor: Creates a new instance of the <see cref="Primer.Lookup{TEntity}"/> Class.
        /// </summary>
        public Lookup()
        {
            _HiddenItems = new List<ILookupItem<string, TEntity, string>>();

        }



        /// <summary>
        /// Adds a new item to the Lookup.
        /// </summary>
        /// <param name="key">The key used to uniqely identify each item in the lookup.</param>
        /// <param name="entity"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public LookupItem<string, TEntity, string> Add(string key, TEntity entity, string description)
        {
            var item = new LookupItem<string, TEntity, string>() { Key = key, Entity = entity, Description = description };
            this.Add(item);
            return item;
        }



        /// <summary>
        /// Clears the currently applied filter and restored the lookup to its initial state.
        /// </summary>
        public void ClearFilter()
        {

            // check if filter has been applied. if so then clear it, otherwise do nothing
            if (_HiddenItems.Count > 0)
            {
            

                // copy all items from lookup (visible & hidden) into temp collection
                var tmp = new List<ILookupItem<string, TEntity, string>>(this);
                tmp.AddRange(_HiddenItems);


                // clear all items from lookup
                this.Clear();
                _HiddenItems.Clear();

            
                // add items back into lookup in ascending key order
                foreach (var item in tmp.OrderBy(i => i.Key))
                {
                    this.Add(item);
                }

            }
        }


        
        /// <summary>
        /// Filter the lookup based using the supplied criteria. Items that do not match the criteria will be hidden from the lookup.
        /// </summary>
        /// <param name="criteria">The expression that will be used to filter the lookup.,</param>
        public void ApplyFilter(Func<ILookupItem<string, TEntity, string>, bool> criteria)
        {

            // clear the existing filter (if there is one)
            ClearFilter();


            // get a list of the items that match the criteria and we want to keep visible
            var visible = this.Where(criteria).ToList();


            // get temp copy of lookup items: prevents invalid operation when removing items from a list you are enumerating.
            var tmp = this.ToList();


            // loop through all items in the list and hide all the ones not in the visible list
            foreach (var item in tmp)
            {
                if (!visible.Contains(item))
                {
                    _HiddenItems.Add(item);
                    this.Remove(item);
                }
                
            }

        }
    }
}
