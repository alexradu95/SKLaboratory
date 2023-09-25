using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Infrastructure.Services;

public class StartHandMenu
{
    public static void InitializeHandMenuStepper(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
    {
        var widgetMenuItems = widgetFactory.WidgetTypes.Select(widgetType =>
            new HandMenuItem(widgetType.Name, null, () => widgetManager.ToggleWidgetActivation(widgetType))).ToArray();

        SK.AddStepper(new HandMenuRadial(new HandRadialLayer("Root", widgetMenuItems)));
    }
}