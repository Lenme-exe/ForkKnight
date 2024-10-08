﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Collisions;
using ForkKnight.Input;
using Microsoft.Xna.Framework;

namespace ForkKnight.Movement
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
    internal interface IMovable
    {
        Vector2 Position { get; set; }

        Vector2 Velocity { get; set; }

        IInputReader InputReader { get; set; }

        CurrentAnimation CurrentAnimation { get; set; }

        Direction Direction { get; set; }

        float Acceleration { get; set; }

        float MaxSpeed { get; set; }

        float JumpStrength { get; set; }

        bool IsFalling { get; set; }

        bool IsJumping { get; set; }
    }
}
