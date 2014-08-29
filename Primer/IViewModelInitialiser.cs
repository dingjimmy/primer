// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModelInitialiser
    {


        /// <summary>
        /// Creates a new <see cref="ViewModelCollection" />. 
        /// </summary>
        /// <param name="query">A collection of {TEntity} that is used to initialise the <see cref="ViewModelCollection"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        ViewModelCollection Collection<TViewModel, TEntity>(System.Collections.Generic.IEnumerable<TEntity> query) where TViewModel : IViewModel, new();



        /// <summary>
        /// Creates a new <see cref="ViewModelCollection{TViewModel}" />. 
        /// </summary>
        /// <param name="query">A collection of {TEntity} that is used to initialise the <see cref="ViewModelCollection{TViewModel}"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        /// <param name="initialiseMethod">An action delegete that handles the initialsation of each new ViewModel in the collection.</param>
        ViewModelCollection<TViewModel> Collection<TViewModel, TEntity>(System.Collections.Generic.IEnumerable<TEntity> query, Action<ViewModelInitialiser, TEntity, TViewModel> initialiseMethod) where TViewModel : IViewModel, new();



        /// <summary>
        /// Creates a new <see cref="Primer.Lookup{TEntity}"/>.
        /// </summary>
        /// <param name="entityCollection">A collection of {TEntity} that is used to initialise a <see cref="Lookup{TEntity}"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        /// <param name="initialiseMethod">An action delegete that handles the initialsation of each new <see cref="Primer.LookupItem{TKey, TEntity, TDescription}" /> in the Lookup.</param>
        Lookup<TEntity> Lookup<TEntity>(System.Collections.Generic.IEnumerable<TEntity> entityCollection, Action<TEntity, LookupItem<string, TEntity, string>> initialiseMethod);


    }
}
