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

        private Texture2D _texture;
        private Animation _animation;
        private MovementManager _movementManager = new MovementManager();

        public Knight(Texture2D texture, IInputReader inputReader)
        {
            _texture = texture;
            _animation = new Animation();
            _animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 4, 1);

            InputReader = inputReader;

            Position = Vector2.One;
            Velocity = new Vector2(4, 0);

        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            _animation.Update(gameTime);
            Move(graphics);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _animation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Move(GraphicsDeviceManager graphics)
        {
            _movementManager.Move(this, graphics);
        }
    }
}
