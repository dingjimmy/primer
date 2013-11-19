// Copyright (c) James Dingle

namespace Primer.Validation
{
    
    public class NullValueValidatorAttribute : ValidatorAttribute
    {
        public override void Validate(object value)
        {

            // clear any errors
            SetState(true, string.Empty);

            // check value is not null
            if (value == null) SetState(false, "This field cannot be empty. Please enter a value.");

        }

    }

}
