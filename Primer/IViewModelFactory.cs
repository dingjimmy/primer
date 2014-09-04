// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModelFactory
    {
        ViewModel<T> Of<T>();

        ViewModel<T> Of<T>(Func<T> loadDataAction);

        ViewModel<T> Of<T>(IViewModel parentViewModel);

        ViewModel<T> Of<T>(IViewModel parentViewModel, Func<T> loadDataAction);
    }
}
