// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Primer
{
    public class ViewModelCollection<TViewModel> : ObservableCollection<TViewModel>
    {
    }

    public class ViewModelCollection : ObservableCollection<IViewModel>
    {
    }
}
