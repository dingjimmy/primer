// Copyright (c) James Dingle

using System;

namespace Primer.Validation
{


    /// <summary>
    /// Checks for Null values. Returns NotValid if found, Valid otherwise.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class NullValueValidatorAttribute : ValidatorAttribute
    {


        /// <summary>
        /// Validates a target's value.
        /// </summary>
        public override void Validate(IValidationTarget target)
        {

            // clear any errors
            SetState(true, string.Empty);


            // check value is not null
            if (target.ValueToValidate == null)
            {
                SetState(false, "This field cannot be empty. Please enter a value.");
            }


        }



        /// <summary>
        /// Initialise the validator with the provided parameters. This validator does not require any parameters to initialise.
        /// </summary>
        public override void Initialise(params object[] parameters)
        {
            // do nothing as this validator does not require initialisation!
        }


    }

}
