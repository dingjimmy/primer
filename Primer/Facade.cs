// Copyright (c) James Dingle;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Primer
{
    public class Facade : INotifyPropertyChanged
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


        #region INotifyPropertyChanged Support


        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
