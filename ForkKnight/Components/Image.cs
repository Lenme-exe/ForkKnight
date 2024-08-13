using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace ForkKnight.Components
{
    internal class Image : Component
    {
        public Rectangle DestinationRectangle { get; set; }

        private Texture2D _texture;

        public Image(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            spriteBatch.Draw(_texture, DestinationRectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
