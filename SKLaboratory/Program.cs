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

        RunMainLoop();
    }

    private static void RegisterWidgetsToFactory()
    {
        IWidgetFactory widgetFactory = _serviceProvider.GetService<IWidgetFactory>();
        // Register Widgets
        widgetFactory.RegisterWidget<CubeWidget>();
        widgetFactory.RegisterWidget<FloorWidget>();
        widgetFactory.RegisterWidget<PassthroughWidget>();
    }

    private static void AddPreInitSteppers()
    {
        SK.AddStepper<PassthroughStepper>();
    }

    private static void BuildServiceProvider()
    {
        ServiceCollection serviceCollection = new();

        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
        serviceCollection.AddSingleton<UIManager>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
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

    private static void RunMainLoop()
    {
        IWidgetManager widgetProvider = _serviceProvider.GetRequiredService<IWidgetManager>();
        UIManager
            uiManager = _serviceProvider
                .GetRequiredService<UIManager>(); //we get it in order to trigger the constructor and add it

        SK.Run(() =>
        {
            foreach (IWidget widget in widgetProvider.ActiveWidgetsList.Values) widget.Draw();
        });
    }
}