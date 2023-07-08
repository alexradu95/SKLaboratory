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

    public void RegisterWidget(Type widgetType, Func<IWidget> createWidgetFunc)
    {
        if (_widgetCreators.ContainsKey(widgetType))
        {
            throw new ArgumentException($"A widget of type {widgetType} is already registered.", nameof(widgetType));
        }

        _widgetCreators[widgetType] = createWidgetFunc;
    }
}