//-----------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="James Dingle">
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
    /// Represents a ViewModel as defined in the MVVM user interface development pattern. Provides methods to 
    /// load, present, update and validate data to and from a View.
    /// </summary>
    /// <remarks> 
    /// A ViewModel is an abstraction of a View that mediates between the View and a related Model; it exposes
    /// data that can be displayed/edited on screen, and actions that can be executed to apply business logic 
    /// and update the Model.
    /// </remarks>
    public class ViewModel : IViewModel, IDataErrorInfo, INotifyPropertyChanged, IDisposable
    {
        private bool isLoaded = false;
        private bool isDisposed = false;
        private string displayName = string.Empty;
        private string error = string.Empty;

        /// <summary>
        /// Event that is raised when a property's value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns> An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                return this.error;
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default value that occurs when there are no errors is an empty string .</returns>
        public string this[string propertyName]
        {
            get
            {
                if (this.Validator != null && !this.Validator.Validate(propertyName))
                {
                    return this.Validator.Errors[propertyName];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Compares the current and proposed values; If they are not equal the current value is replaced with the proposed the <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// event is raised and <see cref="Primer.PropertyChangedMessage"/> message is broadcast.
        /// </summary>
        /// <typeparam name="T">The type of value we are setting.</typeparam>
        /// <param name="propertyToSet">An expression which identifies the property to update.</param>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="proposedValue">The proposed value of the property</param>
        public virtual void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue)
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
        public virtual void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate)
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
        public virtual void SetProperty<T>(Expression<Func<T>> propertyToSet, ref T currentValue, T proposedValue, bool forceUpdate, bool broadcastMessage)
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

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the property's value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        public virtual void RaisePropertyChanged(string propertyName)
        {
            this.RaisePropertyChanged(this, propertyName);
        }

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the property's value has changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="propertyName">The name of the property that has changed.</param>
        public virtual void RaisePropertyChanged(object sender, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Parameter 'propertyName' cannot be null, empty or full of whitespace.");
            }

            var handlers = this.PropertyChanged;
            if (handlers != null)
            {
                handlers(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the value of the provided properties have changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="properties">A list of the properties that have been changed.</param>
        public virtual void RaisePropertyChanged(object sender, params string[] properties)
        {
            foreach (var p in properties)
            {
                this.RaisePropertyChanged(sender, p);
            }
        }

        /// <summary>
        /// Clear up any left-over resources used by this ViewModel.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of managed and un-managed resources as appropriate.
        /// </summary>
        /// <param name="disposing">True to dispose of both managed and un-managed resources, false to dispose of un-managed resources only.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("ViewModel");
            }
            else
            {
                if (disposing)
                {
                    // TODO: Get rid of managed resources.
                }

                // TODO: Get rid of unmanaged resources. Only required if you use unmanaged resources directly in this class.

                // Set flag to confirm disposal complete.
                this.isDisposed = true;
            }
        }

        //// Class destructr: Only required if you use unmanaged resources directly in this class.
        ////~ViewModel()
        ////{
        ////  Dispose(false)
        ////}
    }
}
