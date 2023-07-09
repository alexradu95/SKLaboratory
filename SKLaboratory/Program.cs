using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Infrastructure;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Steppers;
using SKLaboratory.Widgets;
using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SKLaboratory
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            BuildServiceProvider();

            AddPreInitSteppers();

            InitializeStereoKit();

            RegisterWidgetsToFactory();

            RunMainLoop();
        }

        private static void RegisterWidgetsToFactory()
        {
            var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();
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
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
            serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
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

        private static void RunMainLoop()
        {
            var widgetProvider = _serviceProvider.GetRequiredService<IWidgetManager>();

            SK.Run(() =>
            {
                foreach (var widget in widgetProvider.ActiveWidgetsList.Values)
                {
                    widget.Draw();
                }
            });
        }
    }
}