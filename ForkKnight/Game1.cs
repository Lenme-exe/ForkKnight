using System.Collections.Generic;
using ForkKnight.GameObjects;
using ForkKnight.Input;
using ForkKnight.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace ForkKnight
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #region Tilemaps

        private TileMapManager _tileMapManager;
        private TmxMap _level1;
        private Texture2D _tileset;

        #endregion

        #region Knight

        private Knight _knight;

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Tilemap

            _level1 = new TmxMap(@"Content\Levels\level1.tmx");
            _tileset = Content.Load<Texture2D>(@"Levels\" + _level1.Tilesets[0].Name);
            var tileWidth = _level1.Tilesets[0].TileWidth;
            var tileHeight = _level1.Tilesets[0].TileHeight;
            var tileSetTilesWide = _tileset.Width / tileWidth;
            _tileMapManager = new TileMapManager(_level1, _tileset, tileSetTilesWide, tileWidth, tileHeight);

            #endregion

            #region Knight

            _knight = new Knight(new List<Texture2D>()
            {
                Content.Load<Texture2D>(@"GameObjects\Knight\idle"),
                Content.Load<Texture2D>(@"GameObjects\Knight\run")
            }, new KeyboardReader());

            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _knight.Update(gameTime, _graphics);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _tileMapManager.Draw(_spriteBatch);
            _knight.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}