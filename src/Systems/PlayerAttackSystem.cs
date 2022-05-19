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
            if (!Input.IsActionJustPressed("attack")) return;
            
            commands.ForEach((ScanArea2D ray, Strength strength, Player _) =>
            {
                GD.Print("Attack!");
                
                var areas = ray.GetOverlappingAreas();

                foreach (Area2D area in areas)
                {
                    if (area is not HitArea2D hitArea) continue;
                    
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