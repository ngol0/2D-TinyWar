using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Strategy.Grid;

namespace Strategy
{
    class UnitType
    {
        public string name;
        public int cost;

        public int health;
        public float shootTimer;
        public int damageDealt;
    }

    class UnitGameData
    {
        public static UnitType infantry = new UnitType() { name = UnitTypeString.INFANTRY, cost = 50, health = 100, shootTimer = 2.0f, damageDealt = 20 };
        public static UnitType tank = new UnitType() { name = UnitTypeString.TANK, cost = 80, health = 200, shootTimer = 2.0f, damageDealt = 20 };
        public static UnitType plane = new UnitType() { name = UnitTypeString.PLANE, cost = 100, health = 70, shootTimer = 1.0f, damageDealt = 50 };
        public static UnitType tower = new UnitType() { name = UnitTypeString.RESOURCE, cost = 20, health = 60, shootTimer = 0.0f, damageDealt = 0 };
    }

    class UnitTypeString
    {
        public static string INFANTRY = "infantry";
        public static string TANK = "tank";
        public static string PLANE = "plane";
        public static string RESOURCE = "resource";
    }
}
