using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using Strategy.Grid;
using Strategy.UI;
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
        private int originalMoney;
        public int CurrentMoneyAmount => currentMoneyAmount;
        private int score = 0;
        public int Score => score;
        #endregion

        #region Enemy Manager
        EnemyManager enemManager;
        int enemyKilledCount;
        public EnemyManager EnemyManager => enemManager;
        #endregion
        //
        World world;
        LevelManager levelManager;
        HighScoreManager highScoreManager;

        public System.Action OnRestart;

        public Scene() { }

        public void Init(World world)
        {
            offSet = new Vector2(5.0f);
            startingButtonPos = new Vector2(50, 330);

            InitLevelGrid();
            InitUnitList();
            InitUnitButtons();

            enemManager = new EnemyManager(this);
            enemManager.InitEnemySpawner();

            originalMoney = 10000;
            currentMoneyAmount = originalMoney;
            this.world = world;

            levelManager = new LevelManager();
            levelManager.LoadTextFile();

            highScoreManager = new HighScoreManager();
            highScoreManager.Load();
            Trace.WriteLine($"{highScoreManager.currentPlayerStat.Score}");
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

        public int InitCurrentSelectedUnit(GridPosition position)
        {
            Vector2 worldPosition = GetWorldPosition(position) + offSet;
            var unit = Globals.entityFactory.CreateUnit(
                new Transform() { gridPos = position, worldPos = worldPosition, scale = 40, tag = "unit" },
                CurrentSelectedUnitType);

            return unit.Id;
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
            enemyKilledCount++; 

            if (enemyKilledCount == GetLevelInfo().Count)
            {
                score += currentMoneyAmount / 100;
                //victory
                highScoreManager.Save(new PlayerStat() { Score = score });
                Globals.windowManager.SetWindow(Globals.victoryWindow);
            }
        }

        public void Restart()
        {
            score = 0;
            enemyKilledCount = 0;
            currentMoneyAmount = originalMoney;
            CollisionManager.Colliders.Clear();

            OnRestart?.Invoke();
        }

        public GridPosition GetGridPosition(Vector2 worldPos) => levelGrid.GetGridPosition(worldPos);
        public Vector2 GetWorldPosition(GridPosition position) => levelGrid.GetWorldPosition(position);
        public GridItem GetGridItem(GridPosition position) => levelGrid.GetGridItem(position);
        public bool IsValidPosGrid(GridPosition position) => levelGrid.IsValidGridPos(position);
        public bool IsGridWalkable(GridPosition position) => levelGrid.GetGridItem(position).IsWalkable;
        public List<LevelInfo> GetLevelInfo() => levelManager.levelInfos;
        public PlayerStat GetCurrentPlayerStat() => highScoreManager.currentPlayerStat;
        public void Save(PlayerStat stat) => highScoreManager.Save(stat);
    }
}
