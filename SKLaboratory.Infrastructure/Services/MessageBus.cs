namespace SKLaboratory.Infrastructure.Services
{
    /// <summary>
    /// A simple message bus for pub-sub communication.
    /// </summary>
    public class MessageBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscribers = new();

        /// <summary>
        /// Publishes a message of the specified type to all subscribers.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message to publish.</param>
        public void Publish<TMessage>(TMessage message)
        {
            var messageType = typeof(TMessage);
            if (_subscribers.ContainsKey(messageType))
            {
                var subscribers = _subscribers[messageType];
                foreach (var subscriber in subscribers)
                {
                    subscriber(message);
                }
            }
        }

        /// <summary>
        /// Subscribes to messages of the specified type.
        /// </summary>
        /// <typeparam name="TMessage">The type of the messages to subscribe to.</typeparam>
        /// <param name="action">The action to perform when a message of the specified type is published.</param>
        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType] = new List<Action<object>>();
            }

            _subscribers[messageType].Add(x => action((TMessage)x));
        }

        /// <summary>
        /// Unsubscribes from messages of the specified type.
        /// </summary>
        /// <typeparam name="TMessage">The type of the messages to unsubscribe from.</typeparam>
        /// <param name="action">The action to remove from the subscribers list.</param>
        public void Unsubscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType].Remove(x => action((TMessage)x));
            }
        }
    }
}
