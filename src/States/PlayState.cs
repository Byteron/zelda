using Zelda.Core;
using Zelda.Systems;

namespace Zelda.States;

public class PlayState : GameState
{
    public override void Init(GameStateController gameStates)
    {
        InitSystems.Add(new SpawnSystem());
        
        UpdateSystems
            .Add(new DebugSystem())
            .Add(new PlayerMoveSystem())
            .Add(new PlayerAttackSystem())
            .Add(new AiSystem())
            .Add(new DamageTriggerSystem())
            .Add(new UpdateCameraPositionSystem());
    }
}