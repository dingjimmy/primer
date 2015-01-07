using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Primer.Tests
{
    [TestClass]
    public class MessagingChannelTests
    {
        [TestMethod]
        [TestCategory("MessagingChannel")]
        public void Listen_UsingMessageHandler_AddsHandlerToDictionary()
        {
            // Arrange
            var channel = new MessagingChannel();           

            // Action
            channel.Listen<DummyMessage>(m => Assert.Fail("This placeholder method should not have been invoked!!"));

            // Assert
            var type = typeof(DummyMessage);
            Assert.IsTrue(channel.Handlers.ContainsKey(type));

            var dummyHanders = channel.Handlers[type];
            Assert.AreEqual(1, dummyHanders.Count);
            
        }

        [TestMethod]
        [TestCategory("MessagingChannel")]
        public void Listen_UsingMessageHandlerAndCriteria_AddsHandlerToDictionary()
        {
            // Arrange
            var channel = new MessagingChannel();

            // Action
            channel.Listen<DummyMessage>(m => true, m => Assert.Fail("This placeholder method should not have been invoked!!"));

            // Assert
            var type = typeof(DummyMessage);
            Assert.IsTrue(channel.Handlers.ContainsKey(type));

            var dummyHanders = channel.Handlers[type];
            Assert.AreEqual(1, dummyHanders.Count);
        }

        [TestMethod]
        [TestCategory("MessagingChannel")]
        [ExpectedException(typeof(NotImplementedException))]
        public void Listen_ReturnsFluentMessageHandlerBuilder()
        {
            // NOTE: THE METHOD ON TEST HAS NOT YET BEEN IMPLEMENTED.

            // Arrange
            var channel = new MessagingChannel();

            // Action
            var builder = channel.Listen<DummyMessage>();

            // Assert - Expecting NotImplementedException
        }

        [TestMethod]
        [TestCategory("MessagingChannel")]
        public void Broadcast_RaisesMessageBroadcastEvent()
        {
            // NOTE: NOT SURE IF THIS NECCASSARY; ARE WE TESTING AN IMPLEMENTATION DETAIL??

            // Arrange
            var channel = new MessagingChannel();
            var message = Mock.Of<IMessage>();
            var eventRaised = false;
            channel.MessageBroadcast += (m) => eventRaised = true;

            // Action
            channel.Broadcast(message);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        [TestCategory("MessagingChannel")]
        public void Broadcast_MessageType_InvokesMessageHandlersForSameMessageType()
        {         
            // Arrange
            var channel = new MessagingChannel();
            var message = new PropertyChangedMessage() { Name = "TestProperty", Sender = this, BroadcastOn = DateTime.Now };
            var hasBroadcast = false;
            channel.Listen<PropertyChangedMessage>(m => {
                hasBroadcast = true;
                Assert.IsInstanceOfType(m, typeof(PropertyChangedMessage));
                Assert.AreEqual("TestProperty", m.Name);
                Assert.AreEqual(this, m.Sender);
            });

            // Action
            channel.Broadcast(message);

            // Assert
            Assert.IsTrue(hasBroadcast);
        }

        [TestMethod]
        [TestCategory("MessagingChannel")]
        public void Broadcast_MessageType_DoesNotInvokeMessageHandlersOfOtherMessageTypes()
        {           
            // Arrange
            var channel = new MessagingChannel();
            var message = new PropertyChangedMessage() { Name = "TestProperty", Sender = this, BroadcastOn = DateTime.Now }; 
            var hasBroadcast = false;
            channel.Listen<DummyMessage>(m => {
                hasBroadcast = true; 
            });

            // Action
            channel.Broadcast(message);

            // Assert
            Assert.IsFalse(hasBroadcast);
        }
    }

    internal class DummyMessage : MessageBase
    {
        public DummyMessage()
        {
        }
    }
}
