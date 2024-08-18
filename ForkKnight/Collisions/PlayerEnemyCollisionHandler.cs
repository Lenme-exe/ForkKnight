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
    internal class PlayerPlayerEnemyCollisionHandler : IPlayerEnemyCollisionHandler
    {
        private readonly ICollisionResponder _collisionResponder;

        public PlayerPlayerEnemyCollisionHandler(ICollisionResponder collisionResponder)
        {
            _collisionResponder = collisionResponder;
        }

        public void CheckCollision(Enemy enemy, GameObject player)
        {
            if (player.Hitbox.Intersects(enemy.Hitbox))
            {
                _collisionResponder.RespondToCollision(player, enemy.Hitbox);

                _collisionResponder.RespondToCollision(enemy, player.Hitbox);
            }
        }
    }
}
