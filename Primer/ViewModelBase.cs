//-----------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="James Dingle">
//     Copyright (c) James Dingle
// </copyright>
//-----------------------------------------------------------------------

namespace Primer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the base class for all Primer ViewModels.
    /// </summary>
    public abstract class ViewModelBase : PropertyChangedBase, IViewModel
    {
        private bool isLoaded = false;
        private string displayName = string.Empty;
    
        /// <summary>
        /// Gets or sets a value indicating whether the ViewModel has been initialized successfully.
        /// </summary>
        public bool IsLoaded
        {
            get 
            { 
                return this.isLoaded; 
            }

            set
            {
                this.SetProperty(() => this.IsLoaded, ref this.isLoaded, value);
            }
        }     

        /// <summary>
        /// Gets or sets the display name of the ViewModel; Its main purpose is to be used as the text that appears on the title-bar of a window.
        /// </summary>
        public string DisplayName
        {
            get 
            { 
                return this.displayName; 
            }

            set
            {
                this.SetProperty(() => this.DisplayName, ref this.displayName, value);
            }
        }

        /// <summary>
        /// Gets or sets the default validator for this ViewModel.
        /// </summary>
        public IValidator Validator { get; set; }

        /// <summary>
        /// Gets or sets the default messaging channel for this ViewModel.
        /// </summary>
        public IMessagingChannel Channel { get; set; }

        /// <summary>
        /// Gets or sets the default error logger for this ViewModel.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
        /// </summary>
        /// <typeparam name="T">The type of value we are setting.</typeparam>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue)
        {
            this.SetProperty(propertyToSet, ref currentValue, proposedValue, false, true);
        }
        
        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
        /// </summary>
        /// <typeparam name="T">The type of value we are setting.</typeparam>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate)
        {
            this.SetProperty(propertyToSet, ref currentValue, proposedValue, forceUpdate, true);
        }

        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
        /// </summary>
        /// <typeparam name="T">The type of value we are setting.</typeparam>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        /// <param name="forceUpdate">Force the property to update, regardless of if the proposed and current values are the same.</param>
        /// <param name="broadcastMessage">Choose whether to broadcast a PropertyChanged message on the ViewModel's messaging channel.</param>
        public void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate, bool broadcastMessage)
        {
            var propertyName = Reflection.GetPropertyName(propertyToSet);

            if (forceUpdate || !EqualityComparer<T>.Default.Equals(currentValue, proposedValue))
            {
                currentValue = proposedValue;
                this.RaisePropertyChanged(propertyName);

                if (broadcastMessage && this.Channel != null)
                {
                    this.Channel.Broadcast(new PropertyChangedMessage() { Name = propertyName, Sender = this });
                }
            }
        }
    }
}
