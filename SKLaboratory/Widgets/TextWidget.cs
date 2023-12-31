﻿using SKLaboratory.Core.Services;
using SKLaboratory.Infrastructure;
using SKLaboratory.Infrastructure.Services;
using StereoKit;
using System;

public class TextWidget : BaseWidget
{
    private readonly MessageBus _messageBus;
    private string _text = "Button has not been pressed yet.";

    public TextWidget(MessageBus messageBus)
    {
        _messageBus = messageBus;
        _messageBus.Subscribe<ButtonPressedMessage>(message => _text = message.NewText);
    }

    public override void OnFrameUpdate()
    {
        UI.Label(_text);
    }
}