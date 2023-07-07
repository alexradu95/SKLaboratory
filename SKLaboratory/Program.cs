using SKLaboratory.Factories;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    private static WidgetManager widgetManager = new WidgetManager(new WidgetFactory());

    static void Main(string[] args)
    {
        InitializePreSteppers();
        InitializeStereoKit();
        InitializePostInitSteppers();
        widgetManager.ActivateWidget("CubeWidget");
        widgetManager.ActivateWidget("FloorWidget");

        SK.Run(() => DrawActiveWidgets());

    }

    private static void InitializePreSteppers()
    {
    }

    private static void InitializeStereoKit()
    {
        var settings = new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets",
        };
        SK.Initialize(settings);
    }

    private static void InitializePostInitSteppers()
    {
    }

    public static void DrawActiveWidgets()
    {
        foreach (var widget in widgetManager.ActiveWidgetsList.Values)
        {
            widget.Draw();
        }
    }
}
