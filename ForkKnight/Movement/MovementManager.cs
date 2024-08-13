using System;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Movement
{
    internal class MovementManager : IMovementManager
    {
        private const float Gravity = 0.5f;
        private const float MaxFallSpeed = 10f;
        private readonly IJumpManager _jumpManager;

        public MovementManager(IJumpManager jumpManager)
        {
            _jumpManager = jumpManager;
        }

        public void Move(IMovable movable, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            _jumpManager.HandleJump(movable, gameTime);

            var direction = movable.InputReader.ReadInput();

            movable.Velocity = new Vector2(direction.X * movable.MovementSpeed, movable.Velocity.Y);

            if (direction.X != 0)
                movable.CurrentAnimation = CurrentAnimation.Run;
            else
                movable.CurrentAnimation = CurrentAnimation.Idle;

            if (direction.X > 0)
                movable.Direction = Direction.Right;
            else if (direction.X < 0)
                movable.Direction = Direction.Left;

            var futurePosition = movable.Position + movable.Velocity;

            //if (IsWithinScreenBoundaries(futurePosition, graphics))
            //{
                movable.Position = futurePosition;
                movable.IsFalling = true;
            //}
            ApplyGravity(movable);
        }

        public void ApplyGravity(IMovable movable)
        {
            if (movable.IsFalling)
                movable.Velocity = new Vector2(movable.Velocity.X, Math.Min(movable.Velocity.Y + Gravity, MaxFallSpeed));
            else
                movable.Velocity = new Vector2(movable.Velocity.X, 0);
        }

        private bool IsWithinScreenBoundaries(Vector2 position, GraphicsDeviceManager graphics)
        {
            var screenWidth = graphics.PreferredBackBufferWidth - 32;

            return position.X >= 0 && position.X <= screenWidth;
        }
    }
}
