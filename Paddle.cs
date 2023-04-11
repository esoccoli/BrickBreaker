using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;
namespace BrickBreaker
{
    public class Paddle
    {
        private Rectangle position;
        private int x;
        private int y;
        private int width;
        private int height;
        /// <summary>
        /// Texture of the paddle
        /// </summary>
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// Bounds of the paddle
        /// </summary>
        public Rectangle Position
        {
            get => position;
            set
            {
                position = value;
                x = value.X;
                y = value.Y;
                width = value.Width;
                height = value.Height;
            }
        }
        
        /// <summary>
        /// Color of the paddle
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// Creates a paddle with a specified texture, position, and color
        /// </summary>
        /// <param name="texture">Texture of the paddle</param>
        /// <param name="position">Initial position of the paddle</param>
        /// <param name="color">Paddle color</param>
        public Paddle(Texture2D texture, Rectangle position, Color color)
        {
            Texture = texture;
            Position = position;
            Color = color;
        }
        
        /// <summary>
        /// Creates a paddle with a specified texture and position, but a default color
        /// </summary>
        /// <param name="texture">Texure of the paddle</param>
        /// <param name="position">Bounds of the paddle</param>
        public Paddle(Texture2D texture, Rectangle position)
        {
            Texture = texture;
            Position = position;
            Color = Color.Black;
        }

        public void UpdatePaddle(GameTime gt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) ||
                GetButtonDown(1, ArcadeButtons.A1) ||
                GetButtonDown(2, ArcadeButtons.A1))
            {
                position.X -= 5;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.D) ||
                GetButtonDown(1, ArcadeButtons.A2) ||
                GetButtonDown(2, ArcadeButtons.A2))
            {
                position.X += 5;
            }
        }
        
        public void DrawPaddle(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, Color);
        }
    }
}