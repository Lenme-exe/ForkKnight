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
    internal class GreenSlime : GameObject
    {
        private List<Rectangle> _limitRectangles;
        public GreenSlime(
            IMovementManager movementManager,
            ICollisionHandler collisionHandler,
            IAnimationManager animationManager,
            IInputReader inputReader,
            List<Rectangle> limitBoxes) : base(movementManager, collisionHandler, animationManager, inputReader)
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

            // Update the enemy's input based on collisions
            var inputReader = InputReader as GreenSlimeMovement;
            if (inputReader != null)
            {
                inputReader.SetCollision(collidedLeft, collidedRight);
            }

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
            // Example logic, adjust according to your game's needs
            foreach (var rect in _limitRectangles)
            {
                if (rect.Intersects(Hitbox))
                {
                    if (Velocity.X < 0 && Hitbox.Left < rect.Right &&
                        Hitbox.Right > rect.Right)
                    {
                        Position = new Vector2(rect.Right, Position.Y);
                        Velocity = new Vector2(0, Velocity.Y);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckCollisionWithRightBoundary()
        {
            // Example logic, adjust according to your game's needs
            foreach (var rect in _limitRectangles)
            {
                if (rect.Intersects(Hitbox))
                {
                    if (Velocity.X > 0 && Hitbox.Right > rect.Left &&
                        Hitbox.Left < rect.Left)
                    {
                        Position = new Vector2(rect.Left - Hitbox.Width - HitboxOffsetX, Position.Y); //kanker
                        Velocity = new Vector2(0, Velocity.Y);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
