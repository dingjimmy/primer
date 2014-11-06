// Copyright (c) James Dingle

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace Primer.Helpers
{
    public static class ViewModelHelpers
    {

        /// <summary>
        /// A helper that creates a Lookup to be exposed by the <see cref="Primer.ViewModel"/>, based upon a collection of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type"/> of the source items.</typeparam>
        /// <param name="viewModel">The <see cref="Primer.ViewModel"/> the new lookup will be exposed by.</param>
        /// <param name="sourceItems">The collection source</param>
        /// <param name="keyExpression">A lamda expression (or annonymous function) that specifies the source item property to use as the lookup key.</param>
        /// <param name="descriptionExpression">A lamda expression (or annonymous function) that specifies the source item property to use a the lookup description.</param>
        /// <returns>An instance of <see cref="Primer.ILookup<T>"/>; That is a collection of <see cref="ILookupItem<T>"/> that can be displayed by a View.</returns>
        public static ILookup<T> CreateLookup<T>(this ViewModelBase viewModel, IEnumerable<T> sourceItems, Func<T, string> keyExpression, Func<T, string> descriptionExpression)
        {

            // init lookup collection
            ILookup<T> lookup = new Lookup<T>();


            // create a lookup-item for each source-item
            foreach (var sourceItem in sourceItems)
            {             
                lookup.Add(keyExpression(sourceItem), sourceItem , descriptionExpression(sourceItem));
            }

            // return completed lookup to caller
            return lookup;

        }


        public static ICommand CreateCommand(this ViewModelBase viewModel, Action actionToExecute)
        {
            throw new NotImplementedException();
        }


        public static ICommand CreateCommand<T>(this ViewModelBase viewModel, Action<T> actionToExecute)
        {
            throw new NotImplementedException();
        }

    }
}

