using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the paddle and related data
    /// </summary>
    public class Paddle
    {
        private Rectangle hitbox;
        
        /// <summary>
        /// Bounds of the paddle, also used as the hitbox for collision detection
        /// </summary>
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        
        /// <summary>
        /// Color of the paddle
        /// </summary>
        public Color Color { get; private set; }
        
        /// <summary>
        /// What the paddle looks like when it is drawn
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// Creates a paddle with the specified attributes
        /// </summary>
        /// <param name="texture">What the paddle looks like</param>
        /// <param name="position">Location of the top left corner of the paddle</param>
        /// <param name="color">Color of the paddle</param>
        public Paddle(Texture2D texture, Rectangle position, Color color)
        {
            Hitbox = position;
            Color = color;
            Texture = texture;
        }
        
        /// <summary>
        /// Updates the attributes of the paddle
        /// </summary>
        /// <param name="gt">The current time in the game</param>
        public void Update(GameTime gt)
        {
            // Keybinds to move paddle left
            if ((Keyboard.GetState().IsKeyDown(Keys.Left)
                 || Keyboard.GetState().IsKeyDown(Keys.A)
                 || GetButtonDown(1, ArcadeButtons.StickLeft) 
                 || GetButtonDown(2, ArcadeButtons.StickLeft)) 
                && Hitbox.Left >= 0)
            {
                // Only moves paddle if left edge is within window
                hitbox.X -= Game1.Game.WindowSize.Width / 150;
            }

            // Keybinds to move paddle right
            if ((Keyboard.GetState().IsKeyDown(Keys.Right)
                 || Keyboard.GetState().IsKeyDown(Keys.D)
                 || GetButtonDown(1, ArcadeButtons.StickRight) 
                 || GetButtonDown(2, ArcadeButtons.StickRight)) && 
                Hitbox.Right <= Game1.Game.WindowSize.Width)
            {
                // Only moves paddle if right edge is within window
                hitbox.X += Game1.Game.WindowSize.Width / 150;
            }
        }

        /// <summary>
        /// Draws the paddle at the specified position
        /// </summary>
        /// <param name="sb">Allows things to be drawn on screen</param>
        public void Draw(SpriteBatch sb) => sb.Draw(Texture, Hitbox, Color);

        /// <summary>
        /// Resets the texture of the paddle
        /// </summary>
        public void Reset() => Texture = new Texture2D(Game1.Game.GraphicsDevice, 1, 1);
    }
}
