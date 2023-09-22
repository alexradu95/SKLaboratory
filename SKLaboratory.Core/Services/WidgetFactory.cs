using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Services;

public class WidgetFactory : IWidgetFactory
{
    private readonly Dictionary<Type, Func<IWidget>> _widgetCreators = new();
    public List<Type> WidgetTypes => _widgetCreators.Keys.ToList();

    private readonly MessageBus _messageBus;

    public WidgetFactory(MessageBus messageBus) => _messageBus = messageBus;

    public IWidget CreateWidget(Type widgetType)
    {
        if (_widgetCreators.TryGetValue(widgetType, out var createWidgetFunc))
        {
            try
            {
                return createWidgetFunc();
            }
            catch (Exception ex)
            {
                throw new WidgetCreationFailedException($"Failed to create widget of type {widgetType}.", ex);
            }
        }

        throw new UnknownWidgetTypeException($"No widget registered for type {widgetType}.");
    }
    public void RegisterWidget<T>() where T : IWidget
    {
        Type widgetType = typeof(T);
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
            throw new ArgumentException($"Type must be a subclass of IWidget, but was {widgetType}", nameof(widgetType));

        Func<IWidget> createWidgetFunc = () =>
        {
            var constructorWithMessageBus = widgetType.GetConstructor(new[] { typeof(MessageBus) });
            if (constructorWithMessageBus != null)
            {
                return (IWidget)constructorWithMessageBus.Invoke(new object[] { _messageBus });
            }

            var defaultConstructor = widgetType.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
            {
                return (IWidget)defaultConstructor.Invoke(null);
            }

            throw new InvalidOperationException($"Type must have a default constructor.");
        };

        if (_widgetCreators.ContainsKey(widgetType))
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));

        _widgetCreators[widgetType] = createWidgetFunc;
    }
}
