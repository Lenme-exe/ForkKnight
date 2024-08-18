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
    internal class CoinCollisionResponder : ICoinCollisionResponder
    {
        public void RespondToCollision(Coin gameObject, Rectangle collisionRectangle)
        {
            Console.WriteLine("test respond to coin");
            if (gameObject.Hitbox.Intersects(collisionRectangle))
            {
                HandleCoinCollision(gameObject);
            }
        }

        private void HandleCoinCollision(Coin gameObject)
        {
            Console.WriteLine("collected coin");
            gameObject = null;
        }
    }

}
