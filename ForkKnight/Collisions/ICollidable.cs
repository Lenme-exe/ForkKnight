using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

public interface ICollidable
{
    Rectangle Hitbox { get; set; }
    int HitboxOffsetX { get; set; }
    int HitboxOffsetY { get; set; }

    void UpdateHitbox();
}