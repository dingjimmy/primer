// Copyright (c) James Dingle

using System;

namespace Primer
{

    /// <summary>
    /// A communication that is sent between two or more ViewModels.
    /// </summary>
    public class MessageBase : IMessage
    {


        /// <summary>
        /// The class from which the message originated from; usually a ViewModel.
        /// </summary>
        public Object Sender { get; set; }



        /// <summary>
        /// The time and date that the message was broadcast.
        /// </summary>
        public DateTime BroadcastOn { get; set; }



        // Primary Constructor
        public MessageBase()
        {
            BroadcastOn = DateTime.Now;
        }


    }
}