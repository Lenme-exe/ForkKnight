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
            // If there is any intersection between the game object's hitbox and the collision rectangle, handle the collision
            if (gameObject.Hitbox.Intersects(collisionRectangle))
            {
                // Respond to the collision
                HandleCoinCollision(gameObject);
            }
        }

        private void HandleCoinCollision(Coin gameObject)
        {
            // Handle the coin collection logic here
            // For example, you could destroy the coin or trigger some other event
            Console.WriteLine("collected coin");
            gameObject = null;
        }
    }

}
