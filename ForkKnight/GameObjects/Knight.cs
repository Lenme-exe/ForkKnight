using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.GameObjects
{
    internal class Knight : IGameObject, IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get;  set; }
        public IInputReader InputReader { get; set; }
        public CurrentAnimation CurrentAnimation { get; set; }
        public Direction Direction { get; set; }


        private readonly List<Animation> _animations;
        private readonly MovementManager _movementManager = new MovementManager();

        public Knight(List<Texture2D> textures, IInputReader inputReader)
        {
            _animations = new List<Animation>();
            foreach (var t in textures)
            {
                _animations.Add(new Animation(t, 32, 32));
            }

            InputReader = inputReader;

            Position = Vector2.One;
            Velocity = new Vector2(2, 0);

        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            Move(graphics);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteEffects effect = SpriteEffects.None;
            if (Direction == Direction.Left)
                effect = SpriteEffects.FlipHorizontally;

            switch (CurrentAnimation)
            {
                case CurrentAnimation.Idle:
                    _animations[0].Draw(spriteBatch, Position, gameTime, effect);
                    break;
                case CurrentAnimation.Run:
                    _animations[1].Draw(spriteBatch, Position, gameTime, effect);
                    break;
                case CurrentAnimation.Hit:
                    break;
                case CurrentAnimation.Death:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Move(GraphicsDeviceManager graphics)
        {
            _movementManager.Move(this, graphics);
        }
    }
}
