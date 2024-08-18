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
        private List<GameObject> _purpleSlimes;
        private List<GameObject> _slimeBullets;
        private Texture2D _slimeBulletTexture;
        private ICollisionHandler _slimeBulletCollisionHandler;

        #endregion

        #region Coins

        private List<Pickup> _coins;

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


            var coinSheet = contentManager.Load<Texture2D>(@"GameObjects/Coin/coin");

            var coinAnimations = new Dictionary<CurrentAnimation, Animation>()
            {
                {
                    CurrentAnimation.Idle,
                    new Animation(coinSheet, 16, 16)
                },
                {
                    CurrentAnimation.Run,
                    new Animation(coinSheet, 16, 16)
                },
            };

            IAnimationManager slimeAnimationManager = new AnimationManager(greenSlimeAnimations);

            var purpleSlimeSheet = _contentManager.Load<Texture2D>(@"GameObjects\PurpleSlime\sheet");

            var purpleSlimeAnimations = new Dictionary<CurrentAnimation, Animation>
            {
                {
                    CurrentAnimation.Idle,
                    new Animation(purpleSlimeSheet, 24, 24)
                },
                {
                    CurrentAnimation.Run,
                    new Animation(purpleSlimeSheet, 24, 24)
                },
            };

            IAnimationManager greenSlimeAnimationManager = new AnimationManager(greenSlimeAnimations);

            IAnimationManager purpleSlimeAnimationManager = new AnimationManager(purpleSlimeAnimations);

            IAnimationManager knightAnimationManager = new AnimationManager(knightAnimations);

            IAnimationManager coinAnimationManager = new AnimationManager(coinAnimations);

            #endregion

            #region MovementManager and CollisionHandler

            var movementManager = new MovementManager(new JumpManager());
            var collisionHandler = new CollisionHandler(new SolidObjectCollisionResponder(), collisionRects);
            var coinCollisionHandler = new CoinCollisionHandler(new CoinCollisionResponder());
            var playerCollisionHandler = new PlayerPlayerEnemyCollisionHandler(new PlayerEnemyCollisionResponder());

            #endregion

            #region coins

            _coins = new List<Pickup>();

            foreach (var o in level1.ObjectGroups["Coins"].Objects)
            {
                var coinPosition = new Vector2((int)o.X, (int)o.Y - (int)o.Height);

                _coins.Add(new Coin(coinAnimationManager)
                {
                    Position = coinPosition,
                });
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
                coinCollisionHandler,
                _coins)
            {
                Position = spawnPosKnight
            };

            #endregion

            #region Enemies

            _greenSlimes = new List<GameObject>();

            var limitBoxes = new List<Rectangle>();

            foreach (var rect in level1.ObjectGroups["EnemyLimit"].Objects)
            {
                limitBoxes.Add(new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
            }

            foreach (var o in level1.ObjectGroups["GreenSlime"].Objects)
            {
                _greenSlimes.Add(new GreenSlime(movementManager, collisionHandler, greenSlimeAnimationManager,
                    new GreenSlimeMovement(), playerCollisionHandler, _knight, limitBoxes)
                {
                    Position = new Vector2((int)o.X, (int)o.Y - (int)o.Height)
                });
            }

            _purpleSlimes = new List<GameObject>();

            _slimeBullets = new List<GameObject>();
            _slimeBulletTexture = _contentManager.Load<Texture2D>(@"GameObjects\PurpleSlime\slimeBullet");
            _slimeBulletCollisionHandler =
                new CollisionHandler(new SlimeBulletSolidObjectCollisionResponder(), collisionRects);

            foreach (var o in level1.ObjectGroups["PurpleSlime"].Objects)
            {
                _purpleSlimes.Add(new PurpleSlime(movementManager, collisionHandler, purpleSlimeAnimationManager, new PurpleSlimeMovement(), playerCollisionHandler, _knight)
                {
                    Position = new Vector2((int)o.X, (int)o.Y - (int)o.Height)
                });
            }



            #endregion
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_background, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

            _tileMapManager.Draw(spriteBatch);

            _knight.Draw(spriteBatch, gameTime);

            foreach (var greenSlime in _greenSlimes)
                greenSlime.Draw(spriteBatch, gameTime);
            foreach (var coin in _coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }

            foreach (var purpleSlime in _purpleSlimes)
                purpleSlime.Draw(spriteBatch, gameTime);

            foreach (var slimeBullet in _slimeBullets)
                slimeBullet.Draw(spriteBatch, gameTime);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            bool allDestroyed = _coins.All(coin => coin.IsDestroyed);
            _knight.Update(gameTime);
            foreach (var greenSlime in _greenSlimes)
                greenSlime.Update(gameTime);

            foreach (var purpleSlime in _purpleSlimes)
            {
                purpleSlime.Update(gameTime);
                var slime = purpleSlime as PurpleSlime;
                var slimeBullet = slime.Shoot(_slimeBulletTexture, _slimeBulletCollisionHandler, gameTime);
                if(slimeBullet != null)
                    _slimeBullets.Add(slimeBullet);
            }

            foreach (var slimeBullet in _slimeBullets)
            {
                slimeBullet.Update(gameTime);
            }

            foreach (var coin in _coins)
            {
                coin.Update(gameTime);
            }

            if (allDestroyed)
            {
                _game.ChangeState(new Level2State(_game, _graphicsDevice, _contentManager));
            }


            if (_knight.Position.Y > _graphicsDevice.Viewport.Height + 100 || _knight.IsDestroyed)
            {
                _game.ChangeState(new DeathState(_game, _graphicsDevice, _contentManager));
            }
        }
    }
}
