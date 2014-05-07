// Copyright (c) James Dingle

using System;

namespace Primer.Validation
{

    /// <summary>
    /// When overridden in a derived class, handles the validation of a target object against a pre-determined set of rules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    public abstract class ValidatorAttribute: Attribute, IComparable<ValidatorAttribute>
    {


        #region IsValid Property


        // property backing field
        private bool _IsValid;

        /// <summary>
        /// Gets a value that indicates whether the property this attribute adorns has passed validation.
        /// </summary>
        /// <returns>
        /// True if the property has passed validation; otherwise false.
        /// </returns>
        public bool IsValid
        {
            get { return _IsValid; }
            protected set { _IsValid = value; }
        }


        #endregion


        #region Message Property


        // property backing field
        string _Message;

        /// <summary>
        /// Gets a string that describes the reason for the property this attribute adorns failing validation. If validation has passed then <see cref="String.Empty"/> is returned.
        /// </summary>
        public string Message
        {
            get { return _Message; }
            protected set { _Message = value; }
        }


        #endregion


        #region ProcessingOrder Property


        // property backing field
        private byte _ProcessingOrder;

        /// <summary>
        /// Gets or Sets the order in which the validation attribute is processed, in relation to other validation attributes that may be adorning the same property.
        /// </summary>
        public byte ProcessingOrder
        {
            get { return _ProcessingOrder; }
            set { _ProcessingOrder = value; }
        }


        #endregion


        #region Methods


        /// <summary>
        /// Validates a target's value.
        /// </summary>
        /// <param name="target">The target object we wish to validate.</param>
        public abstract void Validate(IValidationTarget target);



        /// <summary>
        /// Initialise the validator with the provided parameters.
        /// </summary>
        public abstract void Initialise(params object[] parameters);



        /// <summary>
        /// Sets the validation state of this Attribute.
        /// </summary>
        /// <param name="isValid">Sets the value of the <see cref="ValidatorAttribute.IsValid"/> property.</param>
        /// <param name="message">Sets the value of the <see cref="ValidatorAttribute.Message"/> property.</param>
        protected void SetState(bool isValid, string message)
        {
            _IsValid = isValid;
            _Message = message;
        }


        
        /// <summary>
        /// Compares the processing order of this attribute, against another. Used by a ViewModel to sort attrubutes for a particular property into ascending order.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ValidatorAttribute other)
        {
            return _ProcessingOrder.CompareTo(other._ProcessingOrder);
        }


#endregion

    }

}
