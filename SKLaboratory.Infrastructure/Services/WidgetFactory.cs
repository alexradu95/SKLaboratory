using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;

namespace SKLaboratory.Infrastructure.Services;

public class WidgetFactory : IWidgetFactory
{
    private readonly Dictionary<Type, Func<IWidget>> _widgetCreators = new();

    public List<Type> WidgetTypes => _widgetCreators.Keys.ToList();

    private readonly MessageBus _messageBus;

    public WidgetFactory(MessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public IWidget CreateWidget(Type widgetType)
    {
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
        {
            throw new ArgumentException($"Type must implement {nameof(IWidget)}.", nameof(widgetType));
        }

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
    }


    public void RegisterWidget<T>() where T : IWidget
    {
        Type widgetType = typeof(T);
        // Check if the provided type is a subclass of IWidget
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
            // If not, throw an exception indicating that the type must be a subclass of IWidget
            throw new ArgumentException($"Type must be a subclass of IWidget, but was {widgetType}",
                nameof(widgetType));

        // Define a function that creates an instance of the provided type
        Func<IWidget> createWidgetFunc = () => (IWidget) Activator.CreateInstance(widgetType);

        // Check if a widget of the provided type is already registered
        if (_widgetCreators.ContainsKey(widgetType))
            // If so, throw an exception indicating that a widget of this type is already registered
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));

        // If the type is a valid subclass of IWidget and is not already registered,
        // add the createWidgetFunc to the _widgetCreators dictionary with the widgetType as the key
        _widgetCreators[widgetType] = createWidgetFunc;
    }
}