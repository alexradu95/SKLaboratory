using SKLaboratory.Core.Services;
using SKLaboratory.Infrastructure.Interfaces;

public class TextWidgetCreator : IWidgetCreator
{
    private readonly MessageBus _messageBus;

    public TextWidgetCreator(MessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public IWidget CreateWidget()
    {
        return new TextWidget(_messageBus);
    }
}
