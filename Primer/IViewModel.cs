// Copyright (c) James Dingle

using System;

namespace Primer
{
    public interface IViewModel
    {
        public bool IsLoaded { get; set; }
        public string DisplayName { get; set; }
        IMessagingChannel Channel { get; set; }
        IValidator Validator { get; set; }
    }
}
