using SKLaboratory.Core.Interfaces;
using SKLaboratory.Core.Services;

namespace SKLaboratory.Widgets.Creators;

public class ButtonWidgetCreator(MessageBus messageBus) : IWidgetCreator
{
    public IWidget CreateWidget()
    {
        return new ButtonWidget(messageBus);
    }
}