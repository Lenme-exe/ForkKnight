using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.GameObjects;
using ForkKnight.Input;
using ForkKnight.Levels;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using TiledSharp;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ForkKnight.States
{
    internal class GameState: State
    {
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

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            #region Tilemap

            _level1 = new TmxMap(@"Content\Levels\level1.tmx");
            _tileset = _contentManager.Load<Texture2D>(@"Levels\" + _level1.Tilesets[0].Name);
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
                    _collisionRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                }
            }

            #endregion

            #region Animations

            var idleTexture = _contentManager.Load<Texture2D>(@"GameObjects\Knight\idle");
            var runTexture = _contentManager.Load<Texture2D>(@"GameObjects\Knight\run");

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _tileMapManager.Draw(spriteBatch);
            _knight.Draw(spriteBatch, gameTime);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _knight.Update(gameTime, _collisionRects);

            if (_knight.Position.Y > _graphicsDevice.Viewport.Height + 100)
            {
                _game.ChangeState(new DeathState(_game, _graphicsDevice, _contentManager));
            }
        }
    }
}
