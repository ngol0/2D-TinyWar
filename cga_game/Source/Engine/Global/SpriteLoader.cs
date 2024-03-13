using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Strategy
{
    static class SpriteLoader
    {
        public static Texture2D groundTexture;
        public static Texture2D infantryTexture;
        public static Texture2D tankTexture;
        public static Texture2D planeTexture;
        public static Texture2D moneyTowerTexture;
        public static Texture2D unitButton;

        public static void LoadAllSprite()
        {
            groundTexture = Globals.contentManager.Load<Texture2D>("graphic/grass");

            //unit
            infantryTexture = Globals.contentManager.Load<Texture2D>("graphic/infantry");
            tankTexture = Globals.contentManager.Load<Texture2D>("graphic/tank");
            planeTexture = Globals.contentManager.Load<Texture2D>("graphic/plane");
            moneyTowerTexture = Globals.contentManager.Load<Texture2D>("graphic/resource");

            unitButton = Globals.contentManager.Load<Texture2D>("graphic/button");
        }
    }
}
