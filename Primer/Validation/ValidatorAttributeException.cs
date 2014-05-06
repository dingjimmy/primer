// Copyright (c) James Dingle

using System;

namespace Primer
{
    public class ValidatorAttributeException : ApplicationException
    {

        public ValidatorAttributeException(string message) : base(message) { }

        public ValidatorAttributeException(string message, Exception innerEx) : base(message, innerEx) { }

    }
}
