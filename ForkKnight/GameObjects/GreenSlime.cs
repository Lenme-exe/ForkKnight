using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    internal class GreenSlime : Enemy
    {
        private List<Rectangle> _limitRectangles;
        public GreenSlime(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader,
            IPlayerEnemyCollisionHandler playerCollisionHandler,
            GameObject player,
            List<Rectangle> limitBoxes) : base(movementManager, collisionHandler, animationManager, inputReader, playerCollisionHandler, player)
        {
            _limitRectangles = limitBoxes;
            HitboxOffsetX = 4;
            HitboxOffsetY = 8;
            Acceleration = 10f;
            MaxSpeed = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            bool collidedLeft = CheckCollisionWithLeftBoundary();
            bool collidedRight = CheckCollisionWithRightBoundary();

            if (InputReader is GreenSlimeMovement inputReader)
                inputReader.SetCollision(collidedLeft, collidedRight);

            base.Update(gameTime);
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 24 - 5 - 5;
            var hitboxHeight = 24 - 9;

            Hitbox = new Rectangle((int)Position.X + 5, (int)Position.Y + 9, hitboxWidth, hitboxHeight);
        }

        private bool CheckCollisionWithLeftBoundary()
        {
            foreach (var rect in _limitRectangles)
            {
                if (!rect.Intersects(Hitbox)) continue;
                if (!(Velocity.X < 0) || Hitbox.Left >= rect.Right ||
                    Hitbox.Right <= rect.Right) continue;
                Position = new Vector2(rect.Right, Position.Y);
                Velocity = new Vector2(0, Velocity.Y);
                return true;
            }
            return false;
        }

        private bool CheckCollisionWithRightBoundary()
        {
            foreach (var rect in _limitRectangles)
            {
                if (!rect.Intersects(Hitbox)) continue;
                if (!(Velocity.X > 0) || Hitbox.Right <= rect.Left ||
                    Hitbox.Left >= rect.Left) continue;
                Position = new Vector2(rect.Left - Hitbox.Width - HitboxOffsetX, Position.Y);
                Velocity = new Vector2(0, Velocity.Y);
                return true;
            }
            return false;
        }
    }
}
