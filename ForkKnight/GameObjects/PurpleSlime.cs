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
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal class PurpleSlime : Enemy
    {
        private readonly IMovementManager _movementManager;
        private readonly IPlayerEnemyCollisionHandler _playerCollisionHandler;

        public PurpleSlime(
            IMovementManager movementManager, 
            ICollisionHandler collisionHandler, 
            IAnimationManager animationManager, 
            IInputReader inputReader,
            IPlayerEnemyCollisionHandler playerCollisionHandler,
            GameObject player) : base(movementManager, collisionHandler, animationManager, inputReader, playerCollisionHandler, player)
        {
            _movementManager = movementManager;
            _playerCollisionHandler = playerCollisionHandler;

            HitboxOffsetX = 4;
            HitboxOffsetY = 8;
        }

        public override void Update(GameTime gameTime)
        {
            var isPlayerLeft = CheckPlayerIsLeft();

            if (InputReader is PurpleSlimeMovement inputReader)
                inputReader.IsPlayerLeft(isPlayerLeft);

            base.Update(gameTime);
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 24 - 4 - 5;
            var hitboxHeight = 24 - 9;

            Hitbox = new Rectangle((int)Position.X + 4, (int)Position.Y + 9, hitboxWidth, hitboxHeight);
        }

        private bool CheckPlayerIsLeft()
        {
            return Player.Hitbox.Left + Player.Hitbox.Width/ 2 < Hitbox.Left + Hitbox.Width / 2;
        }

        private const float _delay = 2;
        private float _remainingDelay = _delay;

        public SlimeBullet Shoot(Texture2D texture, ICollisionHandler collisionHandler, GameTime gameTime)
        {
            if (IsDestroyed)
                return null;

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _remainingDelay -= timer;

            if (!(_remainingDelay <= 0)) return null;
            _remainingDelay = _delay;

            var slimeBulletAnimation = new Animation(texture, 16, 16);

            var slimeBulletAnimationManager = new AnimationManager(new Dictionary<CurrentAnimation, Animation>()
            {
                {
                    CurrentAnimation.Idle, slimeBulletAnimation
                },
                {
                    CurrentAnimation.Run, slimeBulletAnimation
                }
            });

            var slimeBulletVelocity = CheckPlayerIsLeft() ? new Vector2(-3, -5) : new Vector2(3, -5);

            var slimeBullet = new SlimeBullet(_movementManager, collisionHandler, slimeBulletAnimationManager, new PurpleSlimeMovement(),
                _playerCollisionHandler, Player)
            {
                Position = Position,
                Velocity = slimeBulletVelocity
            };
            return slimeBullet;

        }
    }
}
