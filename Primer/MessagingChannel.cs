//-----------------------------------------------------------------------
// <copyright file="MessagingChannel.cs" company="James Dingle">
//     Copyright (c) James Dingle
// </copyright>
//-----------------------------------------------------------------------

namespace Primer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class MessagingChannel : IMessagingChannel
    {
        /// <summary>
        /// Gets the message-handlers waiting to be invoked by a message broadcast.
        /// </summary>
        public IDictionary<Type, IList<Action<IMessage>>> Handlers { get; private set; }

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
            this.MessageBroadcast += ExecuteMessageHandlers;
        }

        private void ExecuteMessageHandlers(IMessage msg)
        {
            var key = msg.GetType();

            if (this.Handlers.ContainsKey(key))
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
            var mb = MessageBroadcast;
            
            if (mb != null)
            {
                mb(message);
            }
        }

        private void AddMessageHandler<T>(Action<IMessage> messageHandler) where T : IMessage
        {
            var key = typeof(T);

            if (this.Handlers.ContainsKey(key))
            {
                this.Handlers[key].Add(messageHandler);
            }
            else
            {
                var list = new List<Action<IMessage>>();
                list.Add(messageHandler);
                this.Handlers.Add(key, list);
            }
        }

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        /// <remarks>
        /// We've made the 'messageHander' delegate generic to provide a simpler syntax; otherwise use of this method would require an explicit cast to IMessage.
        /// To get this to work, we wrap the provided generic Action[T] delegete inside an Action[IMessage] delegete such that we can add it to the handlers 
        /// dictionary.
        /// </remarks>
        public void Listen<T>(Action<T> messageHandler) where T : IMessage
        {
            Action<IMessage> wrapper = (m) =>
            {
                messageHandler((T)m);
            };

            AddMessageHandler<T>(wrapper);
        }

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        /// <param name="criteria">An expression that provides additional criteria to control when the messageHander is to be executed.</param>
        /// <remarks>
        /// On this occasion we are using a wrapper delegate to ensure that the messageHandler is only triggered when any message meets a set criteria, in addition
        /// to making it compatable with the handlers dictionary.
        /// </remarks>
        public void Listen<T>(Func<T, bool> criteria, Action<T> messageHandler) where T : IMessage
        {        
            Action<IMessage> wrapper = (m) =>
            {
                if (criteria((T)m) == true)
                {
                    messageHandler((T)m);
                }
            };

            AddMessageHandler<T>(wrapper);
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
            var key = typeof(T);

            if (Handlers.ContainsKey(key))
            {
                Handlers[key].Clear();
                Handlers.Remove(key);
            }
        }
    }
}
