using SKLaboratory.Core.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Core.Services
{
    public class WidgetManagerUI : IStepper
    {
        private readonly IWidgetManager _widgetManager;
        private readonly IWidgetFactory _widgetFactory;
        private Pose _demoSelectPose = new Pose(0, 0, 0, Quat.Identity);
        private readonly Vec2 _windowSize = new Vec2(50 * U.cm, 0);
        private readonly List<string> _demoNames;
        private bool _enabled = false;

        public WidgetManagerUI(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
        {
            _widgetManager = widgetManager ?? throw new ArgumentNullException(nameof(widgetManager));
            _widgetFactory = widgetFactory ?? throw new ArgumentNullException(nameof(widgetFactory));
            _demoNames = _widgetFactory.RegisteredWidgetTypes.Select(type => type.Name).ToList();
        }

        public bool Initialize()
        {
            _enabled = true;
            return _enabled;
        }

        public bool Enabled => _enabled;

        public void Step()
        {
            if (!_enabled) return;

            UI.WindowBegin("All Widgets", ref _demoSelectPose, _windowSize);
            DisplayWidgetButtons();
            DisplayExitButton();
            UI.WindowEnd();
        }

        private void DisplayWidgetButtons()
        {
            for (int i = 0; i < _demoNames.Count; i++)
            {
                if (UI.Button(_demoNames[i]))
                {
                    _widgetManager.ToggleWidgetVisibility(_widgetFactory.RegisteredWidgetTypes[i]);
                }
                UI.SameLine();
            }
            UI.NextLine();
            UI.HSeparator();
        }

        private static void DisplayExitButton()
        {
            if (UI.Button("Exit"))
            {
                SK.Quit();
            }
        }

        public void Shutdown() => throw new NotImplementedException();

        internal void Toggle() => _enabled = !_enabled;
    }
}
