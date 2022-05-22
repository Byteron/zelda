using Godot;
using RelEcs;
using RelEcs.Godot;

public class Spawner : Position2D
{
    [Export] public PackedScene Scene;

    public void _Convert(Marshallable<Commands> commands)
    {
        if (Scene == null)
        {
            GD.PushWarning("Spawner does not have Scene to spawn!");
            return;
        }
        
        commands.Value.Spawn()
            .Add(new Coordinates(GlobalPosition))
            .Add(Scene);
        
        QueueFree();
    }
}
