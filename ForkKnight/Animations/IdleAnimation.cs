using ForkKnight.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForkKnight.Animations
{
    internal class IdleAnimation : Animation
    {
        public IdleAnimation() { }

        public IdleAnimation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    for (int i = 480; i > 480 - (32 * 4); i -= 32)
                    {
                        AddFrame(new AnimationFrame(new Rectangle(i, 0, 32, 32)));
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < 32 * 4; i += 32)
                    {
                        AddFrame(new AnimationFrame(new Rectangle(i, 0, 32, 32)));
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
