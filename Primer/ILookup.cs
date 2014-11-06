// Copyright (c) James Dingle

using System;
using System.Collections;

namespace Primer
{

    public interface ILookup<T>
    {
        ILookupItem<T> Add(string key, T entity, string description);
        void ApplyFilter(Func<ILookupItem<T>, bool> criteria);
        void ClearFilter();
    }


    public interface ILookupItem<T>
    {
        string Key;
        string Description;
        T Entity;
    }
}
