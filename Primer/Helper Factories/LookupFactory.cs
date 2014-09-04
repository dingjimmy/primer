// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public class LookupFactory : ILookupFactory
    {

        /// <summary>
        /// Gets a new <see cref="Primer.Lookup{TEntity}"/> from the source data provided.
        /// </summary>
        /// <param name="sourceCollection">A collection of {TEntity} that is used to load the <see cref="Lookup{TEntity}" /> with items. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        /// <param name="key">An expression that returns a key that uniqely identifies each source item.</param>
        /// <param name="description">An expression that returns a brief (one or two word) description of the source item.</param>
        /// <param name="value">An express that returns the value of the source item; which can be the item itself.</param>
        public Lookup<T> From<T>(IEnumerable<T> sourceCollection, Func<T, string> key, Func<T, string> description, Func<T, T> value)
        {

            // init lookup collection
            var lookup = new Lookup<T>();


            // loop through entity collection and create a lookupitem for each entity.
            foreach (var source in sourceCollection)
            {

                try
                {

                    var item = new LookupItem<string, T, string>();

                    item.Key = key(source);
                    item.Description = description(source);
                    item.Entity = value(source);

                    lookup.Add(item);

                }
                catch (Exception ex)
                {
                    throw new LookupException("Creation of Lookup Failed. See inner exception for further info.", ex);
                }

            }


            // return completed lookup to caller
            return lookup;
        }
    }


    public class LookupException : ApplicationException
    {
        public LookupException(string message) : base(message) { }

        public LookupException(string message, Exception ex) : base(message, ex) { }
    }

}
