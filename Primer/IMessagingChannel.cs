// Copyright (c) James Dingle

namespace Primer
{
    using System;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public interface IMessagingChannel
    {
        /// <summary>
        /// Broadcasts a message to anyone who is listening to the channel.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        void Broadcast(IMessage message);

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        void Listen<T>(Action<T> messageHandler) where T : IMessage;

        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to invoke when a message of the desired type is broadcast.</param>
        /// <param name="criteria">An expression that provides additional criteria to control when the messageHander is to be executed.</param>
        void Listen<T>(Func<T, bool> criteria, Action<T> messageHandler) where T : IMessage;

        /// <summary>
        /// Listens to the channel for a particular message type and provides options for when and how to execute a delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        IMessageHandlerBuilder<T> Listen<T>();

        /// <summary>
        /// Stops listening to the channel for a particular message type. Removes all message handler delegates for the specified type, so that when a message of that type is broadcast nothing will happen.
        /// </summary>
        /// <typeparam name="T">The type of message to ingore.</typeparam>
        void Ignore<T>() where T : IMessage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageHandlerBuilder<T>
    {
        IMessageHandlerBuilder<T> AndInvoke(Action<T> messageHandler);

        void When(Func<T,bool> condition);

        void Always();
    }
}
