using Godot;
using System;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;

public class Enemy : KinematicBody2D
{
    [Export] public int Vision = 64;
    
    public void _Convert(Marshallable<Commands> commands)
    {
        commands.Value.Spawn(this).Add(new Health(16)).Add(new Strength(3));
    }

    public override void _Ready()
    {
        Update();
    }

    public override void _Draw()
    {
        DrawCircle(Vector2.Zero, Vision, new Color("33FF0000"));
    }
}
