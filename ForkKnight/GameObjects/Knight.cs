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
    internal class Knight : IGameObject, IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public IInputReader InputReader { get; set; }
        public CurrentAnimation CurrentAnimation { get; set; }
        public Direction Direction { get; set; }
        public float MovementSpeed { get; set; }
        public Rectangle Hitbox { get; set; }
        public bool IsFalling { get; set; }
        public bool IsJumping { get; set; }

        private readonly IMovementManager _movementManager;
        private readonly ICollisionHandler _collisionHandler;
        private readonly IAnimationManager _animationManager;

        public Knight(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader)
        {
            _movementManager = movementManager;
            _collisionHandler = collisionHandler;
            _animationManager = animationManager;
            InputReader = inputReader;

            Position = Vector2.One;
            Velocity = Vector2.Zero;
            MovementSpeed = 3f;
            UpdateHitbox();
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics, List<Rectangle> collisionRects)
        {
            _movementManager.Move(this, gameTime, graphics);
            _collisionHandler.CheckCollision(this, collisionRects);
            _animationManager.Update(this, gameTime);
            UpdateHitbox();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationManager.Draw(spriteBatch, this, gameTime);
        }

        private void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X + 8, (int)Position.Y + 10, 32 - 10, 32 - 4);
        }
    }
}
