using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;

namespace Zelda.Systems;

public class DamageTrigger
{
    public Entity Entity;
    public int Amount;

    public DamageTrigger(Entity entity, int amount)
    {
        Entity = entity;
        Amount = amount;
    }
}
    
public class DamageTriggerSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((DamageTrigger trigger) =>
        {
            GD.Print("Damage Trigger!");
                
            if (!trigger.Entity.Has<Health>()) return;

            var health = trigger.Entity.Get<Health>();
            health.Value -= trigger.Amount;
                
            GD.Print("Damage Dealt!");

            if (health.Value > 0) return;
                
            trigger.Entity.DespawnAndFree();
            GD.Print("DEATH!");
        });
    }
}