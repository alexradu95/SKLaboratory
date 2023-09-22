using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
namespace SKLaboratory.Infrastructure.Services;

public class WidgetManager : IWidgetManager
{
    private readonly Dictionary<Type, IWidget> _activeWidgets = new();
    private readonly IWidgetFactory _widgetFactory;

    public WidgetManager(IWidgetFactory widgetFactory) => _widgetFactory = widgetFactory;

    public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

    public void ToggleWidget(Type widgetType)
    {
        try
        {
            if (_activeWidgets.ContainsKey(widgetType))
                DeactivateWidget(widgetType);
            else
                ActivateWidget(widgetType);
        }
        catch (WidgetCreationFailedException ex)
        {
            Log.Err($"Failed to create or deactivate widget of type: {widgetType}. {ex.Message}");
        }
        catch (UnknownWidgetTypeException ex)
        {
            Log.Err($"Unknown widget type: {widgetType}. {ex.Message}");
        }
        catch (Exception ex)
        {
            Log.Err($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void ActivateWidget(Type widgetType) => _activeWidgets.Add(widgetType, _widgetFactory.CreateWidget(widgetType));
    private void DeactivateWidget(Type widgetType) => _activeWidgets.Remove(widgetType);
}