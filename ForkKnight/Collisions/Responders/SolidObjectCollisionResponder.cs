using ForkKnight.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions.Responders
{
    internal class SolidObjectCollisionResponder : IEnemyCollisionResponder
    {
        public void RespondToCollision(GameObject gameObject, Rectangle collisionRectangle)
        {
            var intersection = Rectangle.Intersect(gameObject.Hitbox, collisionRectangle);

            if (intersection.Height < intersection.Width)
            {
                if (gameObject.Velocity.Y > 0 && gameObject.Hitbox.Bottom > collisionRectangle.Top &&
                    gameObject.Hitbox.Top < collisionRectangle.Top)
                {
                    gameObject.Position = new Vector2(gameObject.Position.X, collisionRectangle.Top - gameObject.Hitbox.Height - gameObject.HitboxOffsetY); //kanker
                    gameObject.Velocity = new Vector2(gameObject.Velocity.X, 0);
                    gameObject.IsFalling = false;
                }
                else if (gameObject.Velocity.Y < 0 && gameObject.Hitbox.Top < collisionRectangle.Bottom &&
                         gameObject.Hitbox.Bottom > collisionRectangle.Bottom)
                {
                    gameObject.Position = new Vector2(gameObject.Position.X, collisionRectangle.Bottom);
                    gameObject.Velocity = new Vector2(gameObject.Velocity.X, 0);
                }
            }
            else if (intersection.Width < intersection.Height)
            {
                if (gameObject.Velocity.X > 0 && gameObject.Hitbox.Right > collisionRectangle.Left &&
                    gameObject.Hitbox.Left < collisionRectangle.Left)
                {
                    gameObject.Position = new Vector2(collisionRectangle.Left - gameObject.Hitbox.Width - gameObject.HitboxOffsetX, gameObject.Position.Y); //kanker
                    gameObject.Velocity = new Vector2(0, gameObject.Velocity.Y);
                }
                else if (gameObject.Velocity.X < 0 && gameObject.Hitbox.Left < collisionRectangle.Right &&
                         gameObject.Hitbox.Right > collisionRectangle.Right)
                {
                    gameObject.Position = new Vector2(collisionRectangle.Right - gameObject.HitboxOffsetX, gameObject.Position.Y);
                    gameObject.Velocity = new Vector2(0, gameObject.Velocity.Y);
                }
            }
        }
    }
}
