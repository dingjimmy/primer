// Copyright (c) James Dingle

using System;

namespace Primer.Validation
{

    public interface IValidationTarget
    {
        object ValueToValidate { get; }

        Type TypeToValidate { get; }
    }



    //public interface IValidationTarget<T>
    //{
    //    T ValueToValidate { get; }
    //}

}
