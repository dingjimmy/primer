// Copyright (c) James Dingle;

using FluentValidation;
using Primer.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Primer
{
    public class Facade : INotifyPropertyChanged, IDataErrorInfo
    {


        #region Message Broadcasting


        /// <summary>
        /// Gets or sets the default messaging channel for this Facade.
        /// </summary>
        public IMessagingChannel Channel;



        /// <summary>
        /// Broadcasts a message of the desired type on ViewModels default messaging channel.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        public void Broadcast(IMessage message)
        {
            if (Channel != null)
            {
                Channel.Broadcast(message);
            }
        }


        #endregion


        #region Property Updating


        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/> event is raised.
        /// </summary>
        /// <param name="propertyName">The name of the property to change.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        /// <returns>True if the current value has been updated, false otherwise.</returns>
        [ObsoleteAttribute("This method will soon be removed from the public api. Please use the 'SetProperty()' method instead.")]
        public bool UpdateProperty<T>(string propertyName, ref T currentValue, T proposedValue, bool forceUpdate)
        {

            if (forceUpdate || !EqualityComparer<T>.Default.Equals(currentValue, proposedValue))
            {
                currentValue = proposedValue;
                RaisePropertyChanged(propertyName);
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/> 
        /// event is raised and <see cref="Primer.Messages.PropertyChanged"/> message is broadcast.
        /// </summary>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue)
        {
            SetProperty(propertyToSet, ref currentValue, proposedValue, false, true);
        }


        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/> 
        /// event is raised and <see cref="Primer.Messages.PropertyChanged"/> message is broadcast.
        /// </summary>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate)
        {
            SetProperty(propertyToSet, ref currentValue, proposedValue, forceUpdate, true);
        }


        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/> 
        /// event is raised and <see cref="Primer.Messages.PropertyChanged"/> message is broadcast.
        /// </summary>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        /// <param name="broadcastMessage">Choose whether to broadcast a PropertyChanged message on the ViewModel messaging channel.</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate, bool broadcastMessage)
        {

            var name = GetPropertyName(propertyToSet);

            if (UpdateProperty(name, ref currentValue, proposedValue, forceUpdate) && broadcastMessage)
            {
                Broadcast(new Messages.PropertyChanged() { Name = name, Sender = this });
            }

        }


        #endregion


        #region Validation


        private Dictionary<string, string> _Errors = new Dictionary<string, string>();

        /// <summary>
        /// Gets a value that indicates whether the ViewModel has properties in an error state.
        /// </summary>
        /// <returns>
        /// True if the ViewModel does have properties in an error state; otherwise false.
        /// </returns>
        protected bool HasErrors
        {
            get { return _Errors.Count > 0 ? true : false; }
        }



        /// <summary>
        /// Gets or sets the Fluent-Validator that will used to validate properties on the Model.
        /// </summary>
        public IValidator Validator
        {
            get { return _Validator; }
            set { _Validator = value; }
        }
        private IValidator _Validator;





        /// <summary>
        /// Validates this property against the rules setup in the <see cref="ViewModel{TModel}.Validator" /> and returns a value that indicates whether the property passed validation.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <returns>True if the property has passed validation; false otherwise.</returns>
        public bool Validate(string propertyName)
        {

            ClearError(propertyName);


            if (_Validator == null) return true;


            var result = _Validator.Validate(this);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    if (propertyName == error.PropertyName)
                        SetError(error.PropertyName, error.ErrorMessage);
                }
            }

            return !InError(new[] {propertyName});

        }



        /// <summary>
        /// Validate multiple properties at once.
        /// </summary>
        public void Validate(params string[] properties)
        {

            ClearError(properties);


            if (_Validator == null) return;


            var result = _Validator.Validate(this);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    if (properties.Contains(error.PropertyName))
                        SetError(error.PropertyName, error.ErrorMessage);
                }
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


        #region INotifyPropertyChanged Support


        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion


        #region IDataErrorInfo Support


        string _Error = string.Empty;
        public string Error
        {
            get { return _Error; }
        }


        public string this[string propertyName]
        {
            get
            {
                if (!Validate(propertyName))
                    return _Errors[propertyName];
                else
                   return String.Empty;
            }
        }


        #endregion


        #region Reflection Helpers


        public string GetMethodName<T>(Expression<Func<T>> methodToInspect)
        {
            return ((MethodCallExpression)methodToInspect.Body).Method.Name;
        }


        public string GetPropertyName<T>(Expression<Func<T>> propertyToInspect)
        {
            return ((MemberExpression)propertyToInspect.Body).Member.Name;
        }


        #endregion

    }
}
