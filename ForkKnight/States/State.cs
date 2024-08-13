using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.States
{
    public abstract class State
    {
        protected ContentManager _contentManager;

        protected GraphicsDevice _graphicsDevice;

        protected Game1 _game;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
        }

        public abstract void Update(GameTime gameTime);
    }
}
