using global::SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Infrastructure
{
    public class HandMenuStepperManager : IStepperManager
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly IWidgetManager _widgetManager;

        HandMenuRadial? currentHandMenu;

        public HandMenuStepperManager(IWidgetFactory widgetFactory, IWidgetManager widgetManager)
        {
            _widgetFactory = widgetFactory;
            _widgetManager = widgetManager;
        }

        public void Initialize()
        {
            var widgetMenuItems = _widgetFactory.WidgetTypes
                .Select(widgetType => new HandMenuItem(widgetType.Name, null, () => _widgetManager.ToggleWidget(widgetType)))
                .ToList();

            widgetMenuItems.Add(new HandMenuItem("Back", null, null, HandMenuAction.Back));

            currentHandMenu = SK.AddStepper(new HandMenuRadial(
                new HandRadialLayer("Root",
                    new HandMenuItem("Log", null, () => Log.Info("Alex_Radu")),
                    new HandMenuItem("Boss 👻", null, () => Log.Info("Big_Boss")),
                    new HandMenuItem("Widgets", null, null, "Widgets")),
                new HandRadialLayer("Widgets", widgetMenuItems.ToArray())
            ));
        }

        public void Shutdown()
        {
            SK.RemoveStepper(currentHandMenu);
            currentHandMenu = null;
        }
    }
}
