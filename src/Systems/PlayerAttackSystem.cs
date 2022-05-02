using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;
using Zelda.Core;

namespace Zelda.Systems
{
    public class PlayerAttackSystem : ISystem
    {
        public void Run(Commands commands)
        {
            if (!commands.TryGetElement<DeltaTime>(out var deltaTime))
            {
                return;
            }

            if (!Input.IsActionJustPressed("attack")) return;
            
            commands.ForEach((ref Node<ScanArea2D> ray, ref Strength strength, ref Player _) =>
            {
                GD.Print("Attack!");
                
                var areas = ray.Value.GetOverlappingAreas();

                foreach (Area2D area in areas)
                {
                    if (!(area is HitArea2D hitArea)) continue;
                    
                    var meta = hitArea.GetMeta("Entity");

                    if (meta is Marshallable<Entity> colliderEntity)
                    {
                        commands.Send(new DamageTrigger(colliderEntity.Value, strength.Value));
                    }
                }
            });
        }
    }
}