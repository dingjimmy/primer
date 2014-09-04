// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public interface ILookupFactory
    {
        Lookup<T> From<T>(IEnumerable<T> sourceCollection, Func<T, string> key, Func<T, string> description, Func<T, T> value);
    }
}
