//-----------------------------------------------------------------------
// <copyright file="MessagingChannel.cs" company="James Dingle">
//     Copyright (c) James Dingle
// </copyright>
//-----------------------------------------------------------------------

namespace Primer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class MessagingChannel : IMessagingChannel
    {
        private IDictionary<Type, IList<Action<IMessage>>> Handlers;

        /// <summary>
        /// Occurs when a message has been broadcast on this channel.
        /// </summary>
        public event Action<IMessage> MessageBroadcast;

        /// <summary>
        /// Default constructor. Creates a new instance of <see cref="Primer.MessagingChannel"/>.
        /// </summary>
        public MessagingChannel()
        {
            this.Handlers = new Dictionary<Type, IList<Action<IMessage>>>();
            this.MessageBroadcast += HandleBroadcast;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
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

        /// <summary>
        /// Broadcasts a message to anyone who is listening to the channel.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        public void Broadcast(IMessage message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        public void Listen<T>(Action<T> messageHandler) where T : IMessage
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        /// <param name="filter"></param>
        public void Listen<T>(Func<T, bool> filter, Action<T> messageHandler) where T : IMessage
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listens to the channel for a particular message type and provides options for when and how to execute a delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        public IMessageHandlerBuilder<T> Listen<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops listening to the channel for a particular message type. Removes all message handler delegates for the specified type, so that when a message of that type is broadcast nothing will happen.
        /// </summary>
        /// <typeparam name="T">The type of message to ingore.</typeparam>
        public void Ignore<T>() where T : IMessage
        {
            throw new NotImplementedException();
        }
    }
}
