using SKLaboratory.Core.Interfaces;
using SKLaboratory.Core.Services;

namespace SKLaboratory.Widgets.Creators;

public class TextWidgetCreator(MessageBus messageBus) : IWidgetCreator
{
    public IWidget CreateWidget()
    {
        return new TextWidget(messageBus);
    }
}