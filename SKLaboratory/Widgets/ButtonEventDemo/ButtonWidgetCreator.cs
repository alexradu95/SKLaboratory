namespace SKLaboratory.Widgets.ButtonEventDemo;

public class ButtonWidgetCreator() : IWidgetCreator
{
	private readonly MessageBus _messageBus;

	public ButtonWidgetCreator(MessageBus messageBus) : this()
	{
		this._messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
	}
	
	public IWidget CreateWidget()
	{
		return new ButtonWidget(_messageBus);
	}
}