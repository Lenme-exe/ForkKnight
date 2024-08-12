using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal interface IGameObject
    {
        void Update(GameTime gameTime, GraphicsDeviceManager graphics, List<Rectangle> collisionRects);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
