using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }

        private readonly Texture2D _spriteSheet;

        private List<AnimationFrame> _frames;
        private int _counter = 0;
        private double _secondCounter = 0;


        public Animation(Texture2D spriteSheet, int frameWidth, int frameHeight)
        {
            _frames = new List<AnimationFrame>();
            _spriteSheet = spriteSheet;

            GetFramesFromTextureProperties(
                _spriteSheet.Width,
                _spriteSheet.Height,
                _spriteSheet.Width / frameWidth,
                _spriteSheet.Height / frameHeight
            );
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, SpriteEffects effect = SpriteEffects.None)
        {
            CurrentFrame = _frames[_counter];

            _secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            var fps = 6;

            if (_secondCounter >= 1d / fps)
            {
                _counter++;
                _secondCounter = 0;
            }

            if (_counter >= _frames.Count)
                _counter = 0;

            spriteBatch.Draw(_spriteSheet, position, CurrentFrame.SourceRectangle, Color.White, 0f, new Vector2(), 1f, effect, 1);
        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites,
            int numberOfHeightSprites)
        {
            var widthOfFrame = width / numberOfWidthSprites;
            var heightOfFrame = height / numberOfHeightSprites;

            for (var y = 0; y <= height - heightOfFrame; y += heightOfFrame)
            {
                for (var x = 0; x <= width - widthOfFrame; x += widthOfFrame)
                {
                    _frames.Add(new AnimationFrame(new Rectangle(x, y, widthOfFrame, heightOfFrame)));
                }
            }
        }
    }
}
