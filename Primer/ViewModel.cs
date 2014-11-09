// Copyright (c) James Dingle

using System;
using System.ComponentModel;

namespace Primer
{

    /// <summary>
    /// A Base Class that represents a ViewModel as defined in the MVVM user interface development pattern. 
    /// </summary>
    /// <remarks> 
    /// A ViewModel is an abstraction of a View that mediates between the View and a related Model; it exposes
    /// data that can be displayed/edited on screen, and actions that can be executed to apply business logic 
    /// and update the Model.
    /// </remarks>
    public abstract class ViewModel: ViewModelBase, IViewModel, IDataErrorInfo, IDisposable
    {


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
                if (Validator != null && !Validator.Validate(propertyName))
                    return Validator.Errors[propertyName];
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
            if (disposing)
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
