// Copyright (c) James Dingle

using Primer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Primer.SmartProperties;


namespace Primer
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo, IDisposable
    {


#region Fields

        private Dictionary<string, string> _Errors;
        private Dictionary<string, List<ValidatorAttribute>> _Validators;

#endregion


#region Properties


        /// <summary>
        /// Gets a value that indicates whether the ViewModel has properties in an error state.
        /// </summary>
        /// <returns>
        /// True if the ViewModel does have properties in an error state; otherwise false.
        /// </returns>
        protected bool HasErrors { get { return _Errors.Count > 0 ? true : false; } }


#endregion


#region Constructor


        /// <summary>
        /// Primary Constructor
        /// </summary>
        public ViewModel()
        {

            // init error and validator dictionaries
            _Errors = new Dictionary<string, string>();
            _Validators = new Dictionary<string, List<ValidatorAttribute>>();

        }


#endregion


#region Initialise Methods



        /// <summary>
        /// Requiered method for ViewModel to operate correctly. Caches validation attributes, but DOES NOT trigger initialisation of Fields and Commands; this must be done seperatly.
        /// </summary>
        protected void Initialise()
        {

            // cache validators
            CacheValidatorAttributes();
     
        }


        /// <summary>
        /// Requiered method for ViewModel to operate correctly. Caches validation attributes and triggers initialisation of Fields and Commands
        /// </summary>
        /// <param name="source">The source object to initialise the viewmodel with. Usually an entity or linq query, but could be anyting.</param>
        protected void Initialise(object source)
        {

            // cache validators
            CacheValidatorAttributes();


            // initialise data properties
            InitialiseFields(source, new FieldInitialiser(this));
            

            // initialise action properties
            InitialiseCommands(new CommandInitialiser(this));

        }


        protected internal abstract void InitialiseFields(object source, FieldInitialiser fi);


        protected internal abstract void InitialiseCommands(CommandInitialiser ci);


#endregion


#region Update Property Methods


        /// <summary>
        /// Compares the current and proposed values and raises the <see cref="ViewModel.PropertyChanged"/> event if they are not equal.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <returns>The proposed value if the values are not equal; otherwise the current value.</returns>
        public T UpdateProperty<T>(string propertyName, T currentValue, T proposedValue)
        {

            if (!EqualityComparer<T>.Default.Equals(currentValue, proposedValue))
            {
                RaisePropertyChanged(this, propertyName);
                return proposedValue;
            }
            else
            {
                return currentValue;
            }

        }


#endregion


#region Validation Methods


        /// <summary>
        /// Validates this property against its adorned <see cref="ValidatorAttribute" />s and returns a value that indicates whether the property passed validation.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <returns>True if the property has passed validation; false otherwise.</returns>
        protected bool Validate(string propertyName)
        {
            bool isValid = true;


            // clear the error state on this property
            ClearError(propertyName);


            // check if the property has validators attached to it
            if (_Validators.ContainsKey(propertyName))
            {

                // get the value of the property
                var value = this.GetType().GetProperty(propertyName).GetValue(this, null);


                // get a list of all validator attributes assigned to the property
                var validators = _Validators[propertyName];


                // loop through all validator attributes
                foreach (var validator in validators)
                {

                    // run validation
                    validator.Validate(value);


                    // if validation was not successfull then set error and don't process the remaining attributes
                    if (!validator.IsValid)
                    {
                        SetError(propertyName, validator.Message);
                        isValid = false;
                        break;
                    }
                }
            }

            // return a value indicating whether this property is valid or not.
            return isValid;

        }



        /// <summary>
        /// Validate multiple properties at once.
        /// </summary>
        protected void Validate(params string[] properties)
        {
            foreach (var p in properties)
            {
                Validate(p);
            }
        }



        /// <summary>
        /// Checks to see if the properties are in an error state and returns a value that confirms if this is the case. 
        /// </summary>
        /// <param name="properties">A list of properties to check.</param>
        /// <returns>True if the properties are in an error state; false otherwise.</returns>
        protected bool InError(params string[] properties)
        {

            // check all provided properties to see if any are in error and return true if so.
            foreach (var p in properties)
            {
                if (_Errors.ContainsKey(p)) return true;
            }

            // if we get here then all the properties are error free, so return false.
            return false;
        }



        /// <summary>
        /// Puts a property into an error state.
        /// </summary>
        /// <param name="propertyName">The name of the property you want to affect.</param>
        /// <param name="errorMessage">The error message to assign to the property.</param>
        protected void SetError(string propertyName, string errorMessage)
        {

            // does this property alredy exist? If so then overwrite the error message; otherwise add a new error.
            if (_Errors.ContainsKey(propertyName))
                _Errors[propertyName] = errorMessage;

            else
                _Errors.Add(propertyName, errorMessage);

        }



        /// <summary>
        /// Clears the error state of a property.
        /// </summary>
        /// <param name="propertyName">The name of the property you want to clear.</param>
        protected void ClearError(string propertyName)
        {
            // does this property already exist? If so then remove it!
            if (_Errors.ContainsKey(propertyName)) _Errors.Remove(propertyName);
        }



        /// <summary>
        /// Clears the error state of multiple properties.
        /// </summary>
        /// <param name="properties">An array containing the names of the properties you want to clear.</param>
        protected void ClearError(params string[] properties)
        {
            foreach (var p in properties)
            {
                ClearError(p);
            }
        }



        /// <summary>
        /// Clears the error state for all properties.
        /// </summary>
        protected void ClearAllErrors()
        {
            _Errors.Clear();
        }


#endregion


#region Attribute Caching Methods


        /// <summary>
        /// Caches all property validator attributs. This is to save reduce processing time later, as reading attributes via reflection is an expensive run time 
        /// operation and doing it 'on-the'fly' is not recommended!
        /// </summary>
        protected void CacheValidatorAttributes()
        {

            // get a list of all public properties on this instance
            var properties = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);


            // loop through each property
            foreach (var p in properties)
            {

                // check property doesnt already exist; this is very reate as normally you cannot have
                // two proprties with the same name. If it does then do nothing and goto next property.
                if (_Validators.ContainsKey(p.Name)) continue;


                // get all validator attributes for this property
                var validators = (ValidatorAttribute[])p.GetCustomAttributes(typeof(ValidatorAttribute), false);


                /*  now sorting on the generic list, rather than the array
                // sort validator attributes into correct processing order
                Array.Sort<ValidatorAttribute>(validators);
                */

                // prepare attribute list
                var list = validators.ToList();

                // sort validator attributes into correct processing order
                list.Sort();


                // add this list of validator attributes to the ViewModels to validator dictionary
                _Validators.Add(p.Name, list);

            }

        }



        protected void AddValidatorAttribute(string propertyName, ValidatorAttribute attribute)
        {

            // if property exists and the attribute isnt already added to the property, then add it.
            if (_Validators.ContainsKey(propertyName))
            {
                if (!_Validators[propertyName].Contains(attribute)) _Validators[propertyName].Add(attribute);
            }

        }


