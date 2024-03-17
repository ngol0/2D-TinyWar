using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Content;

namespace Strategy
{
    static class SpriteLoader
    {
        public static Texture2D groundTexture;
        public static Texture2D infantryTexture;
        public static Texture2D tankTexture;
        public static Texture2D planeTexture;
        public static Texture2D moneyTowerTexture;

        //buttons
        public static Texture2D infantryBtn;
        public static Texture2D tankBtn;
        public static Texture2D planeBtn;
        public static Texture2D moneyTowerBtn;

        //enemy
        public static Texture2D alienTexture;
        public static Texture2D robotTexture;
        public static Texture2D bombTexture;
        public static Texture2D batTexture;

        //bullet
        public static Texture2D bulletTexture;


        public static void LoadAllSprite(ContentManager content)
        {
            groundTexture = content.Load<Texture2D>("graphic/grass");

            //unit
            infantryTexture = content.Load<Texture2D>("graphic/infantry_green");
            tankTexture = content.Load<Texture2D>("graphic/tank_blue");
            planeTexture = content.Load<Texture2D>("graphic/plane_red");
            moneyTowerTexture = content.Load<Texture2D>("graphic/money");

            //buttons
            infantryBtn = content.Load<Texture2D>("graphic/infantrybtn_up");
            tankBtn = content.Load<Texture2D>("graphic/tankbtn_up");
            planeBtn = content.Load<Texture2D>("graphic/planebtn_up");
            moneyTowerBtn = content.Load<Texture2D>("graphic/money_up");

            //enemy
            alienTexture = content.Load<Texture2D>("graphic/alien1");
            robotTexture = content.Load<Texture2D>("graphic/robot1");
            bombTexture = content.Load<Texture2D>("graphic/bomb");
            batTexture = content.Load<Texture2D>("graphic/bat1");

            //bullet
            bulletTexture = content.Load<Texture2D>("graphic/bullet");
        }
    }
}
