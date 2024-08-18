using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.GameObjects;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions.Responders
{
    internal class PlayerEnemyCollisionResponder : ICollisionResponder
    {
        private bool _jumpedOnEnemyHead = false;
        public void RespondToCollision(GameObject gameObject, Rectangle collisionRectangle)
        {
            if (gameObject.GetType() == typeof(Knight))
            {
                var intersection = Rectangle.Intersect(gameObject.Hitbox, collisionRectangle);

                if (intersection.Height < intersection.Width)
                {
                    if (gameObject.Velocity.Y > 0 && gameObject.Hitbox.Bottom > collisionRectangle.Top &&
                        gameObject.Hitbox.Top < collisionRectangle.Top)
                    {
                        _jumpedOnEnemyHead = true;
                        gameObject.Position = new Vector2(gameObject.Position.X, collisionRectangle.Top - gameObject.Hitbox.Height);
                        gameObject.Velocity = new Vector2(gameObject.Velocity.X, 0);
                        gameObject.IsFalling = false;
                    }
                    else
                    {
                        gameObject.Destroy();
                    }
                }
                else
                {
                    gameObject.Destroy();
                }
            }
            else if (gameObject.GetType() != typeof(Knight) && _jumpedOnEnemyHead)
            {
                gameObject.Destroy();
            }
        }

        public void RespondToCollision(Pickup pickup, Rectangle collisionRectangle)
        {
            throw new NotImplementedException();
        }
    }
}
