// Copyright (c) James Dingle

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Primer.Lookups;
using System.Collections.Generic;

namespace Primer.SmartProperties
{


    /// <summary>
    /// 
    /// </summary>
    public class FieldInitialiser
    {

        ViewModel _TargetViewModel;


        #region Constructors


        // Primary Constructor
        public FieldInitialiser(ViewModel targetViewModel)
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
        public FieldInitialiser<T> Initialise<T>(string name)
        {
            return new FieldInitialiser<T>(name, _TargetViewModel);
        }


        #endregion


        #region Collection Initialisation Methods


        /// <summary>
        /// Creates a new <see cref="ViewModelCollection{TViewModel}" />. 
        /// </summary>
        /// <param name="query">A collection of {TEntity} that is used to initialise the <see cref="ViewModelCollection{TViewModel}"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        public ViewModelCollection<TViewModel> InitialiseCollection<TViewModel, TEntity>(IEnumerable<TEntity> query, Action<FieldInitialiser, TEntity, TViewModel> initialiseMethod) 
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
                var initialiser = new FieldInitialiser(vm);

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
        public ViewModelCollection InitialiseCollection<TViewModel, TEntity>(IEnumerable<TEntity> query)
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
                var fi = new FieldInitialiser(vm);
                var ci = new CommandInitialiser(vm);

                // init vm
                vm.InitialiseFields(item, fi);
                vm.InitialiseCommands(ci);

                // add vm to collection
                collection.Add(vm);
            }


            // return the completed collection to the caller
            return collection;

        }


        #endregion


        #region LookupInitialisation Methods

        /// <summary>
        /// Creates a new <see cref="Lookup{TEntity}"/>.
        /// </summary>
        /// <param name="entityCollection">A collection of {TEntity} that is used to initialise a <see cref="Lookup"/>. This is likley to be a Linq To Sql or Linq to Entities query, however any enumerable collection can be used.</param>
        public Lookup<TEntity> InitialiseLookup<TEntity>(IEnumerable<TEntity> entityCollection, Action<TEntity, LookupItem<string, TEntity, string>> initialiseMethod)
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


    }



    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldInitialiser<T>
    {


        private string _PropertyName;
        private ViewModel _ParentViewModel;
        private bool _IsPropertyReadOnly;



        /// <summary>
        /// Public constructor
        /// </summary>
        public FieldInitialiser(string name, ViewModel parentViewModel)
        {
            _PropertyName = name;
            _ParentViewModel = parentViewModel;
        }



        /// <summary>
        /// Set the initial value to the default value of the underlying type. 
        /// </summary>
        public Field<T> WithDefaultValue()
        {
            return new Field<T>(_PropertyName, default(T), _ParentViewModel);
        }



        /// <summary>
        /// Sets the initial value. 
        /// </summary>
        public Field<T> WithValue(T value)
        {
            return new Field<T>(_PropertyName, value, _ParentViewModel);
        }



        /// <summary>
        /// Sets an initial value for the DataProperty from it's string representation.
        /// </summary>
        public Field<T> WithValue(string value)
        {

            object convertedValue = default(T);
            var type = typeof(T);

            try
            {

                // Convert input string to required type. If type not supported then throw exception.
                if (type == typeof(string))
                {
                    convertedValue = value;
                }
                else if (type == typeof(DateTime))
                {
                    convertedValue = Convert.ToDateTime(value);
                }
                else if (type == typeof(DateTime?))
                {
                    if (value != null)
                    {
                        convertedValue = new DateTime?(Convert.ToDateTime(value));
                    }

                }
                else if (type == typeof(Int32))
                {
                    convertedValue = Convert.ToInt32(value);
                }
                else
                {
                    throw new NotSupportedException(String.Format("Converstion of string to {0} is not currently supported", type.Name));
                }


                // return new DataProperty with initial value
                return new Field<T>(_PropertyName, (T)convertedValue, _ParentViewModel);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Cannot initialise data property '{0}' with value '{1}'. See inner exception for more information", _PropertyName, value), ex);
            }
        }

    }



    //public class CollectionInitialiser<T>
    //{

    //}

}
