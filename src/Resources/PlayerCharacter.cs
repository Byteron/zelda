using RelEcs;

namespace Zelda.Resources
{
    public class PlayerCharacter
    {
        public Entity Entity;
        public PlayerCharacter(Entity entity) => Entity = entity;
    }
}