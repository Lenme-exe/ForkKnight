using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

public interface ICollidable
{
    Rectangle Hitbox { get; set; }
}