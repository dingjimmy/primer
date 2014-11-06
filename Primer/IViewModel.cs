// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModel
    {
        IMessagingChannel Channel;
        IValidator Validator;
    }
}
