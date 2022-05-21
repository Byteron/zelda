using Godot;

public class Room : Node2D
{
    public static Vector2 Size = new(21, 13);

    [Export] public Vector2 Coordinates;

    public override void _Ready()
    {
        // Position = Coordinates * Size * Vector2.One * 16f;
    }
}