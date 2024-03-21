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
        public static List<int> Colliders = new List<int>();

        public static void AddToColliders(int collider)
        {
            Colliders.Add(collider);
        }
    }

    public struct IntPair
    {
        public int First { get; }
        public int Second { get; }

        public IntPair(int first, int second)
        {
            First = first;
            Second = second;
        }
    }
}
