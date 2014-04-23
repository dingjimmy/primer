// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Primer
{
    public class ViewModelCollection<T> : ObservableCollection<T>
    {
    }

    public class ViewModelCollection : ObservableCollection<ViewModel>
    {
    }
}
