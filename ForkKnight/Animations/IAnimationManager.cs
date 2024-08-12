using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations;

internal interface IAnimationManager
{
    void Play(CurrentAnimation animation);
    void Update(IMovable gameObject, GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, IMovable gameObject, GameTime gameTime);
}