using ForkKnight.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkKnight.Collisions
{
    internal interface ICoinCollisionHandler
    {
        void CheckCollision(GameObject player, List<Coin> enemies);
    }
}
