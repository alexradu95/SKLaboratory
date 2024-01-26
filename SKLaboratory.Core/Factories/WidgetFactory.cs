using SKLaboratory.Core.Services;
using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Services;

public class WidgetFactory : IWidgetFactory
{
    private readonly Dictionary<Type, IWidgetCreator> _widgetCreators = new();
    private readonly MessageBus _messageBus;

    public WidgetFactory(MessageBus messageBus) => _messageBus = messageBus;

    public IReadOnlyList<Type> RegisteredWidgetTypes => _widgetCreators.Keys.ToList();

    public IWidget CreateWidget(Type widgetType)
    {
        if (!_widgetCreators.TryGetValue(widgetType, out var creator))
        {
            throw new UnknownWidgetTypeException($"Widget type not registered: {widgetType}");
        }

        return creator.CreateWidget();
    }

    public void RegisterWidget<T>(IWidgetCreator creator) where T : IWidget
    {
        Type widgetType = typeof(T);
        ValidateWidgetType(widgetType);
        _widgetCreators[widgetType] = creator;
    }

    private void ValidateWidgetType(Type widgetType)
    {
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
            throw new ArgumentException("Type must implement IWidget", nameof(widgetType));

        if (_widgetCreators.ContainsKey(widgetType))
            throw new ArgumentException("Widget type already registered", nameof(widgetType));
    }

    private IWidget AttemptToCreateWidget(Func<IWidget> createWidget, Type widgetType)
    {
        try
        {
            return createWidget();
        }
        catch (Exception ex)
        {
            throw new WidgetCreationFailedException($"Error creating widget: {widgetType}", ex);
        }
    }

    private IWidget InstantiateWidget<T>() where T : IWidget
    {
        var constructorWithMessageBus = typeof(T).GetConstructor(new[] { typeof(MessageBus) });
        return constructorWithMessageBus != null
            ? (IWidget)constructorWithMessageBus.Invoke(new object[] { _messageBus })
            : Activator.CreateInstance<T>();
    }
}
