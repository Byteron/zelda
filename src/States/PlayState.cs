using Zelda.Core;
using Zelda.Systems;

namespace Zelda.States
{
    public class PlayState : GameState
    {
        public override void Init(GameStateController gameStates)
        {
            UpdateSystems
                .Add(new PlayerMoveSystem())
                .Add(new PlayerAttackSystem())
                .Add(new AiSystem())
                .Add(new DamageTriggerSystem());
        }
    }
}