// Copyright (c) James Dingle

using System;
using System.Linq;
using System.Collections.Generic;

namespace Primer
{
    public class MessagingChannel : IMessagingChannel
    {

        public virtual IDictionary<Type, IList<Action<IMessage>>> Handlers { get; protected set; }


        private void HandleBroadcast(IMessage msg)
        {

            var key = msg.GetType();


            if (Handlers.ContainsKey(key))
            {

                foreach (var messageHandler in Handlers[key])
                {
                    messageHandler(msg);
                }

            }   
      
        }


        #region Constructors


        public MessagingChannel()
        {

            Handlers = new Dictionary<Type, IList<Action<IMessage>>>();

            this.MessageBroadcast += HandleBroadcast;

        }


        #endregion


        #region IMessageChannel Implementation


        /// <summary>
        /// Occurs when a message has been broadcast on this channel.
        /// </summary>
        public virtual event Action<IMessage> MessageBroadcast;



        /// <summary>
        /// Broadcasts a message to anyone who is listening to the channel.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        public void Broadcast(IMessage message)
        {

             // to avoid race conditions, we get a reference to any event-handlers befor invoking
            var mb = MessageBroadcast;

            // invoke the event
            if (mb != null)
            {
                mb(message);  
            }

        }



        /// <summary>
        /// Listens to the channel for a particular message type. When a message of that type is broadcast the message handler delegate is executed.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to trigger when a message of the desired type is broadcast.</param>
        public void Listen<T>(Action<IMessage> messageHandler) where T : IMessage
        {

            var type = typeof(T);

            if (Handlers.ContainsKey(type))
            {
                Handlers[type].Add(messageHandler);
            }
            else
            {
                var list = new List<Action<IMessage>>();
                list.Add(messageHandler);
                Handlers.Add(type, list);
            }

        }


        #endregion


    }
}
