// Copyright (c) James Dingle

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Primer.Validation;

namespace Primer
{


    /// <summary>
    /// Provides methods for initialising the various componants of a ViewModel.
    /// </summary>
    public class ViewModelInitialiser
    {

        ViewModel _TargetViewModel;


        #region Constructors


        // Primary Constructor
        public ViewModelInitialiser(ViewModel targetViewModel)
        {
            if (targetViewModel != null)
                _TargetViewModel = targetViewModel;
            else
                throw new ArgumentNullException("targetViewModel");
        }


        #endregion


        #region Field Initialisation Methods


        /// <summary>
        /// Creates a new <see cref="Primer.SmartProperties.Field{T}"/>, and begins the initialisation process.
        /// </summary>
        /// <typeparam name="T">The underlying data-type for this <see cref="Primer.SmartProperties.Field{R}"/>.</typeparam>
        /// <returns>A  that can be used to complete the initialisation process.</returns>
        public FieldInitialiser<T> Field<T>(string name)
        {
            return new FieldInitialiser<T>(name, _TargetViewModel);
        }


        #endregion


        #region Collection Initialisation Methods


        /// <summary>
        /// Creates a new <see cref="ViewModelCollection{TViewModel}" />. 
        /// </summary>
        /// <param name="query">A collection of {TEntity} that is used to initialise the <see cref="ViewModelCollection{TViewModel}"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        public ViewModelCollection<TViewModel> Collection<TViewModel, TEntity>(IEnumerable<TEntity> query, Action<ViewModelInitialiser, TEntity, TViewModel> initialiseMethod) 
            where TViewModel : ViewModel, new()
        {

            // init collection of desired type
            var collection = new ViewModelCollection<TViewModel>();


            // execute query and loop through the results
            foreach (var item in query)
            {

                // init new viewmodel
                var vm = new TViewModel();

                // init new data-initialiser
                var initialiser = new ViewModelInitialiser(vm);

                // set viewmodel to use its parent's messaging channel, allowing them to blissfully communicate with each-other!
                vm.Channel = _TargetViewModel.Channel;

                // action the init function supplied by caller
                initialiseMethod(initialiser, item, vm);

                // add vm to collection
                collection.Add(vm);
            }


            // return the completed collection to the caller
            return collection;

        }



        /// <summary>
        /// Creates a new <see cref="ViewModelCollection" />. 
        /// </summary>
        /// <param name="query">A collection of {TEntity} that is used to initialise the <see cref="ViewModelCollection"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        public ViewModelCollection Collection<TViewModel, TEntity>(IEnumerable<TEntity> query)
            where TViewModel : ViewModel, new()
        {

            // init collection of desired type
            var collection = new ViewModelCollection();


            // execute query and loop through the results
            foreach (var item in query)
            {

                // init new viewmodel
                var vm = new TViewModel();

                // init new data-initialiser
                var fi = new ViewModelInitialiser(vm);

                // set viewmodel to use its parent's messaging channel, allowing them to blissfully communicate with each-other!
                vm.Channel = _TargetViewModel.Channel;

                // init vm
                vm.Initialise(fi, item);

                // add vm to collection
                collection.Add(vm);
            }


            // return the completed collection to the caller
            return collection;

        }


        #endregion


        #region Lookup Initialisation Methods


        /// <summary>
        /// Creates a new <see cref="Primer.Lookups.Lookup{TEntity}"/>.
        /// </summary>
        /// <param name="entityCollection">A collection of [TEntity] that is used to initialise a <see cref="Lookup"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        public Lookup<TEntity> Lookup<TEntity>(IEnumerable<TEntity> entityCollection, Action<TEntity, LookupItem<string, TEntity, string>> initialiseMethod)
        {

            // init lookup collection
            var lookup = new Lookup<TEntity>();


            // loop through entity collection and create a lookupitem for each entity.
            foreach (var entity in entityCollection)
            {

                var item = new LookupItem<string, TEntity, string>();

                initialiseMethod(entity, item);

                lookup.Add(item);

            }


            // return completed lookup to caller
            return lookup;

        }


        #endregion


        #region Validation Initialisation Methods


        public ValidatorAttacher<T> Validator<T>() where T: ValidatorAttribute, new()
        {
            return new ValidatorAttacher<T>(_TargetViewModel);
        }


        #endregion


    }

}
