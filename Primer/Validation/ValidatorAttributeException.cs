// Copyright (c) James Dingle

using System;

namespace Primer
{
    /// <summary>
    /// The exception that is thrown when an error occours within a <see cref="Primer.Validation.ValidatorAttribute"/>.
    /// </summary>
    public class ValidatorAttributeException : ApplicationException
    {

        public ValidatorAttributeException(string message) : base(message) { }

        public ValidatorAttributeException(string message, Exception innerEx) : base(message, innerEx) { }

    }
}
