using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Resources;

namespace Zelda.Systems
{
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

            var playerNode = playerCharacter.Entity.Get<Character>();

            var withTarget = commands.Query().Has<Enemy>().Has<TargetEntity>();
            var withoutTarget = commands.Query().Has<Enemy>().Not<TargetEntity>();
            
            withoutTarget.ForEach((Entity entity, Enemy enemy) =>
            {
                if (playerNode.Position.DistanceTo(enemy.Position) > enemy.Vision) return;
                entity.Add(new TargetEntity(playerCharacter.Entity));
            });
            
            withTarget.ForEach((Entity entity, Enemy enemy) =>
            {
                var distance = playerNode.Position.DistanceTo(enemy.Position);
                if (distance <= enemy.Vision && distance > 24) return;
                entity.Remove<TargetEntity>();
            });
            
            commands.ForEach((Enemy enemy, ScanArea2D scanArea, TargetEntity targetEntity) =>
            {
                var targetCharacter = targetEntity.Entity.Get<Character>();
                scanArea.LookAt(targetCharacter.GlobalPosition);
                
                var direction = enemy.GlobalPosition.DirectionTo(targetCharacter.GlobalPosition);
                enemy.MoveAndSlide(direction * 45f);
            });
        }
    }
}