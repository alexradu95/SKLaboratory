namespace SKLaboratory.Core.Services;

public class MessageBus
{
	private readonly Dictionary<Type, List<Action<object>>> _subscribers = new();

	public void Publish<TMessage>(TMessage message)
	{
		if (_subscribers.TryGetValue(typeof(TMessage), out var subscribers))
			foreach (var subscriber in subscribers)
				subscriber(message);
	}

	public void Subscribe<TMessage>(Action<TMessage> action)
	{
		var messageType = typeof(TMessage);
		if (!_subscribers.TryGetValue(messageType, out var subscribers))
		{
			subscribers = new List<Action<object>>();
			_subscribers[messageType] = subscribers;
		}

		subscribers.Add(msg => action((TMessage)msg));
	}

	public void Unsubscribe<TMessage>(Action<TMessage> action)
	{
		if (_subscribers.TryGetValue(typeof(TMessage), out var subscribers))
			subscribers.RemoveAll(sub => sub.Target.Equals(action.Target) && sub.Method.Equals(action.Method));
	}
}