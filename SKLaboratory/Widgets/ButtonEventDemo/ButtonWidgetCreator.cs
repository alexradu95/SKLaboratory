namespace SKLaboratory.Widgets.ButtonEventDemo;

public class ButtonWidgetCreator(MessageBus messageBus) : IWidgetCreator
{
	public IWidget CreateWidget()
	{
		return new ButtonWidget(messageBus);
	}
}