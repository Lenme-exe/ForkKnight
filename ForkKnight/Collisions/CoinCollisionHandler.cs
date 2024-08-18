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
    internal class CoinCollisionHandler : IPlayerPickupCollisionHandler
    {
        public ICollisionResponder CollisionResponder { get; }
        
        public CoinCollisionHandler(ICollisionResponder collisionResponder)
        {
            CollisionResponder = collisionResponder;
        }

        public void CheckCollision(Pickup pickup, GameObject player)
        {
            if (player.Hitbox.Intersects(pickup.Hitbox))
            {
                CollisionResponder.RespondToCollision(pickup, player.Hitbox);
            }
        }
    }
}
