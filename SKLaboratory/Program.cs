using SKLaboratory.Infrastructure.Interfaces;
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
            DrawWidgets();
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
            widgetManager.RegisterWidget(new CubeWidget(), true);
            widgetManager.RegisterWidget(new FloorWidget(), true);
            widgetManager.ActivateWidget(new CubeWidget());
            widgetManager.ActivateWidget(new FloorWidget());
        }

        private static void DrawWidgets() => widgetManager.DrawActiveWidgets();
    }
}
