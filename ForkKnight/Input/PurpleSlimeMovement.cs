using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ForkKnight.Input
{
    internal class PurpleSlimeMovement : IInputReader
    {
        private bool _playerLeft;
        public Vector2 ReadInput()
        {
            var direction = Vector2.Zero;
            if (_playerLeft)
                direction = new Vector2(-1, 0);
            else
                direction = new Vector2(1, 0);

            return direction;
        }

        public bool IsJumping()
        {
            return false;
        }

        public void IsPlayerLeft(bool playerLeft)
        {
            _playerLeft = playerLeft;
        }
    }
}
