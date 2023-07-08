using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;

public class WidgetFactory : IWidgetFactory
{
    private readonly Dictionary<Type, Func<IWidget>> _widgetCreators = new Dictionary<Type, Func<IWidget>>();

    public IWidget CreateWidget(Type widgetType)
    {
        if (_widgetCreators.TryGetValue(widgetType, out var createWidgetFunc))
        {
            return createWidgetFunc();
        }

        throw new UnknownWidgetTypeException($"Unknown widget type: {widgetType}");
    }

    public void RegisterWidget(Type widgetType)
    {
        // Check if the type is a subclass of IWidget
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
        {
            throw new ArgumentException($"Type must be a subclass of IWidget, but was {widgetType}", nameof(widgetType));
        }

        // Create a function that creates a new instance of the widget
        Func<IWidget> createWidgetFunc = () => (IWidget)Activator.CreateInstance(widgetType);

        // Register the widget with the factory
        if (_widgetCreators.ContainsKey(widgetType))
        {
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));
        }

        _widgetCreators[widgetType] = createWidgetFunc;
    }
}