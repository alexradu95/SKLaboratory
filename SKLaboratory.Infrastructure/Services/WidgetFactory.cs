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

    public void RegisterWidget(Type widgetType)
    {
        if (!typeof(IWidget).IsAssignableFrom(widgetType))
        {
            throw new ArgumentException($"Type must be a subclass of IWidget, but was {widgetType}", nameof(widgetType));
        }

        Func<IWidget> createWidgetFunc = () => (IWidget)Activator.CreateInstance(widgetType);
        if (_widgetCreators.ContainsKey(widgetType))
        {
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));
        }

        _widgetCreators[widgetType] = createWidgetFunc;
    }
}
