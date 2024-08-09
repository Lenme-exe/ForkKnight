using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Input;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    internal interface IMovable
    {
        Vector2 Position { get; set; }

        Vector2 Velocity { get; set; }

        IInputReader InputReader { get; set; }
    }
}
