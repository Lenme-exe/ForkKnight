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
        Vector2 Position { get; set; }
        bool IsDestroyed { get; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void Destroy();
    }
}
