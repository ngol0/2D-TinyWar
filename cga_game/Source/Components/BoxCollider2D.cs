using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using MonoGame.Extended.Entities;

namespace Strategy
{
    class BoxCollider2D
    {
        public Rectangle boundingBox;
        public Action<int, int> OnCollisionEnter;

        public string tag;

        public BoxCollider2D() { }

        public BoxCollider2D(string tag)
        {
            this.tag = tag;
        }
    }
}
