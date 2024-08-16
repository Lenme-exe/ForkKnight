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

namespace ForkKnight.GameObjects
{
    internal class PurpleSlime : GameObject
    {
        public PurpleSlime(
            IMovementManager movementManager, 
            ICollisionHandler collisionHandler, 
            IAnimationManager animationManager, 
            IInputReader inputReader) : base(movementManager, collisionHandler, animationManager, inputReader)
        {
            HitboxOffsetX = 4;
            HitboxOffsetY = 8;
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 24 - 4 - 5;
            var hitboxHeight = 24 - 9;

            Hitbox = new Rectangle((int)Position.X + 4, (int)Position.Y + 9, hitboxWidth, hitboxHeight);
        }
    }
}
