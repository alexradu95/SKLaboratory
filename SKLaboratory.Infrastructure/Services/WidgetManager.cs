using SKLaboratory.Infrastructure.Interfaces;

public class WidgetManager
{
    private readonly Dictionary<string, IWidget> ActiveWidgets = new Dictionary<string, IWidget>();
    private readonly IWidgetFactory _widgetFactory;

    public IReadOnlyDictionary<string, IWidget> ActiveWidgetsList => ActiveWidgets;

    public WidgetManager(IWidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    public bool ActivateWidget(string widgetType)
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

    public bool DeactivateWidget(string widgetType)
    {
        if (!ActiveWidgets.ContainsKey(widgetType))
        {
            return false;
        }

        ActiveWidgets[widgetType].Shutdown();
        ActiveWidgets.Remove(widgetType);

        // Automatically create a new widget of the same type
        return ActivateWidget(widgetType);
    }
}
