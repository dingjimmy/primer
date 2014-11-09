// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModel
    {
        bool IsLoaded { get; set; }
        string DisplayName { get; set; }
        IMessagingChannel Channel { get; set; }
        IValidator Validator { get; set; }
    }
}
