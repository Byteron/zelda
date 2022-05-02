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
    
    public class AiTargetSystem : ISystem
    {
        public void Run(Commands commands)
        {
            GD.Print("AiTargetSystem");
            
            if (!commands.TryGetElement<PlayerCharacter>(out var playerCharacter)) return;

            var playerNode = playerCharacter.Entity.Get<Node<Character>>().Value;

            var withTarget = commands.Query().Has<Node<Enemy>>().Has<TargetEntity>();
            var withoutTarget = commands.Query().Has<Node<Enemy>>().Not<TargetEntity>();
            
            withoutTarget.ForEach((Entity entity, ref Node<Enemy> enemy) =>
            {
                if (playerNode.Position.DistanceTo(enemy.Value.Position) > enemy.Value.Vision) return;
                GD.Print("Attach TargetEntity");
                entity.Add(new TargetEntity(playerCharacter.Entity));
            });
            
            withTarget.ForEach((Entity entity, ref Node<Enemy> enemy) =>
            {
                if (playerNode.Position.DistanceTo(enemy.Value.Position) <= enemy.Value.Vision) return;
                GD.Print("Remove TargetEntity");
                entity.Remove<TargetEntity>();
            });
        }
    }
}