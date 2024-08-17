using System.Collections.Generic;
using ForkKnight.Collisions.Responders;
using ForkKnight.GameObjects;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

internal interface ICollisionHandler
{
    IEnemyCollisionResponder CollisionResponder { get; }
    void CheckCollision(GameObject gameObject);
}