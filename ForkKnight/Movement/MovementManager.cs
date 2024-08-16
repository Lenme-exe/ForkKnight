using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ForkKnight.Movement
{
    internal class MovementManager : IMovementManager
    {
        private const float Gravity = 0.5f;
        private const float MaxFallSpeed = 10f;
        private const float FrictionCoefficient = 0.2f;
        private readonly IJumpManager _jumpManager;

        public MovementManager(IJumpManager jumpManager)
        {
            _jumpManager = jumpManager;
        }

        public void Move(IMovable movable, GameTime gameTime)
        {
            _jumpManager.HandleJump(movable, gameTime);

            var direction = movable.InputReader.ReadInput();

            Vector2 acceleration = Vector2.Zero;

            if (direction.X != 0)
            {
                acceleration = new Vector2(direction.X * movable.Acceleration, movable.Velocity.Y);

                movable.CurrentAnimation = CurrentAnimation.Run;
            }
            else
            {
                acceleration = new Vector2(0, acceleration.Y);
                movable.Velocity = new Vector2(movable.Velocity.X * FrictionCoefficient, movable.Velocity.Y);

                if (Math.Abs(movable.Velocity.X) < 0.01f)
                {
                    movable.Velocity = new Vector2(0, movable.Velocity.Y);
                    movable.CurrentAnimation = CurrentAnimation.Idle;
                }
            }

            movable.Velocity += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            movable.Velocity = new Vector2(MathHelper.Clamp(movable.Velocity.X, -movable.MaxSpeed, movable.MaxSpeed), movable.Velocity.Y);

            if (direction.X > 0)
                movable.Direction = Direction.Right;
            else if (direction.X < 0)
                movable.Direction = Direction.Left;

            var futurePosition = movable.Position + movable.Velocity;

            movable.Position = futurePosition;
            movable.IsFalling = true;

            ApplyGravity(movable);
        }

        public void ApplyGravity(IMovable movable)
        {
            if (movable.IsFalling)
                movable.Velocity = new Vector2(movable.Velocity.X, Math.Min(movable.Velocity.Y + Gravity, MaxFallSpeed));
            else
                movable.Velocity = new Vector2(movable.Velocity.X, 0);
        }
    }
}
