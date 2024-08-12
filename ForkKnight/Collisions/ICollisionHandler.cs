using System.Collections.Generic;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

internal interface ICollisionHandler
{
    void CheckCollision(IMovable movable, List<Rectangle> collisionRectangles);
}