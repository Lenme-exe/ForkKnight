using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Collisions;
using ForkKnight.Collisions.Responders;
using ForkKnight.GameObjects;
using ForkKnight.Input;
using ForkKnight.Levels;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using TiledSharp;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ForkKnight.States
{
    internal class Level1State : State
    {
        #region Tilemaps

        private TileMapManager _tileMapManager;
        private Texture2D _background;

        #endregion

        #region Knight

        private Knight _knight;

        #endregion

        #region Enemies

        private List<GameObject> _greenSlimes;

        #endregion

        #region Coins

        private List<Coin> _coins;

        #endregion


        public Level1State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            #region Tilemap

            var level1 = new TmxMap(@"Content\Levels\level1.tmx");
            var tileset = _contentManager.Load<Texture2D>(@"Levels\" + level1.Tilesets[0].Name);
            _background = _contentManager.Load<Texture2D>(@"background");
            var tileWidth = level1.Tilesets[0].TileWidth;
            var tileHeight = level1.Tilesets[0].TileHeight;
            var tileSetTilesWide = tileset.Width / tileWidth;
            _tileMapManager = new TileMapManager(level1, tileset, tileSetTilesWide, tileWidth, tileHeight);

            #endregion

            #region Collisions

            var collisionRects = new List<Rectangle>();

            foreach (var o in level1.ObjectGroups["Collision"].Objects)
                collisionRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));

            #endregion

            #region Animations

            var idleTexture = _contentManager.Load<Texture2D>(@"GameObjects\Knight\idle");
            var runTexture = _contentManager.Load<Texture2D>(@"GameObjects\Knight\run");

            var idleAnimation = new Animation(idleTexture, 32, 32);
            var runAnimation = new Animation(runTexture, 32, 32);

            var knightAnimations = new Dictionary<CurrentAnimation, Animation>
            {
                { CurrentAnimation.Idle, idleAnimation },
                { CurrentAnimation.Run, runAnimation }
            };

            var greenSlimeSheet = _contentManager.Load<Texture2D>(@"GameObjects\GreenSlime\sheet");

            var greenSlimeAnimations = new Dictionary<CurrentAnimation, Animation>
            {
                {
                    CurrentAnimation.Idle,
                    new Animation(greenSlimeSheet, 24, 24)
                },
                {
                    CurrentAnimation.Run,
                    new Animation(greenSlimeSheet, 24, 24)
                },
            };

            IAnimationManager slimeAnimationManager = new AnimationManager(greenSlimeAnimations);

            IAnimationManager knightAnimationManager = new AnimationManager(knightAnimations);

            #endregion

            #region MovementManager and CollisionHandler

            var movementManager = new MovementManager(new JumpManager());
            var collisionHandler = new CollisionHandler(new SolidObjectCollisionResponder(), collisionRects);
            var enemyCollisionHandler = new EnemyCollisionHandler(new EnemyCollisionResponder());
            var coinCollisionHandler = new CoinCollisionHandler(new CoinCollisionResponder());

            #endregion

            #region Enemies

            _greenSlimes = new List<GameObject>();

            var limitBoxes = new List<Rectangle>();

            foreach (var rect in level1.ObjectGroups["EnemyLimit"].Objects)
            {
                limitBoxes.Add(new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height));
            }

            foreach (var o in level1.ObjectGroups["GreenSlime"].Objects)
            {
                _greenSlimes.Add(new GreenSlime(movementManager, collisionHandler, slimeAnimationManager,
                    new GreenSlimeMovement(), limitBoxes)
                {
                    Position = new Vector2((int)o.X, (int)o.Y - (int)o.Height)
                });
            }

            #endregion

            #region coins

            _coins = new List<Coin>();

            foreach (var o in level1.ObjectGroups["Coins"].Objects)
            {
                var coinPosition = new Vector2((int)o.X, (int)o.Y - (int)o.Height);

                var coinTexture = contentManager.Load<Texture2D>(@"GameObjects/Coin/coin");
                _coins.Add(new Coin(coinTexture, coinPosition));
            }

            #endregion

            #region Knight

            var spawnPosKnight = Vector2.One;

            foreach (var o in level1.ObjectGroups["Spawn"].Objects)
                spawnPosKnight = new Vector2((int)o.X, (int)o.Y - (int)o.Height);

            _knight = new Knight(
                movementManager,
                collisionHandler,
                knightAnimationManager,
                new KeyboardReader(),
                enemyCollisionHandler,
                coinCollisionHandler,
                _coins,
                _greenSlimes)
            {
                Position = spawnPosKnight
            };

            #endregion           
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);
            _tileMapManager.Draw(spriteBatch);
            _knight.Draw(spriteBatch, gameTime);
            foreach (var greenSlime in _greenSlimes)
            {
                greenSlime.Draw(spriteBatch, gameTime);
            }
            foreach (var coin in _coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _knight.Update(gameTime);
            foreach (var greenSlime in _greenSlimes)
            {
                greenSlime.Update(gameTime);
            }

            foreach (var coin in _coins)
            {
                coin.Update(gameTime);
            }

            if (_knight.Position.Y > _graphicsDevice.Viewport.Height + 100 || _knight.IsDestroyed)
            {
                _game.ChangeState(new DeathState(_game, _graphicsDevice, _contentManager));
            }
        }
    }
}
