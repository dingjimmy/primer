// Copyright (c) James Dingle

using System;

namespace Primer
{
    public class InitialiseViewModelException : ApplicationException
    {

        public InitialiseViewModelException(string message) : base(message) { }

        public InitialiseViewModelException(string message, Exception innerEx) : base(message, innerEx) { }

    }
}
