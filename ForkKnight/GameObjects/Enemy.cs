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
    internal abstract class Enemy :GameObject
    {
        public GameObject Player { get; private set; }

        private IPlayerEnemyCollisionHandler _enemyCollisionHandler;

        public Enemy(
            IMovementManager movementManager, 
            ICollisionHandler collisionHandler, 
            IAnimationManager animationManager, 
            IInputReader inputReader,
            IPlayerEnemyCollisionHandler playerEnemyCollisionHandler,
            GameObject player) : base(movementManager, collisionHandler, animationManager, inputReader)
        {
            _enemyCollisionHandler = playerEnemyCollisionHandler;
            Player = player;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Player != null)
            {
                _enemyCollisionHandler.CheckCollision(this, Player);
            }
        }
    }
}
