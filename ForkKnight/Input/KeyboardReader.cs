using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ForkKnight.Input
{
    internal class KeyboardReader : IInputReader
    {
        private KeyboardState previousState;

        public Vector2 ReadInput()
        {
            var currentState = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (currentState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (currentState.IsKeyDown(Keys.D))
                direction.X += 1;

            if (currentState.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
            {
                direction.Y -= 1;
            }

            previousState = currentState; 

            return direction;
        }
    }
}
