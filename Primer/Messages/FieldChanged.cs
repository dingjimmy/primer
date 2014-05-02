// Copyright (c) James Dingle

using System;

namespace Primer.Messages
{

    /// <summary>
    /// A communication that indicates that a ViewModel Field has been changed.
    /// </summary>
    public class FieldChanged : Message
    {


        /// <summary>
        /// The name of the Field that has changed.
        /// </summary>
        public string Name { get; set; }


    }
}
