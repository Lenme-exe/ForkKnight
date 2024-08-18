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
            JumpStrength = -10;
            HitboxOffsetX = 4;
            HitboxOffsetY = 4;
            MaxSpeed = 3f;
            Acceleration = 2f;
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 16 - 5 - 5;
            var hitboxHeight = 16 - 5 - 5;

            Hitbox = new Rectangle((int)Position.X + 5, (int)Position.Y + 5, hitboxWidth, hitboxHeight);
        }
    }
}
