using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.Animations;
using ForkKnight.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;

namespace ForkKnight.GameObjects
{
    internal class Knight : IGameObject, IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public IInputReader InputReader { get; set; }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }

        private Texture2D _texture;
        private MovementManager _movementManager = new MovementManager();
        private AnimationList _animationList;
        private MovementManager movementManager = new MovementManager();

        public Knight(Texture2D texture, IInputReader inputReader)
        {
            _texture = texture;
            _animationList = new AnimationList();


            InputReader = inputReader;

            Position = Vector2.One;
            Velocity = new Vector2(4, 0);

        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            Move(gameTime, graphics);
            Vector2 input = InputReader.ReadInput();

            if (input != Vector2.Zero)
            {
                // If any arrow key is pressed, update movement animation
                UpdateMovementAnimation(input, gameTime);
            }
            else
            {
                // If no arrow keys are pressed, update idle animation
                UpdateIdleAnimation(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Animation currentAnimation;
            Vector2 input = InputReader.ReadInput();
            // Example: If the character is moving horizontally, use the walk animation; otherwise, use the idle animation
            if (input != Vector2.Zero)
            {
                currentAnimation = _animationList.runanimationList[DetermineMovementDirection(input)];
            }
            else
            {
                currentAnimation = _animationList.idleanimationList[GetIdleDirection()];
            }

            // Draw the current animation
            spriteBatch.Draw(_texture, Position, currentAnimation.CurrentFrame.SourceRectangle, Color.White);

        }


        private void Move(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            movementManager.Move(this, gameTime, graphics);

        }

        private Direction DetermineMovementDirection(Vector2 input)
        {
            return (input.X > 0) ? Direction.Right : Direction.Left;
        }
        private Direction GetIdleDirection()
        {
            return Direction.Left;
        }
        private void UpdateIdleAnimation(GameTime gameTime)
        {
            // Update idle animation based on the direction the character is facing
            foreach (KeyValuePair<Direction, IdleAnimation> kvp in _animationList.idleanimationList)
            {
                kvp.Value.Update(gameTime);
            }
        }
        private void UpdateMovementAnimation(Vector2 input, GameTime gameTime)
        {
            // Determine the direction based on input vector
            Direction movementDirection = DetermineMovementDirection(input);

            // Update movement animation based on the direction
            foreach (KeyValuePair<Direction, RunAnimation> kvp in _animationList.runanimationList)
            {
                if (kvp.Key == movementDirection)
                {
                    kvp.Value.Update(gameTime);
                }
            }

            // Move the character on input
            Position += input * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Limit the speed to ensure consistent movement speed in all directions
            Velocity = Limit(Velocity, 5.0f);
        }
    }
}
