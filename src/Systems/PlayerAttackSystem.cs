using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;
using Zelda.Core;
using Zelda.Nodes.Physics;

namespace Zelda.Systems;

public class PlayerAttackSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!Input.IsActionJustPressed("attack")) return;

        var query = commands.Query<ScanArea2D, Strength>().Has<Controllable>();
        foreach (var (ray, strength) in query)
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
        }
    }
}