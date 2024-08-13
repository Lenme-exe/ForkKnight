using System;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Movement
{
    internal class MovementManager : IMovementManager
    {
        private const float Gravity = 0.5f;
        private const float MaxFallSpeed = 10f;
        private const float JumpStrength = -10f;

        public void Move(IMovable movable, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            var direction = movable.InputReader.ReadInput();

            if (direction.X != 0)
                movable.CurrentAnimation = CurrentAnimation.Run;
            else
                movable.CurrentAnimation = CurrentAnimation.Idle;

            if (direction.X > 0)
                movable.Direction = Direction.Right;
            else if (direction.X < 0)
                movable.Direction = Direction.Left;

            // Handle jumping
            if (direction.Y < 0 && !movable.IsFalling)
            {
                movable.Velocity = new Vector2(movable.Velocity.X, JumpStrength);
                movable.IsFalling = true;
            }

            var distance = direction * movable.Velocity;
            var futurePosition = movable.Position + distance;

            futurePosition.Y += movable.Velocity.Y;

            if (IsWithinScreenBoundaries(futurePosition, graphics))
            {
                movable.Position = futurePosition;
                movable.IsFalling = true;
            }
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
