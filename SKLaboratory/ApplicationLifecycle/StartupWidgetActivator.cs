namespace SKLaboratory.ApplicationLifecycle;

public class StartupWidgetActivator
{
    private WidgetManager _widgetManager;

    public StartupWidgetActivator(WidgetManager widgetManager)
    {
        _widgetManager = widgetManager;
    }

    public void CreateWidgets()
    {
        _widgetManager.ActivateWidget("CubeWidget");
        _widgetManager.ActivateWidget("FloorWidget");
    }
}
