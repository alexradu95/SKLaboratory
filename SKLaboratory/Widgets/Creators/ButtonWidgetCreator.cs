using SKLaboratory.Core.Services;
using SKLaboratory.Infrastructure.Interfaces;

public class ButtonWidgetCreator : IWidgetCreator
{
    private readonly MessageBus _messageBus;

    public ButtonWidgetCreator(MessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public IWidget CreateWidget()
    {
        return new ButtonWidget(_messageBus);
    }
}
