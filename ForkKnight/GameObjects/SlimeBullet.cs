using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.Input;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    internal class SlimeBullet : Enemy
    {
        public SlimeBullet(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader,
            IPlayerEnemyCollisionHandler playerCollisionHandler,
            GameObject player) : base(movementManager, collisionHandler, animationManager, inputReader, playerCollisionHandler, player)
        {
            Velocity = Vector2.One;
        }
    }
}
