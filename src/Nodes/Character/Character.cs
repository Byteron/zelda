using Godot;
using RelEcs;
using RelEcs.Godot;
using Zelda.Components;
using Zelda.Core;
using Zelda.Resources;

public class Character : KinematicBody2D
{
    public void _Convert(Marshallable<Commands> commands)
    {
        var entity = commands.Value.Spawn(this)
            .Add(new Health(12))
            .Add(new Speed(72))
            .Add(new Strength(10))
            .Add<Controllable>();
        
        commands.Value.AddElement(new PlayerCharacter(entity));
    }
}
