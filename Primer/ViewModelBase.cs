﻿// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Primer
{

    /// <summary>
    /// Represents the base class for all Primer ViewModels.
    /// </summary>
    public abstract class ViewModelBase : PropertyChangedBase, IViewModel
    {


        #region State Flags



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
                SetProperty(() => IsLoaded, ref _IsLoaded, value);
            }
        }




        private string _DisplayName;

        /// <summary>
        /// Gets or sets the display name of the ViewModel; Its main purpose is to be used as the text that appears on the title-bar of a window.
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                SetProperty(() => DisplayName, ref _DisplayName, value);
            }
        }
        


        #endregion


        #region Updating


        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
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
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
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
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
        /// </summary>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        /// <param name="broadcastMessage">Choose whether to broadcast a PropertyChanged message on the ViewModel messaging channel.</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate, bool broadcastMessage)
        {
            var propertyName = Reflection.GetPropertyName(propertyToSet);

            if (forceUpdate || !EqualityComparer<T>.Default.Equals(currentValue, proposedValue))
            {

                currentValue = proposedValue;
                RaisePropertyChanged(propertyName);

                if (broadcastMessage && _Channel != null)
                    _Channel.Broadcast(new PropertyChangedMessage() { Name = propertyName, Sender = this });

            }
        }


        #endregion


        #region Validation


        private IValidator _Validator;

        /// <summary>
        /// Gets or sets the default validator for this ViewModel.
        /// </summary>
        public IValidator Validator
        { 
            get { return _Validator; }
            protected set { _Validator = value; }
        }


        #endregion


        #region Messaging
        

        private IMessagingChannel _Channel;

        /// <summary>
        /// Gets or sets the default messaging channel for this ViewModel.
        /// </summary>
        public IMessagingChannel Channel
        {
            get { return _Channel; }
            protected set { _Channel = value; }
        }



        #endregion


        #region Logging


        private ILogger _Logger;

        /// <summary>
        /// Gets or sets the default error logger for this ViewModel.
        /// </summary>
        public ILogger Logger
        {
            get { return _Logger; }
            protected set { _Logger = value; }
        }


        #endregion

    }
}
