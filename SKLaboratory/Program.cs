using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Infrastructure.Services;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;
using SKLaboratory.Core.Services;
using SKLaboratory.Core.Steppers;
using StereoKit.Framework;

class Program
{
    private static IServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
        _serviceProvider = BuildServiceProvider();
        InitializeApplication();
        RunMainLoop();
    }

    private static IServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
        serviceCollection.AddSingleton<StartHandMenu>();
        serviceCollection.AddSingleton<MessageBus>();
        return serviceCollection.BuildServiceProvider();
    }

    private static void InitializeApplication()
    {
        AddPreInitSteppers();
        InitializeStereoKit();
        RegisterWidgetsToFactory();
        AddPostInitSteppers();
    }

    private static void AddPreInitSteppers() => SK.AddStepper<PassthroughFBExt>();

    private static void InitializeStereoKit()
    {
        var settings = new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets"
        };
        SK.Initialize(settings);
    }

    private static void RegisterWidgetsToFactory()
    {
        var messageBus = _serviceProvider.GetService<MessageBus>();
        var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();

        widgetFactory.RegisterWidget<ButtonWidget>(new ButtonWidgetCreator(messageBus));
        widgetFactory.RegisterWidget<TextWidget>(new TextWidgetCreator(messageBus));
    }

    private static void AddPostInitSteppers()
    {

        var widgetManager = _serviceProvider.GetService<IWidgetManager>();
        var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();

        // Assuming WidgetManagerUI can be directly instantiated
        var widgetManagerUI = new WidgetManagerUI(widgetManager, widgetFactory);
        SK.AddStepper(widgetManagerUI);
        StartHandMenu.InitializeHandMenuStepper();
    }

    private static void RunMainLoop()
    {
        var widgetManager = _serviceProvider.GetRequiredService<IWidgetManager>();
        SK.Run(() =>
        {
            foreach (var widget in widgetManager.ActiveWidgetsList.Values)
                widget.OnFrameUpdate();
        });
    }
}
