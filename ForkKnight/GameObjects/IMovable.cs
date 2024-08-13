using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Input;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    public enum CurrentAnimation
    {
        Idle,
        Run, 
        Hit,
        Death
    }

    public enum Direction
    {
        Left, 
        Right
    }
    internal interface IMovable: ICollidable
    {
        Vector2 Position { get; set; }

        Vector2 Velocity { get; set; }

        IInputReader InputReader { get; set; }

        CurrentAnimation CurrentAnimation { get; set; }

        Direction Direction { get; set; }

        bool IsFalling { get; set; }

        bool IsJumping { get; set; }
    }
}
