using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ForkKnight.Collisions.Responders;
using ForkKnight.GameObjects;
using ForkKnight.Movement;
using Microsoft.Xna.Framework;

namespace ForkKnight.Collisions;

internal class CollisionHandler : ICollisionHandler
{
    public IEnemyCollisionResponder CollisionResponder { get; }

    private readonly List<Rectangle> _collisionRects;

    public CollisionHandler(IEnemyCollisionResponder collisionResponder, List<Rectangle> collisionRectangles)
    {
        CollisionResponder = collisionResponder;
        _collisionRects = collisionRectangles;
    }

    public void CheckCollision(GameObject gameObjects)
    {
        foreach (var rect in _collisionRects)
        {
            if (gameObjects.Hitbox.Intersects(rect))
            {
                CollisionResponder.RespondToCollision(gameObjects, rect);
            }
        }
    }
}