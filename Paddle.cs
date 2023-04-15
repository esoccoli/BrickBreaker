using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;
namespace BrickBreaker
{
    public class Paddle
    {
        private Texture2D texture;
        private Rectangle bounds;
        private Color color;
        private Rectangle window;
        
        public Paddle(Texture2D texture, Rectangle bounds, Color color, Rectangle window)
        {
            this.texture = texture;
            this.bounds = bounds;
            this.color = color;
            this.window = window;
        }
        
        /// <summary>
        /// Updates the paddle's position based on user input
        /// </summary>
        public void UpdatePaddle()
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.A) ||
                GetButtonDown(1, ArcadeButtons.StickLeft) ||
                GetButtonDown(2, ArcadeButtons.StickLeft)) &&
                bounds.X > 0)
            {
                bounds.X -= 5;
            }
            
            if ((Keyboard.GetState().IsKeyDown(Keys.D) ||
                GetButtonDown(1, ArcadeButtons.StickRight) ||
                GetButtonDown(2, ArcadeButtons.StickRight)) &&
                bounds.X < window.Width - bounds.Width)
            {
                bounds.X += 5;
            }
        }
        
        public void DrawPaddle(SpriteBatch sb)
        {
            sb.Draw(texture, bounds, color);
        }

    }
}