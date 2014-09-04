// Copyright (c) James Dingle

using System;
using System.Collections.Generic;

namespace Primer
{
    public class ViewModelHelper
    {
        public ILookupFactory Lookup { get; private set; }
        public IViewModelFactory ViewModel { get; private set; }
        public IViewModelCollectionFactory ViewModelCollection { get; private set; }

        public ViewModelHelper(ILookupFactory lookupFactory, IViewModelFactory vmFactory, IViewModelCollectionFactory vmCollectionFactory)
        {
            this.Lookup = lookupFactory;
            this.ViewModel = vmFactory;
            this.ViewModelCollection = vmCollectionFactory;
        }
    }
}
