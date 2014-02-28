// Copyright (c) James Dingle

using System;

namespace Primer.SmartProperties
{


    /// <summary>
    /// 
    /// </summary>
    public class DataPropertyInitialiser
    {

        ViewModel _TargetViewModel;

        public DataPropertyInitialiser(ViewModel targetViewModel)
        {
            if (targetViewModel != null)
                _TargetViewModel = targetViewModel;
            else
                throw new ArgumentNullException("targetViewModel");
        }

        public DataPropertyInitialiser<T> Initialise<T>(string name)
        {
            return new DataPropertyInitialiser<T>(name, _TargetViewModel);
        }

    }



    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataPropertyInitialiser<T>
    {


        private string _PropertyName;
        private ViewModel _ParentViewModel;
        private bool _IsPropertyReadOnly;



        /// <summary>
        /// Public constructor
        /// </summary>
        public DataPropertyInitialiser(string name, ViewModel parentViewModel)
        {
            _PropertyName = name;
            _ParentViewModel = parentViewModel;
        }



        /// <summary>
        /// Set the initial value to the default value of the underlying type. 
        /// </summary>
        public DataProperty<T> WithDefaultValue()
        {
            return new DataProperty<T>(_PropertyName, default(T), _ParentViewModel);
        }



        /// <summary>
        /// Sets the initial value. 
        /// </summary>
        public DataProperty<T> WithValue(T value)
        {
            return new DataProperty<T>(_PropertyName, value, _ParentViewModel);
        }



        /// <summary>
        /// Sets an initial value for the DataProperty from it's string representation.
        /// </summary>
        public DataProperty<T> WithValue(string value)
        {

            object convertedValue = default(T);
            var type = typeof(T);

            try
            {

                // Convert input string to required type. If type not supported then throw exception.
                if (type == typeof(string))
                {
                    convertedValue = value;
                }
                else if (type == typeof(DateTime))
                {
                    convertedValue = Convert.ToDateTime(value);
                }
                else if (type == typeof(DateTime?))
                {
                    if (value != null)
                    {
                        convertedValue = new DateTime?(Convert.ToDateTime(value));
                    }

                }
                else if (type == typeof(Int32))
                {
                    convertedValue = Convert.ToInt32(value);
                }
                else
                {
                    throw new NotSupportedException(String.Format("Converstion of string to {0} is not currently supported", type.Name));
                }


                // return new DataProperty with initial value
                return new DataProperty<T>(_PropertyName, (T)convertedValue, _ParentViewModel);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Cannot initialise data property '{0}' with value '{1}'. See inner exception for more information", _PropertyName, value), ex);
            }
        }

    }

}
