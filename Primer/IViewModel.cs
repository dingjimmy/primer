using System;

namespace Primer
{
    public interface IViewModel
    {

        bool IsLoaded { get; set; }

        IMessagingChannel Channel { get; set; }

        [Obsolete]
        void Initialise(params object[] dataSources);

        bool UpdateProperty<T>(string propertyName, ref T currentValue, T proposedValue, bool forceUpdate);

        void Broadcast(IMessage message);

        void Listen<T>(Action<T> messageHandler) where T : IMessage;

    }
}
