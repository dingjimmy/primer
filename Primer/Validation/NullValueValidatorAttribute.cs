// Copyright (c) James Dingle

using System;

namespace Primer.Validation
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class NullValueValidatorAttribute : ValidatorAttribute
    {

        public override void Validate<T>(Field<T> field)
        {

            // clear any errors
            SetState(true, string.Empty);

            // check value is not null
            if (field != null)
            {
 

            }
                
                
                
                SetState(false, "This field cannot be empty. Please enter a value.");

        }


        public override void Initialise(params object[] parameters)
        {
            // do nothing as this validator does not require initialisation!
        }

    }

}
