using System;
using System.Collections.Generic;

namespace SKLaboratory.Infrastructure.Services
{
    /// <summary>
    /// A simple message bus for pub-sub communication.
    /// </summary>
    public class MessageBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        /// <summary>
        /// Publishes a message of the specified type to all subscribers.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message to publish.</param>
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

        /// <summary>
        /// Subscribes to messages of the specified type.
        /// </summary>
        /// <typeparam name="TMessage">The type of the messages to subscribe to.</typeparam>
        /// <param name="action">The action to perform when a message of the specified type is published.</param>
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

        /// <summary>
        /// Unsubscribes from messages of the specified type.
        /// </summary>
        /// <typeparam name="TMessage">The type of the messages to unsubscribe from.</typeparam>
        /// <param name="action">The action to remove from the subscribers list.</param>
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


/*

Title: Understanding the MessageBus Class in the SKLaboratory Project

The MessageBus class in the SKLaboratory project is a simple yet powerful implementation of the publish-subscribe (pub-sub) pattern. This pattern is a messaging system where senders (publishers) send messages without the knowledge of any receivers (subscribers). Similarly, subscribers listen for specific messages without knowing anything about the publishers. This decoupling of publishers and subscribers can lead to more flexible and scalable systems.

In the context of the SKLaboratory project, the MessageBus class is used to facilitate communication between different widgets. Widgets can publish messages when certain events occur, and other widgets can subscribe to these messages to react accordingly.

The MessageBus class uses a dictionary to map message types to a list of subscribers. Each subscriber is a delegate (a type-safe function pointer in C#) that takes a message of a specific type as a parameter. This allows multiple subscribers to listen for the same message type, which is a key feature of a pub-sub system.

The Publish method is used to send a message to all subscribers of a specific message type. It takes a message as a parameter and invokes all subscribers with this message.

The Subscribe method is used to register a subscriber for a specific message type. It takes an action (a delegate that takes a message as a parameter) and adds it to the list of subscribers for the specified message type. If no subscribers exist for this message type, a new list is created.

The Unsubscribe method is used to remove a subscriber for a specific message type. It takes an action and removes it from the list of subscribers for the specified message type. If no more subscribers exist for this message type, the list is removed from the dictionary.

In conclusion, the MessageBus class in the SKLaboratory project is a well-designed implementation of the pub-sub pattern. It provides a flexible and efficient way for widgets to communicate with each other without being tightly coupled. This can make the system easier to extend and maintain, as new widgets can be added or existing ones can be modified without affecting other parts of the system.


*/