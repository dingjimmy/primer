// Copyright (c) James Dingle

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Primer
{

    public class Lookup<T> : ObservableCollection<ILookupItem<T>>, ILookup<T>
    {


        private IList<ILookupItem<T>> _HiddenItems;


        /// <summary>
        /// Primary Constructor: Creates a new instance of the <see cref="Primer.Lookup{T}"/> Class.
        /// </summary>
        public Lookup()
        {
            _HiddenItems = new List<ILookupItem<T>>();
        }



        /// <summary>
        /// Adds a new item to the Lookup.
        /// </summary>
        /// <param name="key">The key used to uniqely identify each item in the lookup.</param>
        /// <param name="entity"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public ILookupItem<T> Add(string key, T entity, string description)
        {

            var item = new LookupItem<T>() { Key = key, Entity = entity, Description = description };

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
                var tmp = new List<ILookupItem<T>>(this);
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
        public void ApplyFilter(Func<ILookupItem<T>, bool> criteria)
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


    public class LookupItem<T> : ILookupItem<T>
    {

        public String Key { get; set; }

        public String Description { get; set; }

        public T Entity { get; set; }

        public override string ToString()
        {
            return Description.ToString();
        }

    }

}
