using ForkKnight.Collisions.Responders;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkKnight.Collisions
{
    internal class CoinCollisionHandler : IPickupCollisionHandler
    {
        public ICollisionResponder CollisionResponder { get; }
        
        public CoinCollisionHandler(ICollisionResponder collisionResponder)
        {
            CollisionResponder = collisionResponder;
        }

        public void CheckCollision(GameObject player, List<Pickup> coins)
        {
            foreach (var coin in coins)
            {
                if (player.Hitbox.Intersects(coin.Hitbox))
                {
                    Debug.WriteLine("collided");
                    CollisionResponder.RespondToCollision(coin, player.Hitbox);

                    break;
                }
            }
        }
    }
}
