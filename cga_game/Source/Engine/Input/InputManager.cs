using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strategy.Input
{
    public class InputManager
    {
        // #region #endregion tags are a nice way of blockifying code in VS.
        #region Fields
        // Store current and previous states for comparison. 
        public MouseState previousMouseState;
        public MouseState currentMouseState;
        #endregion

        #region Update
        public virtual void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
        #endregion

        #region Mouse Methods
        public Rectangle GetMouseBounds(bool currentState)
        {
            // Return a 1x1 squre representing the mouse click's bounding box.
            if (currentState)
                return new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
            else
                return new Rectangle(previousMouseState.X, previousMouseState.Y, 1, 1);
        }

        public bool GetIsMouseButtonUp(MouseButton btn, bool currentState)
        {
            // Simply returns whether the button state is released or not.

            if (currentState)
                switch (btn)
                {
                    case MouseButton.Left:
                        return currentMouseState.LeftButton == ButtonState.Released;
                    case MouseButton.Middle:
                        return currentMouseState.MiddleButton == ButtonState.Released;
                    case MouseButton.Right:
                        return currentMouseState.RightButton == ButtonState.Released;
                }
            else
                switch (btn)
                {
                    case MouseButton.Left:
                        return previousMouseState.LeftButton == ButtonState.Released;
                    case MouseButton.Middle:
                        return previousMouseState.MiddleButton == ButtonState.Released;
                    case MouseButton.Right:
                        return previousMouseState.RightButton == ButtonState.Released;
                }
            return false;
        }

        public bool GetIsMouseButtonDown(MouseButton btn, bool currentState)
        {
            // This will just call the method above and negate.
            return !GetIsMouseButtonUp(btn, currentState);
        }
        #endregion
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }
}
