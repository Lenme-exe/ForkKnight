using ForkKnight.Animations;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    internal class Coin : Pickup
    {
        public Coin(IAnimationManager animationManager) : base(animationManager)
        {

        }

        public override void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 16, 16);
        }
    }
}

