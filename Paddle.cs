using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;
namespace BrickBreaker
{
    public class Paddle
    {
        private Rectangle window;

        private Vector2 position;
        
        private Rectangle bounds;
        
        /// <summary>
        /// Stores the texture of the paddle
        /// </summary>
        public Texture2D Texture { get; set; }

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
            }
        }

        /// <summary>
        /// Stores the bounds of the paddle as a rectangle object
        /// </summary>
        public Rectangle Bounds
        {
            get => bounds;
            set
            {
                bounds = value;
                position = new Vector2(bounds.X, bounds.Y);
            }
        }

        /// <summary>
        /// Stores the color of the paddle
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// Creates the paddle with the specified data
        /// </summary>
        /// <param name="texture">Texture of the paddle</param>
        /// <param name="bounds">Rectangle storing the paddle's position, width, and height</param>
        /// <param name="color">Color of the paddle</param>
        /// <param name="window">Rectangle storing the bounds of the game window</param>
        public Paddle(Texture2D texture, Rectangle bounds, Color color, Rectangle window)
        {
            Texture = texture;
            Bounds = bounds;
            Color = color;
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
                Bounds.X > 0)
            {
                bounds.X -= 5;
            }
            
            if ((Keyboard.GetState().IsKeyDown(Keys.D) ||
                GetButtonDown(1, ArcadeButtons.StickRight) ||
                GetButtonDown(2, ArcadeButtons.StickRight)) &&
                Bounds.X < window.Width - Bounds.Width)
            {
                bounds.X += 5;
            }
        }
        
        public void DrawPaddle(SpriteBatch sb)
        {
            sb.Draw(Texture, Bounds, Color);
        }

    }
}