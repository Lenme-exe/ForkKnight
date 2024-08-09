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
    internal class Knight : IGameObject
    {
        private Texture2D _texture;
        private Animation _animation;

        public Knight(Texture2D texture)
        {
            _texture = texture;
            _animation = new Animation();
            _animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 4, 1);
        }

        public void Update(GameTime gameTime)
        {
            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(0, 0), _animation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
