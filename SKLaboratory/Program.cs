using System;
using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Services;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    private static IServiceProvider _serviceProvider;

    private static void Main(string[] args)
    {
        BuildServiceProvider();

        AddPreInitSteppers();

        InitializeStereoKit();

        RegisterWidgetsToFactory();

        AddPostInitSteppers();

        RunMainLoop();
    }

    private static void BuildServiceProvider()
    {
        ServiceCollection serviceCollection = new();

        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
        serviceCollection.AddSingleton<UIManager>();
        serviceCollection.AddSingleton<MessageBus>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void AddPreInitSteppers()
    {
        SK.AddStepper<PassthroughStepper>();
    }

    private static void InitializeStereoKit()
    {
        SKSettings settings = new()
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets"
        };

        SK.Initialize(settings);
    }

    private static void RegisterWidgetsToFactory()
    {
        IWidgetFactory widgetFactory = _serviceProvider.GetService<IWidgetFactory>();
        // Register Widgets
        widgetFactory.RegisterWidget<CubeWidget>();
        widgetFactory.RegisterWidget<FloorWidget>();
        widgetFactory.RegisterWidget<PassthroughWidget>();
    }


    private static void AddPostInitSteppers()
    {
        UIManager.InitializeHandMenuStepper(_serviceProvider.GetService<IWidgetManager>(),
            _serviceProvider.GetService<IWidgetFactory>());
    }

    private static void RunMainLoop()
    {
        IWidgetManager widgetProvider = _serviceProvider.GetRequiredService<IWidgetManager>();
        SK.Run(() =>
        {
            foreach (IWidget widget in widgetProvider.ActiveWidgetsList.Values) widget.OnFrameUpdate();
        });
    }
}