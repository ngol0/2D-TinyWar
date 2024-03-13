using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using SharpDX.Direct3D9;
using Strategy.Grid;
using System.Collections.Generic;

namespace Strategy
{
    class Scene
    {
        GridSystem levelGrid;
        public GridSystem LevelGrid => levelGrid;
        private List<Unit> unitList = new List<Unit>() {};

        int gridWidth = 15;
        int gridHeight = 10;
        int cellSize = 50;

        public UnitButton currentSelectedUnitButton = null;
        private int currentMoneyAmount;

        public Unit CurrentSelectedUnitType => currentSelectedUnitButton.unitType;
        public int CurrentMoneyAmount => currentMoneyAmount;

        public Scene() { }

        public void Init()
        {
            InitLevelGrid();
            InitUnitList();
            InitUnitButtons();

            currentMoneyAmount = 100;
        }

        private void InitUnitList()
        {
            Unit infantry = new Unit() { name = UnitType.INFANTRY, cost = 50 };
            Unit tank = new Unit() { name = UnitType.TANK, cost = 80 };
            Unit plane = new Unit() { name = UnitType.PLANE, cost = 100 };
            Unit tower = new Unit() { name = UnitType.RESOURCE, cost = 20 };

            unitList.Add(infantry);
            unitList.Add(tank);
            unitList.Add(plane);
            unitList.Add(tower);
        }

        private void InitLevelGrid()
        {
            levelGrid = new GridSystem(gridWidth, gridHeight, cellSize);
        }

        private void InitUnitButtons()
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                Vector2 buttonPos = new Vector2(500 + i*60, 500);
                var button = Globals.entityFactory.CreateButton(new Transform() { gridPos = GetGridPosition(buttonPos), worldPos = buttonPos, scale = 50 });

                //init data for each button
                button.Get<UnitButton>().unitType = unitList[i];
            }
        }

        public void InitInfantry(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + new Vector2(1,1);
            var infantry = Globals.entityFactory.CreateInfantryUnit(new Transform() { gridPos = position, worldPos = worldPosition, scale = 45 });
        }

        public void InitTank(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + new Vector2(3, 3);
            var tank = Globals.entityFactory.CreateTank(new Transform() { gridPos = position, worldPos = worldPosition, scale = 45 });
        }

        public void InitPlane(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + new Vector2(3, 3);
            var plane = Globals.entityFactory.CreatePlane(new Transform() { gridPos = position, worldPos = worldPosition, scale = 45 });
        }

        public void InitMoneyTower(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + new Vector2(3, 3);
            var tower = Globals.entityFactory.CreateMoneyTower(new Transform() { gridPos = position, worldPos = worldPosition, scale = 45 });
        }

        public void SpendMoney()
        {
            currentMoneyAmount -= CurrentSelectedUnitType.cost;
        }

        public void AddMoney(int amount)
        {
            currentMoneyAmount += amount;
        }

        public GridPosition GetGridPosition(Vector2 worldPos) => levelGrid.GetGridPosition(worldPos);
        public Vector2 GetWorldPosition(GridPosition position) => levelGrid.GetWorldPosition(position);
        public GridItem GetGridItem(GridPosition position) => levelGrid.GetGridItem(position);
        public bool IsValidPosGrid(GridPosition position) => levelGrid.IsValidGridPos(position);
        public bool IsGridWalkable(GridPosition position) => levelGrid.GetGridItem(position).IsWalkable;
    }
}
