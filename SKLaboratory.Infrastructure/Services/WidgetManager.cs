using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;

public class WidgetManager
{
    private readonly Dictionary<Type, IWidget> ActiveWidgets = new Dictionary<Type, IWidget>();
    private readonly IWidgetFactory _widgetFactory;

    public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => ActiveWidgets;

    public WidgetManager(IWidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    public bool ActivateWidget(Type widgetType)
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

    public bool DeactivateWidget(Type widgetType)
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
