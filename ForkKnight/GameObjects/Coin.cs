using ForkKnight.Animations;
using ForkKnight.Collisions;
using Microsoft.Xna.Framework;

namespace ForkKnight.GameObjects
{
    internal class Coin : Pickup
    {
        public Coin(IAnimationManager animationManager, GameObject player, IPlayerPickupCollisionHandler playerPickupCollisionHandler) : base(animationManager, player, playerPickupCollisionHandler)
        {

        }

        public override void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 16, 16);
        }
    }
}

