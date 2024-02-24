using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Strategy
{
    static class SpriteLoader
    {
        public static Texture2D groundTexture;
        public static Texture2D infantryTexture;

        public static void LoadAllSprite()
        {
            groundTexture = Globals.contentManager.Load<Texture2D>("graphic/grass");
            infantryTexture = Globals.contentManager.Load<Texture2D>("graphic/tile_0124");
        }
    }
}
