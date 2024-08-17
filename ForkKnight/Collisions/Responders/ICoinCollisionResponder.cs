using ForkKnight.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForkKnight.GameObjects;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions.Responders
{
    internal interface ICoinCollisionResponder
    {
        void RespondToCollision(Coin gameObject, Rectangle collisionRectangle);
    }
}
