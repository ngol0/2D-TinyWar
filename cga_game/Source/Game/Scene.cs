using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using Strategy.Grid;
using System.Collections.Generic;
using System.Diagnostics;

namespace Strategy
{
    class Scene
    {
        //--Variables--//
        #region Unit Data
        private List<UnitType> unitList = new List<UnitType>() {};
        public List<UnitType> UnitList => unitList;
        public UnitButton currentSelectedUnitButton = null;
        public UnitType CurrentSelectedUnitType => currentSelectedUnitButton.unitType;
        #endregion

        #region Grid
        GridSystem levelGrid;
        int gridWidth = 12;
        int gridHeight = 6;
        int cellSize = 50;

        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        #endregion

        #region Position Vectors
        Vector2 offSet;
        Vector2 startingButtonPos;
        public Vector2 StartingButtonPos => startingButtonPos;
        #endregion

        #region GameStats
        private int currentMoneyAmount;
        public int CurrentMoneyAmount => currentMoneyAmount;
        private int score = 0;
        public int Score => score;
        #endregion

        #region Enemy Manager
        EnemyManager enemManager;
        public EnemyManager EnemyManager => enemManager;
        #endregion
        //
        World world;

        public Scene() { }

        public void Init(World world)
        {
            offSet = new Vector2(5.0f);
            startingButtonPos = new Vector2(50, 520);

            InitLevelGrid();
            InitUnitList();
            InitUnitButtons();

            enemManager = new EnemyManager(this);
            enemManager.InitEnemySpawner();

            currentMoneyAmount = 100000;
            this.world = world;

            CollisionManager.OnBulletCollision += OnScore;
            CollisionManager.OnUnitCollision += UnitCollisionResponse;
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

        public void InitCurrentSelectedUnit(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + offSet;
            var unit = Globals.entityFactory.CreateUnit(
                new Transform() { gridPos = position, worldPos = worldPosition, scale = 40, tag = "unit" },
                CurrentSelectedUnitType);
        }

        private void UnitCollisionResponse(int unit)
        {
            Entity currUnit = world.GetEntity(unit);
            var unitComp = currUnit.Get<UnitComponent>();
            unitComp.currentHealth -= 20;

            if (unitComp.currentHealth <= 0) 
            {
                world.DestroyEntity(unit);
                CollisionManager.Colliders.Remove(unit);
            }
        }

        public void SpendMoney()
        {
            currentMoneyAmount -= CurrentSelectedUnitType.cost;
        }

        public void AddMoney(int amount)
        {
            currentMoneyAmount += amount;
        }

        public void OnScore()
        {
            score += 10;
        }

        public GridPosition GetGridPosition(Vector2 worldPos) => levelGrid.GetGridPosition(worldPos);
        public Vector2 GetWorldPosition(GridPosition position) => levelGrid.GetWorldPosition(position);
        public GridItem GetGridItem(GridPosition position) => levelGrid.GetGridItem(position);
        public bool IsValidPosGrid(GridPosition position) => levelGrid.IsValidGridPos(position);
        public bool IsGridWalkable(GridPosition position) => levelGrid.GetGridItem(position).IsWalkable;
    }
}
