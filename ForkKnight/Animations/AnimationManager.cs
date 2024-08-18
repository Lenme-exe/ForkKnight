using System.Collections.Generic;
using ForkKnight.GameObjects;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForkKnight.Animations;

internal class AnimationManager: IAnimationManager
{    
    public Animation CurrentAnimation { get; set; }

    private readonly Dictionary<CurrentAnimation, Animation> _animations;

    public AnimationManager(Dictionary<CurrentAnimation, Animation> animations)
    {
        _animations = animations;
        CurrentAnimation = _animations[Movement.CurrentAnimation.Idle];
    }

    public void Play(CurrentAnimation animation)
    {
        if (CurrentAnimation == _animations[animation])
            return;
        CurrentAnimation = _animations[animation];
    }

    public void Update(GameObject gameObject, GameTime gameTime)
    {
        switch (gameObject.CurrentAnimation)
        {
            case Movement.CurrentAnimation.Idle:
                Play(Movement.CurrentAnimation.Idle);
                break;
            case Movement.CurrentAnimation.Run:
                Play(Movement.CurrentAnimation.Run);
                break;
            case Movement.CurrentAnimation.Hit:
                Play(Movement.CurrentAnimation.Hit);
                break;
            case Movement.CurrentAnimation.Death:
                Play(Movement.CurrentAnimation.Death);
                break;
            default:
                Play(Movement.CurrentAnimation.Idle);
                break;
        }

        CurrentAnimation.Update(gameTime);
    }

    public void Update(Pickup pickup, GameTime gameTime)
    {
        Play(Movement.CurrentAnimation.Idle);

        CurrentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, GameObject gameObject, GameTime gameTime)
    {
        var effect = SpriteEffects.None;

        if (gameObject.Direction == Direction.Left)
            effect = SpriteEffects.FlipHorizontally;

        CurrentAnimation.Draw(spriteBatch, gameObject.Position, gameTime, effect);
    }

    public void Draw(SpriteBatch spriteBatch, Pickup pickup, GameTime gameTime)
    {
        CurrentAnimation.Draw(spriteBatch, pickup.Position, gameTime);
    }
}