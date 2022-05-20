using Godot;
using Zelda.Core;
using Zelda.Nodes.Debug;
using Zelda.States;

namespace Zelda;

public class Main : Node2D
{
    public override void _Ready()
    {
        var debug = GetNode<Debug>("Debug");
        
        var gsc = new GameStateController();
        AddChild(gsc);
        gsc.PushState(new PlayState());
        
        gsc.World.AddElement(debug);
    }
}