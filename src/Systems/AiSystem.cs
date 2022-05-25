using Godot;
using RelEcs;
using Zelda.Components;
using Zelda.Nodes.Character;
using Zelda.Nodes.Enemy;
using Zelda.Resources;

namespace Zelda.Systems;

public class Target
{
    public Entity Entity;
    public Target(Entity entity) => Entity = entity;
}

public class AiSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<PlayerCharacter>(out var playerCharacter)) return;

        var player = playerCharacter.Entity.Get<Character>();

        foreach (var (entity, enemy, vision) in commands.Query<Entity, Enemy, Vision>().Has<Target>())
        {
            var distance = player.Position.DistanceTo(enemy.Position);
            if (distance <= vision.Value * 1.5f && distance > 24) continue;
            entity.Remove<Target>();
            GD.Print("Target Removed");
        }
        
        foreach (var (entity, enemy, vision) in commands.Query<Entity, Enemy, Vision>().Not<Target>())
        {
            if (player.Position.DistanceTo(enemy.Position) > vision.Value) continue;
            entity.Add(new Target(playerCharacter.Entity));
            GD.Print("Target Added");
        }

        foreach (var (enemy, target) in commands.Query<Enemy, Target>())
        {
            var targetCharacter = target.Entity.Get<Character>();
                
            var direction = enemy.GlobalPosition.DirectionTo(targetCharacter.GlobalPosition);
            enemy.MoveAndSlide(direction * 45f);
            GD.Print("Moved To Target");
        }
    }
}