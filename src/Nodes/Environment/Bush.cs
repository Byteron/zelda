using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;

public class Bush : Node2D
{
    public void _Convert(Marshallable<Commands> commands)
    {
        commands.Value.Spawn(this).Add(new Health(5));
    }
}
