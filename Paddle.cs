using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the paddle and related data
    /// </summary>
    internal class Paddle
    {
        #region Fields
        private Rectangle hitbox;
        private Color color;
        private Texture2D _texture;
        #endregion

        #region Properties
        /// <summary>
        /// Accesses or modifies the hitbox of the paddle
        /// </summary>
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }

        /// <summary>
        /// Accesses or modifies the color of the paddle
        /// </summary>
        public Color PaddleColor { get => color; set => color = value; }
        #endregion

        // === CONSTRUCTORS ===
        /// <summary>
        /// Sets up the paddle with a rectangle for the position
        /// </summary>
        /// <param name="position">The position of the paddle</param>
        /// <param name="color">The color of the paddle</param>
        public Paddle(Texture2D texture, Rectangle position, Color color)
        {
            this.hitbox = position;
            this.color = color;
            this._texture = texture;
        }

        // === METHODS ===
        /// <summary>
        /// Uodates the attributes of the paddle
        /// </summary>
        /// <param name="gameTime">The current time in the game</param>
        public void Update(GameTime gameTime)
        {
            // Moves the paddle left or right when the arrow keys or A/D are pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                // Only moves paddle if left edge is within window
                if (Hitbox.Left >= 0)
                {
                    hitbox.X -= 5;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // Only moves paddle if right edge is within window
                if (Hitbox.Right <= Game1.game.GraphicsDevice.Viewport.Width)
                {
                    hitbox.X += 5;
                }
            }
        }

        /// <summary>
        /// Draws the paddle at the specified position
        /// </summary>
        /// <param name="_spriteBatch">Allows things to be drawn on screen</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, Hitbox, PaddleColor);
        }

        /// <summary>
        /// Resets the texture of the paddle
        /// </summary>
        public void Reset()
        {
            _texture = new Texture2D(Game1.game.GraphicsDevice, 1, 1);
        }
    }
}
