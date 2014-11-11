// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModel
    {
        bool IsLoaded { get; set; }
        string DisplayName { get; set; }
        IMessagingChannel Channel { get; private set; }
        IValidator Validator { get; private set; }
        ILogger Logger { get; private set; }
    }
}
