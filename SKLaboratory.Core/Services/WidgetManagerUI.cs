using SKLaboratory.Core.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Core.Services;

public class WidgetManagerUi : IStepper
{
	private readonly List<string> _demoNames;
	private readonly IWidgetFactory _widgetFactory;
	private readonly IWidgetManager _widgetManager;
	private readonly Vec2 _windowSize = new(50 * U.cm, 0);
	private Pose _demoSelectPose = new(0, 0, 0, Quat.Identity);

	public WidgetManagerUi(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
	{
		_widgetManager = widgetManager ?? throw new ArgumentNullException(nameof(widgetManager));
		_widgetFactory = widgetFactory ?? throw new ArgumentNullException(nameof(widgetFactory));
		_demoNames = _widgetFactory.RegisteredWidgetTypes.Select(type => type.Name).ToList();
	}

	public bool Initialize()
	{
		Enabled = true;
		return Enabled;
	}

	public bool Enabled { get; private set; }

	public void Step()
	{
		if (!Enabled) return;

		UI.WindowBegin("All Widgets", ref _demoSelectPose, _windowSize);
		DisplayWidgetButtons();
		DisplayExitButton();
		UI.WindowEnd();
	}

	public void Shutdown()
	{
		throw new NotImplementedException();
	}

	private void DisplayWidgetButtons()
	{
		for (var i = 0; i < _demoNames.Count; i++)
		{
			if (UI.Button(_demoNames[i]))
				_widgetManager.ToggleWidgetVisibility(_widgetFactory.RegisteredWidgetTypes[i]);
			UI.SameLine();
		}

		UI.NextLine();
		UI.HSeparator();
	}

	private static void DisplayExitButton()
	{
		if (UI.Button("Exit")) SK.Quit();
	}

	internal void Toggle()
	{
		Enabled = !Enabled;
	}
}