using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ForkKnight.Input
{
    internal class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            var state = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (state.IsKeyDown(Keys.D))
                direction.X += 1;

            return direction;
        }

        public bool IsJumping()
        {
            var state = Keyboard.GetState();

            return state.IsKeyDown(Keys.Space);
        }
    }
}
