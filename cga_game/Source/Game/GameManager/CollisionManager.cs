using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended;
using System.Collections.Generic;
using SharpDX;
using System.Diagnostics;

namespace Strategy
{
    class CollisionManager
    {
        public static Dictionary<int, Transform> Colliders = new Dictionary<int, Transform>();

        public static void AddToColliders(int id, Transform transform) 
        {
            Colliders[id] = transform;
        }

        public static bool IsCollided(Transform mainObj, Transform otherObj)
        {
            return (Math.Abs(mainObj.worldPos.X - otherObj.worldPos.X) < 20) && (Math.Abs(mainObj.worldPos.Y - otherObj.worldPos.Y) < 20);
        }
    }

    public struct IntPair
    {
        public Transform FirstCol;
        public Transform SecondCol;

        public IntPair(Transform first, Transform second)
        {
            FirstCol = first;
            SecondCol = second;
        }
    }
}
