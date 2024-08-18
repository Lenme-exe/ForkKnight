using ForkKnight.GameObjects;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations
{
internal interface IAnimationManager
{
    Animation CurrentAnimation { get; set; }
    void Play(CurrentAnimation animation);
    void Update(GameObject gameObject, GameTime gameTime);
    void Update(Pickup pickup, GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, GameObject gameObject, GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Pickup pickup, GameTime gameTime);
}
}


