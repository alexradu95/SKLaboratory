namespace SKLaboratory.Widgets.ButtonEventListener;

public class TextWidgetCreator(MessageBus messageBus) : IWidgetCreator
{
	public IWidget CreateWidget()
	{
		return new TextWidget(messageBus);
	}
}