using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects;

public interface ICollidable
{
    Rectangle Hitbox { get; set; }
}