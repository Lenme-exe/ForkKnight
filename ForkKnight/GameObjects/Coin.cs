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

        private readonly Vector2 _position;
        private Animation _animation;

        public Coin(Texture2D texture, Vector2 position)
        {
            _position = position;
            _animation = new Animation(texture, 16, 16);
        }

        public void Update(GameTime gameTime, List<Rectangle> collisionRects)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animation.Draw(spriteBatch, _position, gameTime);
        }
    }
}
