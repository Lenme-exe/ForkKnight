using System;
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

            var distance = direction * movable.Velocity;
            var futurePosition = movable.Position + distance;

            if (IsWithinScreenBoundaries(futurePosition, graphics))
                movable.Position = futurePosition;
        }

        private bool IsWithinScreenBoundaries(Vector2 position, GraphicsDeviceManager graphics)
        {
            var screenWidth = graphics.PreferredBackBufferWidth - 16;
            var screenHeight = graphics.PreferredBackBufferHeight - 16;

            return (position.X >= 0 && position.X <= screenWidth && position.Y >= 0 && position.Y <= screenHeight);
        }
    }
}
