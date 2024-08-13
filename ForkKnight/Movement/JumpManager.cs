using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Movement
{
    internal class JumpManager : IJumpManager
    {
        private readonly float _jumpStrength = -10f;
        private readonly float _gravity = 0.5f;
        public void HandleJump(IMovable movable, GameTime gameTime)
        {
            if (movable.IsJumping)
            {
                movable.Velocity += new Vector2(0, _gravity);

                if (movable.Velocity.Y > 0)
                {
                    movable.IsJumping = false;
                    movable.IsFalling = true;
                }
            }

            if (!movable.IsJumping && !movable.IsFalling && movable.InputReader.IsJumping())
            {
                movable.IsJumping = true;
                movable.Velocity = new Vector2(movable.Velocity.X, _jumpStrength);
            }
        }
    }
}
