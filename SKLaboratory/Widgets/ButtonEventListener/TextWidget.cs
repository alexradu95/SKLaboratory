namespace SKLaboratory.Widgets.ButtonEventListener;

public class TextWidget : BaseWidget
{
	private string _text = "Button has not been pressed yet.";

	public TextWidget(MessageBus messageBus)
	{
		messageBus.Subscribe<ButtonPressedMessage>(message => _text = message.NewText);
	}

	public override void OnFrameUpdate()
	{
		UI.Label(_text);
	}
}