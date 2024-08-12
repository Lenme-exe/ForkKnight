using System.Collections.Generic;
using System.Windows.Forms;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

internal class CollisionHandler : ICollisionHandler
{
    public void CheckCollision(IMovable movable, List<Rectangle> collisionRectangles)
    {
        foreach (var rect in collisionRectangles)
        {
            if (movable.Hitbox.Intersects(rect))
            {
                HandleCollision(movable, rect);
            }
        }
    }

    private void HandleCollision(IMovable movable, Rectangle collisionRectangle)
    {
        if (movable.Velocity.Y > 0 && movable.Hitbox.Bottom > collisionRectangle.Top)
        {
            movable.Position = new Vector2(movable.Position.X, collisionRectangle.Top - movable.Hitbox.Height);
            movable.Velocity = new Vector2(movable.Velocity.X, 0);
            movable.IsFalling = false;
        }
    }
}