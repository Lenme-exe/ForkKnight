using Microsoft.Xna.Framework;

namespace ForkKnight.Movement;

internal interface IJumpManager
{
    void HandleJump(IMovable movable, GameTime gameTime);
}