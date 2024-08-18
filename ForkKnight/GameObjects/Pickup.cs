using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal abstract class Pickup : IGameObject, ICollidable
    {
        public Rectangle Hitbox { get; set; }
        public int HitboxOffsetX { get; set; }
        public int HitboxOffsetY { get; set; }
        public Vector2 Position { get; set; }

        public GameObject Player { get; private set; }
        public bool IsDestroyed { get; private set; }

        private readonly IAnimationManager _animationManager;
        private readonly IPlayerPickupCollisionHandler _playerPlayerPickupCollisionHandler;

        protected Pickup(IAnimationManager animationManager, GameObject player, IPlayerPickupCollisionHandler playerPlayerPickupCollisionHandler)
        {
            IsDestroyed = false;
            Player = player;
            _animationManager = animationManager;
            _playerPlayerPickupCollisionHandler = playerPlayerPickupCollisionHandler;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!IsDestroyed)
            {
                _animationManager.Update(this, gameTime);
                _playerPlayerPickupCollisionHandler.CheckCollision(this, Player);
                UpdateHitbox();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsDestroyed)
                _animationManager.Draw(spriteBatch, this, gameTime);
        }
        public virtual void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y,
                _animationManager.CurrentAnimation.CurrentFrame.SourceRectangle.Width,
                _animationManager.CurrentAnimation.CurrentFrame.SourceRectangle.Height);
        }

        public virtual void Destroy()
        {
            Hitbox = Rectangle.Empty;
            IsDestroyed = true;
        }
    }
}
