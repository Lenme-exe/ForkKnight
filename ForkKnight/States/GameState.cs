﻿using System;
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
using Color = Microsoft.Xna.Framework.Color;
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
        private Texture2D _background;

        #endregion

        #region Knight

        private Knight _knight;
        private IAnimationManager _knightAnimationManager;
        private IMovementManager _movementManager;
        private ICollisionHandler _collisionHandler;
        private IJumpManager _jumpManager;

        #endregion

        private List<GreenSlime> _greenSlimes;
        private IAnimationManager _slimeAnimationManager;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            #region Tilemap

            _level1 = new TmxMap(@"Content\Levels\level1.tmx");
            _tileset = _contentManager.Load<Texture2D>(@"Levels\" + _level1.Tilesets[0].Name);
            _background = _contentManager.Load<Texture2D>(@"background");
            var tileWidth = _level1.Tilesets[0].TileWidth;
            var tileHeight = _level1.Tilesets[0].TileHeight;
            var tileSetTilesWide = _tileset.Width / tileWidth;
            _tileMapManager = new TileMapManager(_level1, _tileset, tileSetTilesWide, tileWidth, tileHeight);

            #endregion

            #region Collisions

            _collisionRects = new List<Rectangle>();

            foreach (var o in _level1.ObjectGroups["Collision"].Objects)
                _collisionRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));

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

            _slimeAnimationManager = new AnimationManager(greenSlimeAnimations);

            _knightAnimationManager = new AnimationManager(knightAnimations);

            #endregion

            #region MovementManager and CollisionHandler

            _jumpManager = new JumpManager();
            _movementManager = new MovementManager(_jumpManager);
            _collisionHandler = new CollisionHandler();

            #endregion

            #region Knight

            Vector2 spawnPos = Vector2.One;

            foreach (var o in _level1.ObjectGroups["Spawn"].Objects)
                spawnPos = new Vector2((int)o.X, (int)o.Y - (int)o.Height);

            _knight = new Knight(
                _movementManager,
                _collisionHandler,
                _knightAnimationManager,
                new KeyboardReader())
            {
                Position = spawnPos
            };


            #endregion
            _greenSlimes = new List<GreenSlime>();

            foreach (var o in _level1.ObjectGroups["GreenSlime"].Objects)
            {
                _greenSlimes.Add(new GreenSlime(_movementManager, _collisionHandler, _slimeAnimationManager,
                    new KeyboardReader())
                {
                    Position = new Vector2((int)o.X, (int)o.Y - (int)o.Height)
                });
            }
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

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _knight.Update(gameTime, _collisionRects);
            foreach (var greenSlime in _greenSlimes)
            {
                greenSlime.Update(gameTime, _collisionRects);
            }

            if (_knight.Position.Y > _graphicsDevice.Viewport.Height + 100)
            {
                _game.ChangeState(new DeathState(_game, _graphicsDevice, _contentManager));
            }
        }
    }
}
