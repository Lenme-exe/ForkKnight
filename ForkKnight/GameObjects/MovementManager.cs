﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    internal class MovementManager
    {
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


            var distance = direction * movable.Velocity;
            var futurePosition = movable.Position + distance;

            if (IsWithinScreenBoundaries(futurePosition, graphics))
                movable.Position = futurePosition;
        }

        private bool IsWithinScreenBoundaries(Vector2 position, GraphicsDeviceManager graphics)
        {
            var screenWidth = graphics.PreferredBackBufferWidth - 32;
            var screenHeight = graphics.PreferredBackBufferHeight - 32;

            return (position.X >= 0 && position.X <= screenWidth && position.Y >= 0 && position.Y <= screenHeight);
        }
    }
}
