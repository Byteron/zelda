using Godot;
using System;

public class Main : Node2D
{
    public override void _Ready()
    {
        var gsc = new GameStateController();
        AddChild(gsc);
        // gsc.PushState();
    }
}
