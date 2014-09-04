// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public interface IViewModelCollectionFactory
    {
        ViewModelCollection<T> Of<T, TSource>(IEnumerable<TSource> source, Func<T> loadDataAction);

        ViewModelCollection<T> Of<T, TSource>(IViewModel parentViewModel, IEnumerable<TSource> source, Func<T> loadDataAction);
    }
}
