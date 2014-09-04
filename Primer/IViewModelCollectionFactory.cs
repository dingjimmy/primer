// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public interface IViewModelCollectionFactory
    {
        ViewModelCollection<T> Of<T, TSource>(IEnumerable<TSource> sourceItems, Action<ViewModel<T>, TSource> loadData) where T : ViewModel<T>;
        ViewModelCollection<T> Of<T, TSource>(IEnumerable<TSource> sourceItems, IMessagingChannel channel, Action<ViewModel<T>, TSource> loadData) where T : ViewModel<T>;
    }
}
