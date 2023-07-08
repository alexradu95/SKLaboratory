using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.ApplicationLifecycle;
using SKLaboratory.Factories;
using SKLaboratory.Initialization;

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
        serviceCollection.AddSingleton<StereoKitInitializer>();
        serviceCollection.AddSingleton<MainAppLoop>();

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Use the service provider to get your services
        var stereoKitInitializer = serviceProvider.GetRequiredService<StereoKitInitializer>();
        var mainLoop = serviceProvider.GetRequiredService<MainAppLoop>();

        // Use the services
        stereoKitInitializer.Initialize();
        mainLoop.Run();
    }
}
