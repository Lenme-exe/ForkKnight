using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ForkKnight.Input
{
    internal class GreenSlimeMovement : IInputReader
    {
        public Vector2 ReadInput()
        {
            var direction = Vector2.Zero;

            return direction;
        }

        public bool IsJumping()
        {
            return false;
        }
    }
}
