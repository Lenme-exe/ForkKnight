using System;
using System.Collections.Generic;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.Input;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal class Knight : GameObject
    {
        public Knight(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader) : base(movementManager, collisionHandler, animationManager, inputReader)
        {
            HitboxOffsetX = 9;
            HitboxOffsetY = 9;
            Acceleration = 5f;
            MaxSpeed = 3f;
            JumpStrength = -10f;
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 32 - 10 - 10;
            var hitboxHeight = 32 - 10 - 4;

            Hitbox = new Rectangle((int)Position.X + 10, (int)Position.Y +10, hitboxWidth, hitboxHeight);
        }
    }
}
