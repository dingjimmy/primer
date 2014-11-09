// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Primer
{
    public interface IValidator
    {

        Dictionary<string, string> Errors { get; set; }

        bool Validate(string propertyName);
        bool Validate<T>(Expression<Func<T>> property);
        void Validate(params string[] properties);

        bool HasErrors { get; set; }

        bool InError(string propertyName);
        bool InError<T>(Expression<Func<T>> property);
        bool InError(params string[] properties);

        void SetError(string propertyName, string errorMessage);
        void ClearError(string propertyName);
        void ClearError(params string[] properties);
        void ClearAllErrors();

    }
}
