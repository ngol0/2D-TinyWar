using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Strategy.Input;
using MonoGame.Extended.Entities;

namespace Strategy
{
    class UnitData
    {
        public static Unit infantry = new Unit() { name = UnitTypeString.INFANTRY, cost = 50 };
        public static Unit tank = new Unit() { name = UnitTypeString.TANK, cost = 80 };
        public static Unit plane = new Unit() { name = UnitTypeString.PLANE, cost = 100 };
        public static Unit tower = new Unit() { name = UnitTypeString.RESOURCE, cost = 20 };
    }

    class UnitTypeString
    {
        public static string INFANTRY = "infantry";
        public static string TANK = "tank";
        public static string PLANE = "plane";
        public static string RESOURCE = "resource";
    }
}
