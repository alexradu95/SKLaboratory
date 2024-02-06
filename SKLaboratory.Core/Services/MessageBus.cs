namespace SKLaboratory.Core.Services;
public class MessageBus
{
    private readonly Dictionary<Type, List<Delegate>?> _subscribers = new();

    public void Publish<TMessage>(TMessage message)
    {
        var subscribers = TryGetSubscribers<TMessage>();
        if (subscribers == null) return;
        foreach (var subscriber in subscribers)
        {
            ((Action<TMessage>)subscriber).Invoke(message);
        }
    }

    public void Subscribe<TMessage>(Action<TMessage>? action)
    {
        if (action == null) return;

        var messageType = typeof(TMessage);
        var subscribers = AddSubscriberForMessageType(messageType);
        subscribers.Add(action);
    }

    public void Unsubscribe<TMessage>(Action<TMessage>? action)
    {
        if (action == null) return;

        var messageType = typeof(TMessage);
        if (!_subscribers.TryGetValue(messageType, out var subscribers))
        {
            return;
        }

        subscribers?.RemoveAll(sub => sub.Target != null && sub.Target.Equals(action.Target) && sub.Method.Equals(action.Method));
    }

    private List<Delegate?> AddSubscriberForMessageType(Type messageType)
    {
        if (_subscribers.TryGetValue(messageType, out var subscribers)) return subscribers;
        subscribers = new List<Delegate>();
        _subscribers[messageType] = subscribers;
        return subscribers;
    }

    private IEnumerable<Delegate>? TryGetSubscribers<TMessage>()
    {
        _subscribers.TryGetValue(typeof(TMessage), out var subscribers);
        return subscribers;
    }
}