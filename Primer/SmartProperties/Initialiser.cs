// Copyright (c) James Dingle

using System;

namespace Primer.SmartProperties
{

    /// <summary>
    /// 
    /// </summary>
    public class Init
    {

        #region Generic Factory Methods


        public static Data<T> Data<T>(ViewModel vm, string name, T value)
        {
            return new Data<T> { ViewModel=vm, Name = name, Value = value };
        }



        public static Data<Nullable<T>> NullableData<T>(ViewModel vm, string name, T value) where T : struct
        {
            return new Data<Nullable<T>> { ViewModel = vm, Name = name, Value = value };
        }


        #endregion


        #region DateTime Factory Methods


        public static Data<DateTime> InitDate(ViewModel vm, string name, string value)
        {
            return new Data<DateTime> { ViewModel = vm, Name = name, Value = DateTime.Parse(value) };
        }


        public static Data<DateTime?> InitNullableDate(ViewModel vm, string name, string value)
        {
            var dt = string.IsNullOrWhiteSpace(value) ? (DateTime?)null : DateTime.Parse(value);
            return new Data<DateTime?> { ViewModel=vm, Name = name, Value = dt };
        }


        #endregion

    }

}
