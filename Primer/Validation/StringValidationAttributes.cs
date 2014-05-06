//// Copyright (c) James Dingle

//using System;
//using System.Text.RegularExpressions;

//namespace Primer.Validation
//{


//    /// <summary>
//    /// Checks for empty or whitespace-only strings. Returns NotValid if found, IsValid otherwise.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
//    class EmptyStringValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<string> field)
//        {
            
//            // clear error state
//            SetState(true, null);

//            // check for null
//            if (field != null)
//            {

//                    // check for empty string or whitespace
//                    if (string.IsNullOrWhiteSpace(field.Data))
//                    {
//                        SetState(false, "This field cannot be empty. Please enter a value.");
//                    }

//            }

//        }



//        public override void Initialise(params string[] parameters)
//        {
//            // do nothing as this validator does not require initialisation!
//        }

//    }



//    /// <summary>
//    /// Checks for empty or whitespace-only strings and if found converts them to a null value. Returns IsValid.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    public class EmptyStringConverterAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<string> field)
//        {

//            // clear error state
//            SetState(true, null);

//            // check for null
//            if (field != null)
//            {

//                // check for empty string or whitespace
//                if (string.IsNullOrWhiteSpace(field.Data))
//                {
//                    field.Data = null;
//                }

//            }

//        }



//        public override void Initialise(params string[] parameters)
//        {
//            // do nothing as this validator does not require initialisation!
//        }

//    }



//    /// <summary>
//    /// Checks the length of a string is within the supplied range. Returns IsValid if lenght is within range, NotValid otherwise.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    public class StringLengthValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<string> field)
//        {
//            throw new NotImplementedException();
//        }



//        public override void Initialise(params string[] parameters)
//        {
//            throw new NotImplementedException();
//        }

//    }



    
//    /// <summary>
//    /// Checks the contents of a string is only numbers and not letters or symbols.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    public class NumericStringValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<string> field)
//        {
//            throw new NotImplementedException();
//        }



//        public override void Initialise(params string[] parameters)
//        {
//            throw new NotImplementedException();
//        }

//    }



//    /// <summary>
//    /// Checks the contents of a string is a currency format.
//    /// </summary>
//    /// <remarks>
//    /// A valid currency format is an integral number (aka whole number) or a number that is exactly to two decimal places. Everything else is invalid.
//    /// </remarks>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    public class CurrencyStringValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<string> field)
//        {
//            throw new NotImplementedException();
//        }



//        public override void Initialise(params string[] parameters)
//        {
//            throw new NotImplementedException();
//        }

//    }



//    #region Not Yet Implemented
    

//    /// <summary>
//    /// Not Implemented
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    class EmailAddressValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<String> field)
//        {
//            throw new NotImplementedException();
//        }



//        public override void Initialise(params string[] parameters)
//        {
//            throw new NotImplementedException();
//        }

//    }




//    /// <summary>
//    /// Not Implemented.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
//    class PostCodeValidatorAttribute : ValidatorAttribute<string>
//    {

//        public override void Validate(Field<String> field)
//        {
//            throw new NotImplementedException();
//        }



//        public override void Initialise(params string[] parameters)
//        {
//            throw new NotImplementedException();
//        }

//    }


//#endregion


//}
