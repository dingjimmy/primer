// Copyright (c) James Dingle

using System;

namespace Primer.SmartProperties
{
    public class ActionPropertyInitialiser
    {
        ViewModel _TargetViewModel;

        public ActionPropertyInitialiser(ViewModel targetViewModel)
        {
            if (targetViewModel != null)
                _TargetViewModel = targetViewModel;
            else
                throw new ArgumentNullException("targetViewModel");
        }

        public DataPropertyInitialiser<T> Initialise<T>(string name)
        {
            throw new NotImplementedException();
            //return new ActionPropertyInitialiser<T>(name, _TargetViewModel);
        }
    }
}
