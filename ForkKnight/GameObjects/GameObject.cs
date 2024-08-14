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
    internal abstract class GameObject : IGameObject, IMovable
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public IInputReader InputReader { get; set; }
        public CurrentAnimation CurrentAnimation { get; set; }
        public Direction Direction { get; set; }
        public float Acceleration { get; set; }
        public float MaxSpeed { get; set; }
        public float JumpStrength { get; set; }
        public bool IsFalling { get; set; }
        public bool IsJumping { get; set; }

        private readonly IMovementManager _movementManager;
        private readonly ICollisionHandler _collisionHandler;
        private readonly IAnimationManager _animationManager;

        public GameObject(IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader)
        {
            _movementManager = movementManager;
            _collisionHandler = collisionHandler;
            _animationManager = animationManager;
            InputReader = inputReader;

            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime, List<Rectangle> collisionRects)
        {
            _movementManager.Move(this, gameTime);
            _collisionHandler.CheckCollision(this, collisionRects);
            _animationManager.Update(this, gameTime);
            UpdateHitbox();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationManager.Draw(spriteBatch, this, gameTime);
        }

        public virtual void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 
                _animationManager.CurrentAnimation.CurrentFrame.SourceRectangle.Width,
                _animationManager.CurrentAnimation.CurrentFrame.SourceRectangle.Height);
        }
    }
}
