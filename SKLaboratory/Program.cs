using SKLaboratory.Infrastructure.Filters;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Services;
using SKLaboratory.Widgets;
using StereoKit;

namespace SKLaboratory
{
    internal class Program
    {
        private static WidgetManager widgetManager = new WidgetManager();

        static void Main(string[] args)
        {
            InitializePreSteppers();
            InitializeStereoKit();
            InitializePostInitSteppers();
            RegisterWidgets();
            // The widgets are now activated from code, in the future a menu will be implemented
            widgetManager.ActivateAllWidgets(new AllWidgetsFilter());
            SK.Run(() => DrawActiveWidgets());

        }

        private static void InitializePreSteppers()
        {
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

        private static void InitializePostInitSteppers()
        {
        }

        private static void RegisterWidgets()
        {
            widgetManager.RegisterWidget(new CubeWidget());
            widgetManager.RegisterWidget(new CubeWidget());
            widgetManager.RegisterWidget(new CubeWidget());
            widgetManager.RegisterWidget(new FloorWidget());
        }

        public static void DrawActiveWidgets()
        {
            foreach (var widget in widgetManager.ActiveWidgetsList)
            {
                widget.Draw();
            }
        }
    }
}
