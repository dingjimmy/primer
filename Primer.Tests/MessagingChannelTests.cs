// Copyright (c) James Dingle

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Primer.Messages;

namespace Primer.Tests
{

    [TestClass]
    public class MessagingChannelTests
    {

        [TestMethod]
        [TestCategory("Primer.MessagingChannel")]
        public void Broadcast_Raises_MessageBroadcast_Event()
        {

            // Arrange
            var msg = Mock.Of<IMessage>();
            var channel = new MessagingChannel();
            var eventRaised = false;
            channel.MessageBroadcast += (m) => { eventRaised = true; };


            // Action
            channel.Broadcast(msg);


            // Assert
            Assert.IsTrue(eventRaised, "The 'MessageBroadcast' event has not been raised.");

        }



        [TestMethod]
        [TestCategory("Primer.MessagingChannel")]
        public void Listen_Adds_Message_Handler_Delegate_To_Lookup()
        {

            // Arrange
            var channel = new MessagingChannel();


            // Action
            channel.Listen<FieldChanged>((m) => { });


            // Assert
            Assert.IsTrue(channel.Handlers.ContainsKey(typeof(FieldChanged)), "No handlers for FieldChanged messages have been added.");
            Assert.IsTrue(channel.Handlers[typeof(FieldChanged)].Count == 1, "Too many handlers for FieldChanged messages have been added.");

        }



        [TestMethod]
        [TestCategory("Primer.MessagingChannel")]
        public void Raising_MessageBroadcast_Event_Executes_Handlers_For_Specified_Message()
        {

            // Arrange           
            var handlerExecuted = false;
            Action<IMessage>[] handlers = { (m) => { handlerExecuted = true; } };

            var channel =  Mock.Of<MessagingChannel>();
            channel.Handlers.Add(typeof(FieldChanged), handlers.ToList() );
            Mock.Get(channel).CallBase = true;


            // Action
            Mock.Get(channel).Raise(c => c.MessageBroadcast += null, new FieldChanged());


            // Assert
            Assert.IsTrue(handlerExecuted, "The messageHandler delegate has not been executed.");

        }


    }
}
