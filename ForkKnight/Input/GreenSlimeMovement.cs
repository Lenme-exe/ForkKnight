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
        private Vector2 _direction = new Vector2(1, 0); // Start moving right initially
        private bool _collidedLeft;
        private bool _collidedRight;

        public Vector2 ReadInput()
        {
            // If collided with left boundary, move right
            if (_collidedLeft)
            {
                _direction = new Vector2(1, 0);
            }
            // If collided with right boundary, move left
            else if (_collidedRight)
            {
                _direction = new Vector2(-1, 0);
            }

            return _direction;
        }

        public bool IsJumping()
        {
            return false; // No jumping for this enemy
        }

        public void SetCollision(bool collidedLeft, bool collidedRight)
        {
            _collidedLeft = collidedLeft;
            _collidedRight = collidedRight;
        }
    }
}
