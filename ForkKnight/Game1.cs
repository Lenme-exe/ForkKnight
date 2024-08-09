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

        private Texture2D _knightTexture;
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

            _knight = new Knight(_knightTexture, new KeyboardReader());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _level1 = new TmxMap(@"Content\Levels\level1.tmx");
            _tileset = Content.Load<Texture2D>(@"Levels\" + _level1.Tilesets[0].Name);
            var tileWidth = _level1.Tilesets[0].TileWidth;
            var tileHeight = _level1.Tilesets[0].TileHeight;
            var tileSetTilesWide = _tileset.Width / tileWidth;
            _tileMapManager = new TileMapManager(_level1, _tileset, tileSetTilesWide, tileWidth, tileHeight);

            _knightTexture = Content.Load<Texture2D>(@"GameObjects\Knight\idle");
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
            _knight.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}