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
    internal class EnemyCollisionHandler : IEnemyCollisionHandler
    {
        private readonly IEnemyCollisionResponder _collisionResponder;

        public EnemyCollisionHandler(IEnemyCollisionResponder collisionResponder)
        {
            _collisionResponder = collisionResponder;
        }

        public void CheckCollision(GameObject player, List<GameObject> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (player.Hitbox.Intersects(enemy.Hitbox))
                {
                    _collisionResponder.RespondToCollision(player, enemy.Hitbox);

                    _collisionResponder.RespondToCollision(enemy, player.Hitbox);


                    break;
                }
            }
        }
    }
}
