using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Resources;

namespace Zelda.Systems
{
    public struct TargetEntity
    {
        public Entity Entity;
        public TargetEntity(Entity entity) => Entity = entity;
    }
    
    public class AiSystem : ISystem
    {
        public void Run(Commands commands)
        {
            if (!commands.TryGetElement<PlayerCharacter>(out var playerCharacter)) return;

            var playerNode = playerCharacter.Entity.Get<Node<Character>>().Value;

            var withTarget = commands.Query().Has<Node<Enemy>>().Has<TargetEntity>();
            var withoutTarget = commands.Query().Has<Node<Enemy>>().Not<TargetEntity>();
            
            withoutTarget.ForEach((Entity entity, ref Node<Enemy> enemy) =>
            {
                if (playerNode.Position.DistanceTo(enemy.Value.Position) > enemy.Value.Vision) return;
                entity.Add(new TargetEntity(playerCharacter.Entity));
            });
            
            withTarget.ForEach((Entity entity, ref Node<Enemy> enemy) =>
            {
                var distance = playerNode.Position.DistanceTo(enemy.Value.Position);
                if (distance <= enemy.Value.Vision && distance > 24) return;
                entity.Remove<TargetEntity>();
            });
            
            commands.ForEach((ref Node<Enemy> enemy, ref Node<ScanArea2D> scanArea, ref TargetEntity targetEntity) =>
            {
                var targetCharacter = targetEntity.Entity.Get<Node<Character>>().Value;
                scanArea.Value.LookAt(targetCharacter.GlobalPosition);
                
                var direction = enemy.Value.GlobalPosition.DirectionTo(targetCharacter.GlobalPosition);
                enemy.Value.MoveAndSlide(direction * 45f);
            });
        }
    }
}