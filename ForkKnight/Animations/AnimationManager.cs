using System.Collections.Generic;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations;

internal class AnimationManager: IAnimationManager
{
    private readonly Dictionary<CurrentAnimation, Animation> _animations;
    private Animation _currentAnimation;

    public AnimationManager(Dictionary<CurrentAnimation, Animation> animations)
    {
        _animations = animations;
        _currentAnimation = _animations[CurrentAnimation.Idle];
    }

    public void Play(CurrentAnimation animation)
    {
        if (_currentAnimation == _animations[animation])
            return;
        _currentAnimation = _animations[animation];
    }

    public void Update(IMovable movable, GameTime gameTime)
    {
        switch (movable.CurrentAnimation)
        {
            case CurrentAnimation.Idle:
                Play(CurrentAnimation.Idle);
                break;
            case CurrentAnimation.Run:
                Play(CurrentAnimation.Run);
                break;
            case CurrentAnimation.Hit:
                Play(CurrentAnimation.Hit);
                break;
            case CurrentAnimation.Death:
                Play(CurrentAnimation.Death);
                break;
            default:
                Play(CurrentAnimation.Idle);
                break;
        }

        _currentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, IMovable movable, GameTime gameTime)
    {
        var effect = SpriteEffects.None;

        if (movable.Direction == Direction.Left)
            effect = SpriteEffects.FlipHorizontally;

        _currentAnimation.Draw(spriteBatch, movable.Position, gameTime, effect);
    }
}