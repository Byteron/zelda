using Godot;
using RelEcs;
using Zelda.Nodes.Character;
using Zelda.Resources;

namespace Zelda.Systems;

public class UpdateCameraPositionSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<Camera2D>(out var camera)) return;
        if (!commands.TryGetElement<PlayerCharacter>(out var player)) return;
        if (!commands.TryGetElement<MiniMap>(out var miniMap)) return;

        var coords = (player.Entity.Get<Character>().Position / Room.Size / (Vector2.One * 16f)).Floor();
        var position = coords * Room.Size * (Vector2.One * 16f);
        camera.Position = position;
        
        miniMap.SetActiveRoom(coords);
    }
}