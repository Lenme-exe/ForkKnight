using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations;

internal interface IAnimationManager
{
    Animation CurrentAnimation { get; set; }
    void Play(CurrentAnimation animation);
    void Update(IMovable gameObject, GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, IMovable gameObject, GameTime gameTime);
}