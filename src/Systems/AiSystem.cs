using RelEcs;
using Zelda.Components;
using Zelda.Nodes.Character;
using Zelda.Nodes.Enemy;
using Zelda.Resources;

namespace Zelda.Systems;

public class TargetEntity
{
    public Entity Entity;
    public TargetEntity(Entity entity) => Entity = entity;
}
    
public class AiSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<PlayerCharacter>(out var playerCharacter)) return;

        var player = playerCharacter.Entity.Get<Character>();

        var withTarget = commands.Query().Has<Enemy, Vision, TargetEntity>();
        var withoutTarget = commands.Query().Has<Enemy, Vision>().Not<TargetEntity>();
            
        withoutTarget.ForEach((Entity entity, Enemy enemy, Vision vision) =>
        {
            if (player.Position.DistanceTo(enemy.Position) > vision.Value) return;
            entity.Add(new TargetEntity(playerCharacter.Entity));
        });
            
        withTarget.ForEach((Entity entity, Enemy enemy, Vision vision) =>
        {
            var distance = player.Position.DistanceTo(enemy.Position);
            if (distance <= vision.Value * 1.5f && distance > 24) return;
            entity.Remove<TargetEntity>();
        });
            
        commands.ForEach((Enemy enemy, TargetEntity targetEntity) =>
        {
            var targetCharacter = targetEntity.Entity.Get<Character>();
                
            var direction = enemy.GlobalPosition.DirectionTo(targetCharacter.GlobalPosition);
            enemy.MoveAndSlide(direction * 45f);
        });
    }
}