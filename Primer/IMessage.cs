﻿// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer
{

    /// <summary>
    /// A communication that is sent between two or more ViewModels.
    /// </summary>
    public interface IMessage
    {


        /// <summary>
        /// The class from which the message originated from; usually a ViewModel.
        /// </summary>
        Object Sender { get; set; }



        /// <summary>
        /// The time and date that the message was broadcast.
        /// </summary>
        DateTime BroadcastOn { get; set; }

    }
}
