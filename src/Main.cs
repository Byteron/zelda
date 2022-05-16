using System;
using Godot;
using Zelda.Core;
using Zelda.States;

public class Main : Node2D
{
    public override void _Ready()
    {
        var gsc = new GameStateController();
        AddChild(gsc);
        gsc.PushState(new PlayState());
    }
}

