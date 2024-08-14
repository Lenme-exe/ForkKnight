using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkKnight.Movement
{
    internal interface IMovementManager
    {
        void Move(IMovable movable, GameTime gameTime);
    }
}
