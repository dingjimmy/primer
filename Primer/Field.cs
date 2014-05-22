// Copyright (c) James Dingle

using Primer.Messages;
using Primer.Validation;
using System;
using System.ComponentModel;

namespace Primer
{
    public class Field<T> : IDataField<T>, IValidationTarget, IDataErrorInfo
    {

        #region Constructors


        /// <summary>
        /// Default constructor. Only visible internally to assembly as the Init class is used to instantiate and initialse all DataProperties
        /// </summary>
        internal Field(string name, T initialValue, ViewModel parentViewModel) : this(name, initialValue, parentViewModel, false) { }



        /// <summary>
        /// Alternative constructor that allows readonly flag to be set. Only visible internally to assembly as the Init class is used to instantiate and initialse all DataProperties
        /// </summary>
        internal Field(string name, T initialValue, ViewModel parentViewModel, bool readOnly)
        {
            _IsReadOnly = readOnly;
            _Name = name;
            _CurrentValue = initialValue;
            _ViewModel = parentViewModel;
        }


        #endregion


        #region IDataField Support


        // private backing fields
        private bool _IsReadOnly;
        private string _Name;
        private T _CurrentValue;
        private ViewModel _ViewModel;



        /// <summary>
        /// Gets or sets the readonly state of the field. If true the data value will not be modified and the parent ViewModel will not be updated.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set { _IsReadOnly = value; }
        }



        /// <summary>
        /// Gets the name of this property. It is used by a ViewModel to uniqely identify the property when updating, validating and recording errors.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            internal set { _Name = value; }
        }



        /// <summary>
        /// Gets or sets the value of the data this proprty is responsible while automatically updating the state of the parent ViewModel.  
        /// </summary>
        public T Data
        {
            get 
            { 
                return _CurrentValue;
            }
            set
            {
                if (!_IsReadOnly)
                {
                    if (_ViewModel.UpdateProperty(_Name, ref _CurrentValue, value))
                    {
                        _ViewModel.Broadcast(new FieldChanged() { Sender = _ViewModel, Name = this.Name });
                    }
                }
            }
        }



        /// <summary>
        /// Gets the parent ViewModel.
        /// </summary>
        public ViewModel ViewModel
        {
            get { return _ViewModel; }
        }


        #endregion


        #region IValidationTarget Support


        public object ValueToValidate
        {
            get { return Data; }
        }


        public Type TypeToValidate
        {
            get { return typeof(T); }
        }


        #endregion


        #region IDataErrorInfo Support


        /// <Summary>
        /// Gets an error message indicating what is wrong with this Field.
        /// </Summary>
        /// <returns>An error message indicating what is wrong with this Field. The default value (when there are no errors) is an empty string .</returns>
        string IDataErrorInfo.Error
        {
            get { return _ViewModel[_Name]; }
        }



        /// <summary>
        /// Gets an error message indicating what is wrong with this Field.
        /// </summary>
        /// <param name="notused">Due to this (as far as i am aware) entirely unique implementation, this parameter is ignored; however it must remain here in order to satisfy the IDataErrorInfo interface.</param>
        /// <returns>An error message indicating what is wrong with this Field. The default value (when there are no errors) is an empty string .</returns>
        string IDataErrorInfo.this[string notused]
        {
            get { return _ViewModel[_Name]; }
        }


        #endregion


        #region Operator Overloads


        public override string ToString()
        {
            if (_CurrentValue != null)
                return _CurrentValue.ToString();
            else
                return String.Empty;
        }


        #endregion

    }
}
