// Copyright (c) James Dingle

using System;

namespace Primer.Messages
{

    /// <summary>
    /// A communication that indicates that a ViewModel Command has been executed.
    /// </summary>
    public class CommandExecuted : Message
    {


        /// <summary>
        /// The name of the command that has been executed.
        /// </summary>
        public string Name { get; set; }


    }
}
