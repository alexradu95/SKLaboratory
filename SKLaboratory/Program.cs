using SKLaboratory.ApplicationLifecycle;
using SKLaboratory.Factories;
using SKLaboratory.Initialization;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    private static WidgetManager widgetManager = new WidgetManager(new WidgetFactory());
    private static StereoKitInitializer stereoKitInitializer = new StereoKitInitializer();
    private static StartupWidgetActivator widgetCreator = new StartupWidgetActivator(widgetManager);
    private static MainAppLoop mainLoop = new MainAppLoop(widgetManager);

    static void Main(string[] args)
    {
        stereoKitInitializer.Initialize();
        widgetCreator.CreateWidgets();
        mainLoop.Run();
    }
}
