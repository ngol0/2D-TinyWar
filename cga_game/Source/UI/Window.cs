using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace Strategy.UI
{
    class Window
    {
        public virtual void Init() { }
        public virtual void OnEnter() { }
        public virtual void Render(SpriteBatch spriteBatch, SpriteFont font) { }
        public virtual void OnExit() { }
        public virtual bool IsInGame() { return false; }
    }
}
