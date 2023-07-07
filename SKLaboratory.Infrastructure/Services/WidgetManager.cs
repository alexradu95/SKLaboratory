using SKLaboratory.Factories;
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
}
