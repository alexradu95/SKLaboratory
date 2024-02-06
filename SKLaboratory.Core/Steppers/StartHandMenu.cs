using SKLaboratory.Core.Services;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Core.Steppers;

public class StartHandMenu
{
	public static void InitializeHandMenuStepper()
	{
		var widgetManagerUi = SK.GetOrCreateStepper<WidgetManagerUi>();
		SK.AddStepper(new HandMenuRadial(new HandRadialLayer("Root",
			new HandMenuItem("Toggle WidgetMenu", null, widgetManagerUi.Toggle))));
	}
}