#endregion


#region INotifyPropertyChanged Support


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// Raises the <see cref="ViewModel.PropertyChanged" /> event to notify any listeners that the property's value has changed. .
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void RaisePropertyChanged(object sender, string propertyName)
        {

            // to avoid race conditions, we get a reference to any handlers befor invoking
            var handlers = PropertyChanged;

            // invoke the event
            if (handlers != null) handlers(sender, new PropertyChangedEventArgs(propertyName));

        }



        /// <summary>
        /// Raises the <see cref="ViewModel.PropertyChanged" /> event to notify any listeners that the value of the provided properties have changed. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="properties">A list of the properties that have been changed.</param>
        protected void RaisePropertyChanged(object sender, params string[] properties)
        {
            foreach (var p in properties)
            {
                RaisePropertyChanged(sender, p);
            }
        }


#endregion


#region IDataErrorInfo Support


        string _Error = string.Empty;


        /// <Summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </Summary>
        /// <returns> An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get { return _Error; }        
        }


        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string propertyName]
        {
            get 
            { 
                if( _Validators.ContainsKey(propertyName))
                {
                    if (!Validate(propertyName)) return _Errors[propertyName];
                }

                return String.Empty;
            }
        }


#endregion


#region IDisposable Support


        protected bool _IsDisposed = false;


        /// <summary>
        /// Clear up any left-over resources used by this ViewModel.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {

            if (_IsDisposed) throw new ObjectDisposedException("ViewModel");


            // get rid of managed resources
            if(disposing)
            {
                
            }


            // get rid of unmanaged resources
            // TODO: Only required if you use unmanaged resources directly in this class.


            // set flag to confirm disposal complete
            _IsDisposed = true;

        }



        // Class destructr: Only required if you use unmanaged resources directly in this class.
        //~ViewModel()
        //{
        //  Dispose(false)
        //}


#endregion


    }
}
