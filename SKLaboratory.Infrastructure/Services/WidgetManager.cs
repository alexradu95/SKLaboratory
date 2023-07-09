using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;

public class WidgetManager : IWidgetManager
{
    private readonly Dictionary<Type, IWidget> _activeWidgets = new Dictionary<Type, IWidget>();
    private readonly IWidgetFactory _widgetFactory;

    public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

    public WidgetManager(IWidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    public bool ToggleWidget(Type widgetType)
    {
        try
        {
            if (_activeWidgets.ContainsKey(widgetType))
            {
                return DeactivateWidget(widgetType);
            }
            else
            {
                return ActivateWidget(widgetType);
            }
        }
        catch (WidgetCreationFailedException ex)
        {
            Log.Err($"Failed to create or deactivate widget of type: {widgetType}. {ex.Message}");
            return false;
        }
        catch (UnknownWidgetTypeException ex)
        {
            Log.Err($"Unknown widget type: {widgetType}. {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Log.Err($"An unexpected error occurred: {ex.Message}");
            return false;
        }
    }


    private bool ActivateWidget(Type widgetType)
    {
        var widget = _widgetFactory.CreateWidget(widgetType);
        if (widget == null || !widget.Initialize())
        {
            return false;
        }

        _activeWidgets.Add(widgetType, widget);
        return true;
    }

    private bool DeactivateWidget(Type widgetType)
    {
        _activeWidgets[widgetType].Shutdown();
        _activeWidgets.Remove(widgetType);
        return true;
    }
}
