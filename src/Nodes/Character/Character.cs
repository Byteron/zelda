using Godot;
using System;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;
using Zelda.Resources;

public struct Player
{
    public float Speed;
}

public class Character : KinematicBody2D
{
    public void _Convert(Marshallable<Commands> commands)
    {
        var entity = commands.Value.Spawn(this)
            .Add(new Health(12))
            .Add(new Player { Speed = 72f })
            .Add(new Strength(10));
        
        commands.Value.AddElement(new PlayerCharacter(entity));
    }
}
