//// Copyright (c) James Dingle

//using System;

//namespace Primer.Validation
//{

//    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
//    public abstract class ValidatorAttribute<T>: ValidatorAttribute
//    {


//        /// <summary>
//        /// Validates the value against a pre-determned set of rules.
//        /// </summary>
//        /// <param name="value"></param>
//        public abstract void Validate(Field<T> value);



//        /// <summary>
//        /// Validates the value against a pre-determned set of rules.
//        /// </summary>
//        /// <param name="value"></param>
//        public override void Validate<T>(Field<T> value)
//        {
//            var field = value as Field<T>;

//            if (field != null)
//            {
//                Validate(field);
//            }
//            else
//            {
//                throw new ValidatorAttributeException(string.Format("Invalid field type. This '{0}' should only be assigned to a Field<{1}>", this.GetType().ToString(), typeof(T).ToString()));
//            }
//        }



//        /// <summary>
//        /// Initialise the validator with the provided parameters.
//        /// </summary>
//        public abstract void Initialise(params T[] parameters);



//        /// <summary>
//        /// Initialise the validator with the provided parameters.
//        /// </summary>
//        override void ValidatorAttribute.Initialise(params object[] parameters)
//        {

//            // if no parameters provieded, do nothing further.
//            if (parameters == null) return;


//            var prams = parameters as T[];

//            if (prams != null)
//            {
//                Initialise(prams);
//            }
//            else
//            {
//                throw new ValidatorAttributeException(string.Format("Invalid parameter type. This '{0}' should only be initialised with {1}s.", this.GetType().ToString(), typeof(T).ToString()));
//            }
//        }

//    }

//}
