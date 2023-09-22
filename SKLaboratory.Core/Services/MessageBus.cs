namespace SKLaboratory.Infrastructure.Services
{
    /// <summary>
    /// A simple message bus for pub-sub communication.
    /// </summary>
    public class MessageBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Publish<TMessage>(TMessage message)
        {
            var messageType = typeof(TMessage);
            if (_subscribers.TryGetValue(messageType, out var subscribers))
            {
                foreach (Action<TMessage> subscriber in subscribers)
                {
                    subscriber(message);
                }
            }
        }

        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (!_subscribers.TryGetValue(messageType, out var subscribers))
            {
                subscribers = new List<Delegate>();
                _subscribers[messageType] = subscribers;
            }

            subscribers.Add(action);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (_subscribers.TryGetValue(messageType, out var subscribers))
            {
                subscribers.Remove(action);
                if (subscribers.Count == 0)
                {
                    _subscribers.Remove(messageType);
                }
            }
        }
    }
}