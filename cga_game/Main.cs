using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Strategy.Input;
using MonoGame.Extended.Entities;
using Microsoft.Xna.Framework.Content;
using Strategy.UI;

namespace Strategy
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphic;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        Scene scene;
        World world;

        public Main()
        {
            graphic = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphic.IsFullScreen = false;
            graphic.PreferredBackBufferWidth = 720;
            graphic.PreferredBackBufferHeight = 560;
            graphic.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.input = new MouseManager();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("graphic/gameFont");

            Globals.windowManager = new WindowManager();

            scene = new Scene();
            world = new WorldBuilder()
                .AddSystem(new MoneyGenerateSystem(scene))
                .AddSystem(new UnitSpawnSystem(scene))
                .AddSystem(new GridRenderSystem(spriteBatch))
                .AddSystem(new ItemSelectionSystem(scene))
                .AddSystem(new EnemySpawnSystem(scene))
                .AddSystem(new EnemyMovementSystem())
                .AddSystem(new UnitShooterSystem(scene))
                .AddSystem(new BulletMovementSystem())
                .AddSystem(new EnemyCollisionSystem(scene))
                .AddSystem(new SceneRenderSystem(spriteBatch))
                .AddSystem(new GameHUDSystem(spriteBatch, font, scene))
                .Build();

            SpriteLoader.LoadAllSprite(Content);
            Globals.entityFactory = new EntityFactory(world);

            Globals.windowManager.Init(scene);
            scene.Init(world);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update logic here
            //UI
            Globals.windowManager.Update(gameTime);
            if (!Globals.windowManager.GetCurrentWindow().IsInGame()) return;

            //in game input
            Globals.input.Update(gameTime);

            //in game update
            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            spriteBatch.Begin();
            Globals.windowManager.Render(spriteBatch, font);
            spriteBatch.End();

            //ui
            if (!Globals.windowManager.GetCurrentWindow().IsInGame()) return;

            spriteBatch.Begin();
            //in game render
            world.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
