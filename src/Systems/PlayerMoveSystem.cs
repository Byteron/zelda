using Godot;
using RelEcs;
using Zelda.Components;

namespace Zelda.Systems;

public class PlayerMoveSystem : ISystem
{
    public void Run(Commands commands)
    {
        var query = commands.Query().Has<Character, ScanArea2D, Speed, Controllable>();
        query.ForEach((Character node, ScanArea2D scanArea, Speed speed) =>
        {
            var direction = GetMoveDirection();

            if (direction == Vector2.Zero) return;
                
            scanArea.LookAt(scanArea.GlobalPosition + direction);
            node.MoveAndSlide(direction * speed.Value);
        });
    }

    static Vector2 GetMoveDirection()
    {
        var direction = Vector2.Zero;
        direction.y = Input.GetAxis("move_up", "move_down");
        direction.x = Input.GetAxis("move_left", "move_right");
        return direction.Normalized();
    }
}