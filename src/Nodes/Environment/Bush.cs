using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;
using Zelda.Core;

public class Bush : Node2D
{
    public void _Convert(Marshallable<Commands> commands)
    {
        commands.Value.Spawn(this).Add(new Health(5));
    }
}
