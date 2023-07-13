using SKLaboratory.Infrastructure.Base;
using SKLaboratory.Infrastructure.Services;
using StereoKit;

public class ButtonPressedMessage
{
    public string NewText { get; set; }
}

public class ButtonWidget : BaseWidget
{
    private readonly MessageBus _messageBus;

    int i = 0;

    public ButtonWidget(MessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public override void OnFrameUpdate()
    {
        if (UI.Button("Press me"))
        {
            _messageBus.Publish(new ButtonPressedMessage { NewText = $"Button was pressed {i} times!" });
            i++;
        }
    }
}
