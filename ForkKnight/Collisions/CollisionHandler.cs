using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ForkKnight.Movement;
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
        var intersection = Rectangle.Intersect(movable.Hitbox, collisionRectangle);

        if (intersection.Height < intersection.Width)
        {
            if (movable.Velocity.Y > 0 && movable.Hitbox.Bottom > collisionRectangle.Top &&
                movable.Hitbox.Top < collisionRectangle.Top)
            {
                movable.Position = new Vector2(movable.Position.X, collisionRectangle.Top - movable.Hitbox.Height);
                movable.Velocity = new Vector2(movable.Velocity.X, 0);
                movable.IsFalling = false;
            }
            else if (movable.Velocity.Y < 0 && movable.Hitbox.Top < collisionRectangle.Bottom &&
                     movable.Hitbox.Bottom > collisionRectangle.Bottom)
            {
                movable.Position = new Vector2(movable.Position.X, collisionRectangle.Bottom);
                movable.Velocity = new Vector2(movable.Velocity.X, 0);
            }
        }
        else if (intersection.Width < intersection.Height)
        {
            if (movable.Velocity.X > 0 && movable.Hitbox.Right > collisionRectangle.Left &&
                movable.Hitbox.Left < collisionRectangle.Left)
            {
                movable.Position = new Vector2(collisionRectangle.Left - movable.Hitbox.Width, movable.Position.Y);
                movable.Velocity = new Vector2(0, movable.Velocity.Y);
            }
            else if (movable.Velocity.X < 0 && movable.Hitbox.Left < collisionRectangle.Right &&
                     movable.Hitbox.Right > collisionRectangle.Right)
            {
                movable.Position = new Vector2(collisionRectangle.Right, movable.Position.Y);
                movable.Velocity = new Vector2(0, movable.Velocity.Y);
            }
        }
    }
}