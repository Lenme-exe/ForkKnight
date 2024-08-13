using System.Collections.Generic;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.GameObjects;
using ForkKnight.Input;
using ForkKnight.Levels;
using ForkKnight.Movement;
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
        private List<Rectangle> _collisionRects;
        #endregion

        #region Knight

        private Knight _knight;
        private IAnimationManager _animationManager;
        private IMovementManager _movementManager;
        private ICollisionHandler _collisionHandler;
        private IJumpManager _jumpManager;

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

            #region Collisions

            _collisionRects = new List<Rectangle>();

            foreach (var o in _level1.ObjectGroups["Collision"].Objects)
            {
                if (o.Name == "")
                {
                    _collisionRects.Add(new Rectangle((int) o.X, (int) o.Y, (int) o.Width, (int) o.Height));
                }
            }

            #endregion

            #region Animations

            var idleTexture = Content.Load<Texture2D>(@"GameObjects\Knight\idle");
            var runTexture = Content.Load<Texture2D>(@"GameObjects\Knight\run");

            var idleAnimation = new Animation(idleTexture, 32, 32);
            var runAnimation = new Animation(runTexture, 32, 32);

            var animations = new Dictionary<CurrentAnimation, Animation>
            {
                { CurrentAnimation.Idle, idleAnimation },
                { CurrentAnimation.Run, runAnimation }
            };

            _animationManager = new AnimationManager(animations);

            #endregion

            #region MovementManager and CollisionHandler

            _jumpManager = new JumpManager();
            _movementManager = new MovementManager(_jumpManager);
            _collisionHandler = new CollisionHandler();

            #endregion

            #region Knight

            _knight = new Knight(
                _movementManager,
                _collisionHandler,
                _animationManager,
                new KeyboardReader());

            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _knight.Update(gameTime, _graphics, _collisionRects);

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