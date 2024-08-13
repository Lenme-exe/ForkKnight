using System;
using System.Collections.Generic;
using ForkKnight.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.States
{
    internal class DeathState : State
    {
        private List<Component> _components;
        public DeathState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var buttonTexture = _contentManager.Load<Texture2D>("");
            var font = _contentManager.Load<SpriteFont>("");

            var restartButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(300, 200),
                Text = "Restart",
            };

            restartButton.Click += RestartButtonOnClick;

            _components = new List<Component>()
            {
                restartButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var c in _components)
                c.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void RestartButtonOnClick(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
        }
    }
}
