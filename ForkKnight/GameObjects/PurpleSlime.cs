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
    internal class PurpleSlime : Enemy
    {
        public PurpleSlime(
            IMovementManager movementManager, 
            ICollisionHandler collisionHandler, 
            IAnimationManager animationManager, 
            IInputReader inputReader,
            IPlayerEnemyCollisionHandler playerCollisionHandler,
            GameObject player) : base(movementManager, collisionHandler, animationManager, inputReader, playerCollisionHandler, player)
        {
            HitboxOffsetX = 4;
            HitboxOffsetY = 8;
        }

        public override void Update(GameTime gameTime)
        {
            bool isPlayerLeft = CheckPlayerIsLeft();

            if (InputReader is PurpleSlimeMovement inputReader)
                inputReader.IsPlayerLeft(isPlayerLeft);

            base.Update(gameTime);
        }

        public override void UpdateHitbox()
        {
            var hitboxWidth = 24 - 4 - 5;
            var hitboxHeight = 24 - 9;

            Hitbox = new Rectangle((int)Position.X + 4, (int)Position.Y + 9, hitboxWidth, hitboxHeight);
        }

        private bool CheckPlayerIsLeft()
        {
            if (Player.Hitbox.Left + Player.Hitbox.Width/ 2 < Hitbox.Left + Hitbox.Width / 2)
            {
                return true;
            }
            return false;
        }
    }
}
