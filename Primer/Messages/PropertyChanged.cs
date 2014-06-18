// Copyright (c) James Dingle

using System;

namespace Primer.Messages
{

    /// <summary>
    /// A communication that indicates that a ViewModel or Facade property has been changed.
    /// </summary>
    public class PropertyChanged : Message
    {


        /// <summary>
        /// The name of the Property that has changed.
        /// </summary>
        public string Name { get; set; }


    }
}
