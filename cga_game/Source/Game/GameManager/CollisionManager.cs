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
        //public static List<int> Colliders = new List<int>();


        public static Dictionary<int, Transform> Colliders = new Dictionary<int, Transform>();

        //event
        public static System.Action OnBulletCollision;
        public static System.Action<int> OnUnitCollision;

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
        //public int First { get; }
        //public int Second { get; }
        public Transform FirstCol;
        public Transform SecondCol;

        //public IntPair(int first, int second)
        //{
        //    First = first;
        //    Second = second;
        //}

        public IntPair(Transform first, Transform second)
        {
            FirstCol = first;
            SecondCol = second;
        }
    }
}
