using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.ApplicationLifecycle;
using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;
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
        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<WidgetManager>();
        serviceCollection.AddSingleton<StereoKitInitializer>();
        serviceCollection.AddSingleton<StartupWidgetActivator>();
        serviceCollection.AddSingleton<MainAppLoop>();

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Use the service provider to get your services
        var stereoKitInitializer = serviceProvider.GetRequiredService<StereoKitInitializer>();
        var widgetCreator = serviceProvider.GetRequiredService<StartupWidgetActivator>();
        var mainLoop = serviceProvider.GetRequiredService<MainAppLoop>();

        // Use the services
        stereoKitInitializer.Initialize();
        widgetCreator.CreateWidgets();
        mainLoop.Run();
    }
}
