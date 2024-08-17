using System;
using System.Collections.Generic;
using ForkKnight.Components;
using ForkKnight.Components.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.States
{
    internal class VictoryState : State
    {
        private List<Component> _components;
        private SpriteFont _font;
        private Texture2D _background;
        public VictoryState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var buttonTexture = _contentManager.Load<Texture2D>(@"UI\button");
            _font = _contentManager.Load<SpriteFont>(@"UI\font");

            _background = _contentManager.Load<Texture2D>(@"background");

            var image = _contentManager.Load<Texture2D>(@"UI\main-menu-sprite");

            var ContinueButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width / 2 - buttonTexture.Width / 2, 250),
                Text = "Continue",
            };
            var menuButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width / 2 - buttonTexture.Width / 2, 320),
                Text = "Main menu",
            };

            var mainImage = new Image(image)
            {
                DestinationRectangle = new Rectangle(_graphicsDevice.Viewport.Width / 2 - 64, 120, 128, 128),
            };

            ContinueButton.Click += ContinueButtonOnClick;
            menuButton.Click += MainMenuButtonOnClick;

            _components = new List<Component>()
            {
                ContinueButton,
                menuButton,
                mainImage
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

            spriteBatch.DrawString(_font, "VICTORY", new Vector2(_graphicsDevice.Viewport.Width / 2 - _font.MeasureString("Game Over").Length() / 2, 120), Color.White);

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

        private void ContinueButtonOnClick(object sender, EventArgs e)
        {
            if (_game._previousState is Level1State)
            {
                _game.ChangeState(new Level2State(_game, _graphicsDevice, _contentManager));
            }
            else if (_game._previousState is Level2State)
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _contentManager));
            }
        }
        private void MainMenuButtonOnClick(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _contentManager));
        }
    }
}
