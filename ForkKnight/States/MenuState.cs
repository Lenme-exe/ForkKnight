using System;
using System.Collections.Generic;
using ForkKnight.Components;
using ForkKnight.Components.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.States
{
    internal class MenuState : State
    {
        private List<Component> _components;
        private SpriteFont _font;
        private Texture2D _background;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var buttonTexture = _contentManager.Load<Texture2D>(@"UI\button");
            _font = _contentManager.Load<SpriteFont>(@"UI\font");

            _background = _contentManager.Load<Texture2D>(@"background");

            var image = _contentManager.Load<Texture2D>(@"UI\main-menu-sprite");

            var startButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width / 2 - buttonTexture.Width / 2, 250),
                Text = "Play",
            };

            var quitButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width / 2 - buttonTexture.Width / 2, 320),
                Text = "Quit",
            };

            var mainImage = new Image(image)
            {
                DestinationRectangle = new Rectangle(_graphicsDevice.Viewport.Width / 2 - 64, 120, 128, 128),
            };

            startButton.Click += StartButtonOnClick;
            quitButton.Click += QuitButtonOnClick;

            _components = new List<Component>()
            {
                startButton,
                quitButton,
                mainImage
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

            spriteBatch.DrawString(_font, "ForkKnight", new Vector2(_graphicsDevice.Viewport.Width / 2 - _font.MeasureString("Game Over").Length() / 2, 120), Color.White);

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

        private void StartButtonOnClick(object sender, EventArgs e)
        {
            _game.ChangeState(new Level1State(_game, _graphicsDevice, _contentManager));
        }

        private void QuitButtonOnClick(object sender, EventArgs e)
        {
            this._game.Exit();
        }
    }
}
