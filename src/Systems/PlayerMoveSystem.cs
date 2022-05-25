using Godot;
using RelEcs;
using Zelda.Components;
using Zelda.Nodes.Character;
using Zelda.Nodes.Physics;

namespace Zelda.Systems;

public class PlayerMoveSystem : ISystem
{
    public void Run(Commands commands)
    {
        var query = commands.Query<Character, ScanArea2D, Speed>().Has<Controllable>();
        foreach (var (character, scanArea, speed) in query)
        {
            var direction = GetMoveDirection();

            if (direction == Vector2.Zero) return;
                
            scanArea.LookAt(scanArea.GlobalPosition + direction);
            character.MoveAndSlide(direction * speed.Value);
        }
    }

    static Vector2 GetMoveDirection()
    {
        var direction = Vector2.Zero;
        direction.y = Input.GetAxis("move_up", "move_down");
        direction.x = Input.GetAxis("move_left", "move_right");
        return direction.Normalized();
    }
}