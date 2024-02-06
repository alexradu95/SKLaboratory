using SKLaboratory.Core.BaseClasses;
using SKLaboratory.Core.Services;
using StereoKit;

namespace SKLaboratory.Widgets;

public class ButtonPressedMessage
{
    public string NewText { get; set; }
}

public class ButtonWidget(MessageBus messageBus) : BaseWidget
{
    private int i;

    public override void OnFrameUpdate()
    {
        if (UI.Button("Press me"))
        {
            messageBus.Publish(new ButtonPressedMessage { NewText = $"Button was pressed {i++} times!" });
        }
    }
}