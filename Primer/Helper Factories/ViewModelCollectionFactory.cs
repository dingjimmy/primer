// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public class ViewModelCollectionFactory : IViewModelCollectionFactory
    {

        IViewModelFactory _Factory;


        public ViewModelCollectionFactory(IViewModelFactory vmFactory)
        {
            this._Factory = vmFactory;
        }


        public ViewModelCollection<T> Of<T, TSource>(IEnumerable<TSource> sourceItems, Action<ViewModel<T>, TSource> loadData) where T : ViewModel<T>
        {

            // create collection of desired type
            var collection = new ViewModelCollection<T>();


            // execute query and loop through the results
            foreach (var item in sourceItems)
            {

                // get new viewmodel instance
                var vm = _Factory.Of<T>();

                // load data
                loadData(vm, item);

                // add vm to collection
                collection.Add(vm);

            }


            // return the completed collection to the caller
            return collection;
        }


        public ViewModelCollection<T> Of<T, TSource>(IEnumerable<TSource> sourceItems, IMessagingChannel channel, Action<ViewModel<T>, TSource> loadData) where T : ViewModel<T>
        {

            // create collection of desired type
            var collection = new ViewModelCollection<T>();


            // execute query and loop through the results
            foreach (var item in sourceItems)
            {

                // get new viewmodel instance
                var vm = _Factory.Of<T>(channel);

                // load data
                loadData(vm, item);

                // add vm to collection
                collection.Add(vm);

            }


            // return the completed collection to the caller
            return collection;

        }

    }
}

