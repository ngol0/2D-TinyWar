using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Strategy.Input;
using MonoGame.Extended.Entities;

namespace Strategy
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphic;
        private SpriteBatch spriteBatch;
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
            graphic.PreferredBackBufferWidth = 960;
            graphic.PreferredBackBufferHeight = 720;
            graphic.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.input = new InputManager();
            Globals.contentManager = this.Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scene = new Scene();
          
            world = new WorldBuilder()
                .AddSystem(new UnitSelectionSystem(scene))
                .AddSystem(new UnitMovementSystem(scene))
                .AddSystem(new GridRenderSystem(spriteBatch))
                .AddSystem(new RenderSystem(spriteBatch))
                .Build();

            SpriteLoader.LoadAllSprite();
            Globals.entityFactory = new EntityFactory(world);

            scene.Init();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update logic here
            Globals.input.Update(gameTime);
            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            world.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
