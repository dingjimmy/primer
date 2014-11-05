using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Primer
{

    /// <summary>
    /// Base class that implements property change notification for binding clients.
    /// </summary>
    public class PropertyChangedBase : INotifyPropertyChanged
    {


        /// <summary>
        /// Event that is raised when a property's value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;




        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the property's value has changed.
        /// </summary>
        /// <param name="property">An expression that identifies the property that has changed.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            RaisePropertyChanged(this, property);
        }



        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the property's value has changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="property">An expression that identifies the property that has changed.</param>
        protected void RaisePropertyChanged<T>(object sender, Expression<Func<T>> property)
        {
            var propertyName = Reflection.GetPropertyName(property);
            RaisePropertyChanged(sender, propertyName);    
        }



        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the property's value has changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void RaisePropertyChanged(object sender, string propertyName)
        {
            var handlers = PropertyChanged;
            if (handlers != null) handlers(sender, new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged" /> event to notify any bound clients that the value of the provided properties have changed.
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


    }
}
