using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal class Coin : IGameObject
    {
        public Rectangle Hitbox { get; set; }

        private Vector2 _position;
        private Animation _animation;
        private int _width = 16;
        private int _height = 16;

        public Coin(Texture2D texture, Vector2 position)
        {
            _position = position;
            _animation = new Animation(texture, _width, _height);

            Hitbox = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
        }

        public void Update(GameTime gameTime)
        {
            _animation.Update(gameTime);
            UpdateHitbox();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animation.Draw(spriteBatch, _position, gameTime);
        }
        public void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
        }
    }
}

