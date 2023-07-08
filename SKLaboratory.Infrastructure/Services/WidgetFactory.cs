using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;

public class WidgetFactory : IWidgetFactory
{
    private readonly Dictionary<Type, Func<IWidget>> _widgetCreators = new Dictionary<Type, Func<IWidget>>();

    public List<Type> WidgetTypes => _widgetCreators.Keys.ToList();

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
                throw new WidgetCreationFailedException($"Failed to create widget of type: {widgetType}", ex);
            }
        }

        throw new UnknownWidgetTypeException($"Unknown widget type: {widgetType}");
    }

    public void RegisterWidget<T>() where T : IWidget
    {
        var widgetType = typeof(T);
        // Check if the provided type is a subclass of IWidget
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
        {
            // If not, throw an exception indicating that the type must be a subclass of IWidget
            throw new ArgumentException($"Type must be a subclass of IWidget, but was {widgetType}", nameof(widgetType));
        }

        // Define a function that creates an instance of the provided type
        Func<IWidget> createWidgetFunc = () => (IWidget) Activator.CreateInstance(widgetType);

        // Check if a widget of the provided type is already registered
        if (_widgetCreators.ContainsKey(widgetType))
        {
            // If so, throw an exception indicating that a widget of this type is already registered
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));
        }

        // If the type is a valid subclass of IWidget and is not already registered,
        // add the createWidgetFunc to the _widgetCreators dictionary with the widgetType as the key
        _widgetCreators[widgetType] = createWidgetFunc;
    }
}
