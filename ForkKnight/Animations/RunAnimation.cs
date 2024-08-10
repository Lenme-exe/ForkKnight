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
    internal class RunAnimation : Animation
    {
        public RunAnimation() { }

        public RunAnimation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    for (int row = 0; row <2; row++)
                    {
                        for (int i = 480; i > 480 - (32 * 4); i -= 32)
                        {
                            AddFrame(new AnimationFrame(new Rectangle(i, 64 + (32 * row), 32, 32)));
                        }
                    }
                    break;
                case Direction.Right:
                    for(int row = 0; row < 2; row++)
                    {
                        for (int i = 0; i < 32 * 8; i += 32)
                        {
                            AddFrame(new AnimationFrame(new Rectangle(i, 64 + (32 * row), 32, 32)));
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
