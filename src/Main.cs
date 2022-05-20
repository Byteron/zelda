using Godot;
using Zelda.Core;
using Zelda.States;

namespace Zelda;

public class Main : Node2D
{
    public override void _Ready()
    {
        var gsc = new GameStateController();
        AddChild(gsc);
        gsc.PushState(new PlayState());
    }
}