using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;

namespace Zelda.Nodes.Enemy;

public class Enemy : KinematicBody2D
{
    Entity entity;
    
    // public override void _Ready()
    // {
    //     Update();
    // }
    //
    // public override void _Draw()
    // {
    //     if (!entity.IsAlive) return;
    //     
    //     DrawCircle(Vector2.Zero, entity.Get<Vision>().Value, new Color("33FF0000"));
    //     DrawCircle(Vector2.Zero, entity.Get<Vision>().Value * 1.5f, new Color("33FF0000"));
    // }

    public void _Convert(Marshallable<Commands> commands)
    {
        entity = commands.Value.Spawn(this).Add(new Health(16)).Add(new Strength(3)).Add(new Vision(64));
    }
}