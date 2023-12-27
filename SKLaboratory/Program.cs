using Microsoft.Extensions.DependencyInjection;
using SKLaboratory;
using SKLaboratory.Core.Services;
using SKLaboratory.Core.Steppers;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Services;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;
using System;

var _serviceProvider = BuildServiceProvider();

AddPreInitSteppers();
InitializeStereoKit();
RegisterWidgetsToFactory();
AddPostInitSteppers();
RunMainLoop();

IServiceProvider BuildServiceProvider()
{
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
    serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
    serviceCollection.AddSingleton<StartHandMenu>();
    serviceCollection.AddSingleton<MessageBus>();
    return serviceCollection.BuildServiceProvider();
}

void AddPreInitSteppers() => SK.AddStepper<PassthroughStepper>();

void InitializeStereoKit()
{
    var settings = new SKSettings
    {
        appName = "SKLaboratory",
        assetsFolder = "Assets"
    };

    SK.Initialize(settings);
}

void RegisterWidgetsToFactory()
{
    var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();
    widgetFactory.RegisterWidget<CubeWidget>();
    widgetFactory.RegisterWidget<FloorWidget>();
    widgetFactory.RegisterWidget<PassthroughWidget>();
    widgetFactory.RegisterWidget<ButtonWidget>();
    widgetFactory.RegisterWidget<TextWidget>();
}

void AddPostInitSteppers()
{
    WidgetManagerUI.InitializeWidgetManagerUIStepper(_serviceProvider.GetService<IWidgetManager>(),
    _serviceProvider.GetService<IWidgetFactory>());
    StartHandMenu.InitializeHandMenuStepper();

}

void RunMainLoop()
{
    var widgetProvider = _serviceProvider.GetRequiredService<IWidgetManager>();
    SK.Run(() =>
    {
        foreach (var widget in widgetProvider.ActiveWidgetsList.Values)
            widget.OnFrameUpdate();
    });
}

