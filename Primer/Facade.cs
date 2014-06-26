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


        public String GetPropertyName<T>(Expression<Func<T>> property)
        {
            MemberExpression body = (MemberExpression)property.Body;
            return body.Member.Name;
        }

        public string GetPropertyName<T>(T property)
        {
            return GetPropertyName(() => property);
        }


        public bool UpdateProperty<T>(T currentValue, T proposedValue, bool forceUpdate)
        {

            var name = GetPropertyName(currentValue);

            return SetProperty(name, ref currentValue , proposedValue, forceUpdate);

        }



        private int _Firstname;
        public int Firstname
        {
            get { return _Firstname; }
            set
            {
                if (UpdateProperty("Firstname", ref _Firstname, value, false))
                {
                    Broadcast(new Messages.PropertyChanged() { Name = "Firstname", Sender = this });
                }
            }
        }

        void test()
        {
            UpdateProperty(this._Firstname, 12345, true);
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
       

    }
}
