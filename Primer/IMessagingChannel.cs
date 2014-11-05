// Copyright (c) James Dingle

using System;
using System.Linq;

namespace Primer
{
    public interface IMessagingChannel
    {


        /// <summary>
        /// Occurs when a message has been broadcast on this channel.
        /// </summary>
        event Action<IMessage> MessageBroadcast;



        /// <summary>
        /// Broadcasts a message to anyone who is listening to the channel.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        void Broadcast(IMessage message);



        /// <summary>
        /// Listens to the channel for a particular message type. When a message of that type is broadcast the message handler delegate is executed.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to trigger when a message of the desired type is broadcast.</param>
        void Listen<T>(Action<IMessage> messageHandler) where T : IMessage;



        /// <summary>
        /// Listens to the channel for a particular message type and executes the provided delegate.
        /// </summary>
        /// <typeparam name="T">The type of message to listen out for.</typeparam>
        /// <param name="messageHandler">The delegate to execute when a message of the desired type is broadcast.</param>
        void Listen<T>(Action<T> messageHandler) where T : IMessage;



        /// <summary>
        /// Stops listening to the channel for a particular message type. Removes all message handler delegates for the specified type, so that when a message of that type is broadcast nothing will happen.
        /// </summary>
        /// <typeparam name="T">The type of message to ingore.</typeparam>
        void Ignore<T>() where T : IMessage;


    }
}
