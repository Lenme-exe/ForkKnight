using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.Input;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal class GreenSlime : GameObject
    {
        public GreenSlime(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader) : base(movementManager, collisionHandler, animationManager, inputReader)
        {
            Acceleration = 2f;
            MaxSpeed = 3f;
        }

        public override void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X + 5, (int)Position.Y + 9, 24 - 5, 24);
        }
    }
}
