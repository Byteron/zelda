using System;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Zelda.Systems
{
    
    public class AiMoveSystem : ISystem
    {
        public void Run(Commands commands)
        {
            GD.Print("AiMoveSystem");
            
            commands.ForEach((ref Node<Enemy> enemy, ref Node<ScanArea2D> scanArea, ref TargetEntity targetEntity) =>
            {
                var targetCharacter = targetEntity.Entity.Get<Node<Character>>().Value;
                scanArea.Value.LookAt(targetCharacter.GlobalPosition);
            });
        }
    }
}