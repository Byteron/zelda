using Godot;

namespace Zelda.Nodes.Debug;

public class Debug : CanvasLayer
{
    public Label Label;
    
    public override void _Ready()
    {
        Label = GetNode<Label>("%Label");
    }
}