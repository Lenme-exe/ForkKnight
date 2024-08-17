using ForkKnight.Collisions.Responders;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkKnight.Collisions
{
    internal class CoinCollisionHandler : ICoinCollisionHandler
    {
        private readonly ICoinCollisionResponder _coincollisionResponder;

        public CoinCollisionHandler(ICoinCollisionResponder collisionResponder)
        {
            _coincollisionResponder = collisionResponder;
        }

        public void CheckCollision(GameObject player, List<Coin> coins)
        {
            foreach (var coin in coins)
            {
                if (player.Hitbox.Intersects(coin.Hitbox))
                {
                    Console.WriteLine("collided");
                    _coincollisionResponder.RespondToCollision(coin, player.Hitbox);

                    break;
                }
            }
        }
    }
}
