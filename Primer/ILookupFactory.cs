// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public interface ILookupFactory
    {
        Lookup<T> From<T>(IEnumerable<T> source, Func<T> key, Func<T> description, Func<T> entity);
    }
}
