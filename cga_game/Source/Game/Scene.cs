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
        private List<UnitType> unitList = new List<UnitType>() {};
        public List<UnitType> UnitList => unitList;

        int gridWidth = 15;
        int gridHeight = 10;
        int cellSize = 50;

        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;

        Vector2 offSet;
        Vector2 startingButtonPos;
        public Vector2 StartingButtonPos => startingButtonPos;

        public UnitButton currentSelectedUnitButton = null;
        private int currentMoneyAmount;

        public UnitType CurrentSelectedUnitType => currentSelectedUnitButton.unitType;
        public int CurrentMoneyAmount => currentMoneyAmount;

        WaveManager waveManager;
        public WaveManager WaveManager => waveManager;

        public Scene() { }

        public void Init()
        {
            offSet = new Vector2(5.0f);
            startingButtonPos = new Vector2(50, 520);

            InitLevelGrid();
            InitUnitList();
            InitUnitButtons();

            waveManager = new WaveManager(this);
            waveManager.InitEnemySpawner();

            currentMoneyAmount = 100000;
        }

        private void InitUnitList()
        {
            unitList.Add(UnitGameData.tower);
            unitList.Add(UnitGameData.infantry);
            unitList.Add(UnitGameData.tank);
            unitList.Add(UnitGameData.plane);
        }

        private void InitLevelGrid()
        {
            levelGrid = new GridSystem(gridWidth, gridHeight, cellSize);
        }

        private void InitUnitButtons()
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                Vector2 buttonPos = startingButtonPos + new Vector2(i * 70, 0);
                var button = Globals.entityFactory.CreateButton(
                    new Transform() { gridPos = GetGridPosition(buttonPos), worldPos = buttonPos, scale = 60 },
                    unitList[i].name);

                //init data for each button
                button.Get<UnitButton>().unitType = unitList[i];
            }
        }

        public void InitCurrentSelectedUnitType(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + offSet;
            Globals.entityFactory.CreateUnit(
                new Transform() { gridPos = position, worldPos = worldPosition, scale = 40 },
                CurrentSelectedUnitType);
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
