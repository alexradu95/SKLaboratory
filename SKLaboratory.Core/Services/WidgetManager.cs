using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
namespace SKLaboratory.Infrastructure.Services;

public class WidgetManager(IWidgetFactory widgetFactory) : IWidgetManager
{
    private readonly Dictionary<Type, IWidget> _activeWidgets = [];

    public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

    public void ToggleWidgetVisibility(Type widgetType)
    {
        if (_activeWidgets.ContainsKey(widgetType))
            DeactivateWidget(widgetType);
        else
            ActivateWidget(widgetType);
    }

    private void ActivateWidget(Type widgetType)
    {
        try
        {
            _activeWidgets[widgetType] = widgetFactory.CreateWidget(widgetType);
        }
        catch (Exception ex)
        {
            HandleWidgetException(ex, widgetType);
        }
    }

    private void DeactivateWidget(Type widgetType) => _activeWidgets.Remove(widgetType);

    private void HandleWidgetException(Exception ex, Type widgetType)
    {
        if (ex is WidgetCreationFailedException || ex is UnknownWidgetTypeException)
            Log.Err($"Widget operation failed for type {widgetType}: {ex.Message}");
        else
            Log.Err($"An unexpected error occurred: {ex.Message}");
    }
}
