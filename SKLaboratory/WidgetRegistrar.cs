using SKLaboratory.GenerativeWorld;

namespace SKLaboratory;

public class WidgetRegistrar(IServiceProvider serviceProvider)
{
	public void RegisterWidgetsToFactory()
	{
		var messageBus = serviceProvider.GetService<MessageBus>();
		var widgetFactory = serviceProvider.GetService<IWidgetFactory>();

		widgetFactory.RegisterWidget<ButtonWidget>(new ButtonWidgetCreator(messageBus));
		widgetFactory.RegisterWidget<TextWidget>(new TextWidgetCreator(messageBus));
		widgetFactory.RegisterWidget<GenerativeWorldWidget>(new GenerativeWorldCreator());
	}
}