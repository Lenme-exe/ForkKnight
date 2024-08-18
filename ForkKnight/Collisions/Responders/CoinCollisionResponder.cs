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
    internal class CoinCollisionResponder : ICollisionResponder
    {
        public void RespondToCollision(GameObject gameObject, Rectangle collisionRectangle)
        {
            throw new NotImplementedException();
        }

        public void RespondToCollision(Pickup pickup, Rectangle collisionRectangle)
        {
            if (!pickup.Hitbox.Intersects(collisionRectangle)) return;
            pickup.Destroy();
        }
    }

}
