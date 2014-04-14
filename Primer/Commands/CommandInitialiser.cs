// Copyright (c) James Dingle

using System;

namespace Primer.SmartProperties
{
    public class CommandInitialiser
    {
        ViewModel _TargetViewModel;

        public CommandInitialiser(ViewModel targetViewModel)
        {
            if (targetViewModel != null)
                _TargetViewModel = targetViewModel;
            else
                throw new ArgumentNullException("targetViewModel");
        }

        public FieldInitialiser<T> Initialise<T>(string name)
        {
            throw new NotImplementedException();
            //return new ActionPropertyInitialiser<T>(name, _TargetViewModel);
        }
    }
}
