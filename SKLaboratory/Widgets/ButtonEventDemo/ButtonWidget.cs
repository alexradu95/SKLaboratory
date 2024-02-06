namespace SKLaboratory.Widgets.ButtonEventDemo;

public class ButtonPressedMessage
{
	public string NewText { get; init; }
}

public class ButtonWidget(MessageBus messageBus) : BaseWidget
{
	private int _i;

	public override void OnFrameUpdate()
	{
		if (UI.Button("Press me"))
			messageBus.Publish(new ButtonPressedMessage { NewText = $"Button was pressed {_i++} times!" });
	}
}