using SKLaboratory.Core.Services;
using SKLaboratory.Infrastructure;
using StereoKit;

public class ButtonPressedMessage
{
    public string NewText { get; set; }
}

public class ButtonWidget : BaseWidget
{
    private readonly MessageBus _messageBus;
    private int i;

    public ButtonWidget(MessageBus messageBus) => _messageBus = messageBus;

    public override void OnFrameUpdate()
    {
        if (UI.Button("Press me"))
        {
            _messageBus.Publish(new ButtonPressedMessage { NewText = $"Button was pressed {i++} times!" });
        }
    }
}
