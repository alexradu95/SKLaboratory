namespace SKLaboratory;

public class MainLoopRunner(IServiceProvider serviceProvider)
{
	public void RunMainLoop()
	{
		var widgetManager = serviceProvider.GetRequiredService<IWidgetManager>();
		SK.Run(() => { UpdateWidgetsInFrame(widgetManager); });
	}

	private static void UpdateWidgetsInFrame(IWidgetManager widgetManager)
	{
		foreach (var widget in widgetManager.ActiveWidgetsList.Values)
			widget.OnFrameUpdate();
	}
}