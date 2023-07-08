using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Factories;
using SKLaboratory.Widgets;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    static void Main(string[] args)
    {
        // Initialize StereoKit
        InitializeStereoKit();

        // Build ServiceProvider and Register Core Infrastructure Services
        ServiceProvider serviceProvider = BuildServiceProvider();

        // Register Widgets
        RegisterWidgets(serviceProvider);

        // Activate Start Widgets
        ActivateStartWidgets(serviceProvider);

        // Run the main loop
        RunMainLoop(serviceProvider);
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

    private static ServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<WidgetProvider>();

        return serviceCollection.BuildServiceProvider();
    }

    private static void RegisterWidgets(ServiceProvider serviceProvider)
    {
        var widgetProvider = serviceProvider.GetRequiredService<WidgetProvider>();

        widgetProvider.RegisterWidget(typeof(CubeWidget));
        widgetProvider.RegisterWidget(typeof(FloorWidget));
    }

    private static void ActivateStartWidgets(ServiceProvider serviceProvider)
    {
        var widgetProvider = serviceProvider.GetRequiredService<WidgetProvider>();

        widgetProvider.ActivateWidget(typeof(CubeWidget));
        widgetProvider.ActivateWidget(typeof(FloorWidget));
    }

    private static void RunMainLoop(ServiceProvider serviceProvider)
    {
        var widgetProvider = serviceProvider.GetRequiredService<WidgetProvider>();

        SK.Run(() =>
        {
            foreach (var widget in widgetProvider.ActiveWidgetsList.Values)
            {
                widget.Draw();
            }
        });
    }
}
