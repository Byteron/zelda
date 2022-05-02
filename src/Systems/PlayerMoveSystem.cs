using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Core;

namespace Zelda.Systems
{
    public class PlayerMoveSystem : ISystem
    {
        public void Run(Commands commands)
        {
            if (!commands.TryGetElement<DeltaTime>(out var deltaTime))
            {
                return;
            }
            
            commands.ForEach((ref Node<Character> node, ref Node<ScanArea2D> scanArea, ref Player player) =>
            {
                var direction = GetMoveDirection();

                if (direction == Vector2.Zero) return;
                
                scanArea.Value.LookAt(scanArea.Value.GlobalPosition + direction);
                node.Value.MoveAndSlide(direction * player.Speed);
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
}