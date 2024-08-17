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
        private Vector2 _direction = new Vector2(1, 0);
        private bool _collidedLeft;
        private bool _collidedRight;

        public Vector2 ReadInput()
        {
            if (_collidedLeft)
            {
                _direction = new Vector2(1, 0);
            }
            else if (_collidedRight)
            {
                _direction = new Vector2(-1, 0);
            }

            return _direction;
        }

        public bool IsJumping()
        {
            return false;
        }

        public void SetCollision(bool collidedLeft, bool collidedRight)
        {
            _collidedLeft = collidedLeft;
            _collidedRight = collidedRight;
        }
    }
}
