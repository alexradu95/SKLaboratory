using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;

public class WidgetProvider
{
    private readonly Dictionary<string, IWidget> ActiveWidgets = new Dictionary<string, IWidget>();
    private readonly IWidgetFactory _widgetFactory;

    public IReadOnlyDictionary<string, IWidget> ActiveWidgetsList => ActiveWidgets;

    public WidgetProvider(IWidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    public bool ActivateWidget(string widgetType)
    {
        try
        {
            if (ActiveWidgets.ContainsKey(widgetType))
            {
                return false;
            }

            var widget = _widgetFactory.CreateWidget(widgetType);
            if (widget == null || !widget.Initialize())
            {
                return false;
            }

            ActiveWidgets.Add(widgetType, widget);
            return true;
        }
        catch (UnknownWidgetTypeException ex)
        {
            // Log the error or handle it as appropriate
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeactivateWidget(string widgetType)
    {
        if (!ActiveWidgets.ContainsKey(widgetType))
        {
            return false;
        }

        try
        {
            ActiveWidgets[widgetType].Shutdown();
            ActiveWidgets.Remove(widgetType);
            return ActivateWidget(widgetType);
        }
        catch (Exception ex)
        {
            // Log the error or handle it as appropriate
            Console.WriteLine(ex.Message);
            return false;
        }
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
        _widgetFactory.RegisterWidget(widgetType.Name, createWidgetFunc);
    }
}
