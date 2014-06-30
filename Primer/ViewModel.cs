﻿// Copyright (c) James Dingle

using FluentValidation;
using Primer.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Primer
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModel<TModel> : INotifyPropertyChanged, IDataErrorInfo, IDisposable, IViewModel
    {


#region Constructor


        /// <summary>
        /// Primary Constructor
        /// </summary>
        public ViewModel() {}


#endregion


#region Initialisation


        private TModel _Model;

        /// <summary>
        /// Gets or sets the Model containing the data to be displayed by the View.
        /// </summary>
        public TModel Model
        {
            get { return _Model; }
            set
            {
                if (UpdateProperty("Model", ref _Model, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "Model", Sender = this });
                }
            }
        }



        private bool _IsLoaded = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the ViewModel has been initialised successfully.
        /// </summary>
        /// <returns>
        /// True if ViewModel initialisation has been successfull; otherwise false.
        /// </returns>
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set
            {
                if (UpdateProperty("IsLoaded", ref _IsLoaded, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "IsLoaded", Sender = this });
                }
            }
        }



        /// <summary>
        /// Requiered method for ViewModel to operate correctly. Triggers the internal initialisation method.
        /// </summary>
        /// <param name="dataSources">Objects to use in the viewmodel initialisation.</param>
        public void Initialise(params object[] dataSources)
        {

            try
            {

                // initialise the view model. This method is implemented in sub classes, therefore passing initialisation over to creator of the sub class.
                Initialise(new ViewModelInitialiser(this), dataSources);


                // set loaded state
                IsLoaded = true;

            }
            catch (Exception ex)
            {

                // set loaded state
                IsLoaded = false;

                // throw more descriptive exception to caller
                throw new InitialiseViewModelException("ViewModel initialisation has failed. Please see inner exception for further details", ex);

            }

        }



        /// <summary>
        /// Internal initialisation method. Implemented by a sub-class, this is where all the ViewModel initialisation should go!
        /// </summary>
        /// <param name="initialise">Handles initialisation of Lookups and ViewModelCollections</param>
        /// <param name="dataSources">The data-sources to initialise the ViewModel with.</param>
        protected internal abstract void Initialise(ViewModelInitialiser initialise, params object[] dataSources);



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



        private IValidator<TModel> _Validator;

        /// <summary>
        /// Gets or sets the Fluent-Validator that will used to validate properties on the Model.
        /// </summary>
        public IValidator<TModel> Validator
        {
            get { return _Validator; }
            set
            {
                if (UpdateProperty("Validator", ref _Validator, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "Validator", Sender = this });
                }
            }
        }



        /// <summary>
        /// Validates this property against the rules setup in the <see cref="ViewModel{TModel}.Validator" /> and returns a value that indicates whether the property passed validation.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <returns>True if the property has passed validation; false otherwise.</returns>
        protected bool Validate(string propertyName)
        {

            ClearError(propertyName);

            
            if (_Validator == null) return true;


            var result = _Validator.Validate(_Model, propertyName);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    SetError(error.PropertyName, error.ErrorMessage);
                }
            }


            return result.IsValid;

        }



        /// <summary>
        /// Validate multiple properties at once.
        /// </summary>
        protected void Validate(params string[] properties)
        {

            ClearError(properties);


            if (_Validator == null) return;


            var result = _Validator.Validate(_Model, properties);


            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
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

            var name = ((MemberExpression)propertyToSet.Body).Member.Name;

            if (UpdateProperty(name, ref currentValue, proposedValue, forceUpdate) && broadcastMessage)
            {
                Broadcast(new Messages.PropertyChanged() { Name = name, Sender = this });
            }

        }


        #endregion


#region Message Broadcasting


        private IMessagingChannel _Channel;

        /// <summary>
        /// Gets or sets the default messaging channel for this ViewModel.
        /// </summary>
        public IMessagingChannel Channel
        {
            get { return _Channel; }
            set
            {
                if (UpdateProperty("Channel", ref _Channel, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "Channel", Sender = this });
                }
            }
        }



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



        /// <summary>
        /// Listens to the default messaging channel for a particular message type and executes the provided delegate. This is a friendly wrapper of the <see cref="MessagingChannel.Listen{T}"/> method.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to execute when a message of the desired type is broadcast.</param>
        public void Listen<T>(Action<T> messageHandler) where T: IMessage
        {

            if (Channel != null)
            {

                // wrap the provided generic Action<T> delegete in an Action<IMessage> delegete such that we can easily add it to the messaging channel.
                Action<IMessage> wrapper = (m) =>
                    {
                        messageHandler((T)m);
                    };


                // add the new wrapper delegate to the channel
                Channel.Listen<T>(wrapper);

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
        /// <returns>The error message for the property. The default value that occours when there are no errors is an empty string .</returns>
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
