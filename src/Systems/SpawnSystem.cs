using Godot;
using Godot.Collections;
using RelEcs;
using RelEcs.Godot;
using Zelda.Core;

namespace Zelda.Systems;

public class SpawnTrigger
{
    public PackedScene Scene;
    public Coordinates Coords;
}

public class SpawnSystem : Object, ISystem
{
    Commands commands;
    GameState state;
    
    public void Run(Commands commands)
    {
        this.commands = commands;
        state = commands.GetElement<CurrentGameState>().State;

        foreach (var (coordinates, scene) in commands.Query<Coordinates, PackedScene>()) Spawn(coordinates, scene);
    }

    void Spawn(Coordinates coords, PackedScene scene)
    {
        var instance = scene.Instance<Node2D>();
        state.AddChild(instance);
        
        instance.GlobalPosition = coords.Value;
        instance.Connect("tree_exited", this, nameof(OnInstanceFreed), new Array() { coords, scene });
        
        if (instance.HasMethod("_Convert")) instance.Call("_Convert", new Marshallable<Commands>(commands));
    }

    void OnInstanceFreed(Coordinates coords, PackedScene scene)
    {
        Spawn(coords, scene);
    }
}