﻿using System;
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
        private readonly IEnemyCollisionHandler _enemyCollisionHandler;
        private readonly List<GameObject> _enemies;
        public Knight(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader,
            IEnemyCollisionHandler enemyCollisionHandler,
            List<GameObject> enemies) : base(movementManager, collisionHandler, animationManager, inputReader)
        {
            HitboxOffsetX = 9;
            HitboxOffsetY = 9;
            _enemyCollisionHandler = enemyCollisionHandler;
            _enemies = enemies;
            Acceleration = 5f;
            MaxSpeed = 3f;
            JumpStrength = -10f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _enemyCollisionHandler.CheckCollision(this, _enemies);
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 32 - 10 - 10;
            var hitboxHeight = 32 - 10 - 4;

            Hitbox = new Rectangle((int)Position.X + 10, (int)Position.Y +10, hitboxWidth, hitboxHeight);
        }
    }
}
