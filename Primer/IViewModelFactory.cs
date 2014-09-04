// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModelFactory
    {
        ViewModel<T> Of<T>();

        ViewModel<T> Of<T>(Func<T> loadDataAction);

        ViewModel<T> Of<T>(IMessagingChannel channel);

        ViewModel<T> Of<T>(IMessagingChannel channel, Func<T> loadDataAction);
    }
}
