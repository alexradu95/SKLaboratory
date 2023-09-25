using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Infrastructure.Services
{
    public class WidgetManagerUI : IStepper
    {
        private IWidgetManager _widgetManager;
        private IWidgetFactory _widgetFactory;
        private Pose _demoSelectPose = new Pose(0, 0, 0, Quat.Identity);
        private Vec2 _windowSize = new Vec2(50 * U.cm, 0);
        private List<string> _demoNames;

        public static void InitializeWidgetManagerUIStepper(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
        {
            SK.AddStepper(new WidgetManagerUI(widgetManager, widgetFactory));
        }

        private WidgetManagerUI(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
        {
            _widgetManager = widgetManager;
            _widgetFactory = widgetFactory;
            _demoNames = _widgetFactory.WidgetTypes.ConvertAll(type => type.Name);
        }

        public bool Initialize()
        {
            _enabled = true;
            return _enabled;
        }

        private bool _enabled = false;
        public bool Enabled => _enabled;

        public void Step()
        {
            if (_enabled) { 
            UI.WindowBegin("All Widgets", ref _demoSelectPose, _windowSize);

            for (int i = 0; i < _demoNames.Count; i++)
            {
                if (UI.Button(_demoNames[i]))
                {
                    _widgetManager.ToggleWidgetActivation(_widgetFactory.WidgetTypes[i]);
                }
                UI.SameLine();
            }

            UI.NextLine();
            UI.HSeparator();

            if (UI.Button("Exit"))
            {
                SK.Quit();
            }

            UI.WindowEnd();
            }
        }


        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        internal void Toggle()
        {
            this._enabled = !this._enabled;
        }
    }
}