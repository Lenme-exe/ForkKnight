using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions.Responders
{
    internal class SlimeBulletSolidObjectCollisionResponder : ICollisionResponder
    {
        public void RespondToCollision(GameObject gameObject, Rectangle collisionRectangle)
        {
            var intersection = Rectangle.Intersect(gameObject.Hitbox, collisionRectangle);

            if (intersection.Height < intersection.Width)
            {
                if (gameObject.Velocity.Y > 0 && gameObject.Hitbox.Bottom > collisionRectangle.Top &&
                    gameObject.Hitbox.Top < collisionRectangle.Top)
                {
                    gameObject.Destroy();
                }
                else if (gameObject.Velocity.Y < 0 && gameObject.Hitbox.Top < collisionRectangle.Bottom &&
                         gameObject.Hitbox.Bottom > collisionRectangle.Bottom)
                {
                    gameObject.Destroy();
                }
            }
            else if (intersection.Width < intersection.Height)
            {
                if (gameObject.Velocity.X > 0 && gameObject.Hitbox.Right > collisionRectangle.Left &&
                    gameObject.Hitbox.Left < collisionRectangle.Left)
                {
                    gameObject.Destroy();
                }
                else if (gameObject.Velocity.X < 0 && gameObject.Hitbox.Left < collisionRectangle.Right &&
                         gameObject.Hitbox.Right > collisionRectangle.Right)
                {
                    gameObject.Destroy();
                }
            }
        }

        public void RespondToCollision(Pickup pickup, Rectangle collisionRectangle)
        {
            throw new NotImplementedException();
        }
    }
}
