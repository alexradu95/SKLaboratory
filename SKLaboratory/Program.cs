using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.ApplicationLifecycle;
using SKLaboratory.Factories;
using SKLaboratory.Initialization;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    static void Main(string[] args)
    {

        // Create a new service collection
        var serviceCollection = new ServiceCollection();

        // Register your services
        serviceCollection.AddSingleton<WidgetFactory>();
        serviceCollection.AddSingleton<WidgetManager>();

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Use the service provider to get your services
        var stereoKitInitializer = serviceProvider.GetRequiredService<StereoKitInitializer>();
        var mainLoop = serviceProvider.GetRequiredService<MainAppLoop>();

        var settings = new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets",
        };

        SK.Initialize(settings);


        SK.Run(() =>
        {
            foreach (var widget in _widgetManager.ActiveWidgetsList.Values)
            {
                widget.Draw();
            }
        });
    }
}
