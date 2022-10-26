using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the ball and related data
    /// </summary>
    internal class Ball
    {
        #region Fields
        private Rectangle windowSize;
        
        private Rectangle bounds;
        private Vector2 position;
        private Vector2 velocity;

        private Texture2D texture;

        private int xPos;
        private int yPos;
        #endregion

        /// <summary>
        /// Tracks the hitbox and lets other classes access it
        /// </summary>
        public Rectangle Bounds { get { return bounds; } }

        /// <summary>
        /// Tracks the position and lets other classes access it
        /// </summary>
        public Vector2 Position
        { 
            get { return position; } 
            set 
            {
                position = value;
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
            }
        }

        /// <summary>                        
        /// Tracks the velocity and lets other classes access it
        /// </summary>
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        public int X { get { return xPos; } set { xPos = value; } }
        public int Y { get { return yPos; } set { yPos = value; } }

        /// <summary>
        /// Sets up the ball object at a given position
        /// </summary>
        /// <param name="bounds">The rectangle that the ball is located in</param>
        public Ball(Texture2D texture)
        {
            this.texture = texture;

            windowSize = Game1.game.GraphicsDevice.Viewport.Bounds;
            bounds = new Rectangle((windowSize.Width / 2) - 16, (windowSize.Height - 200), 32, 32);

            position = new Vector2(bounds.X, bounds.Y);
            velocity = new Vector2(200, -200);
        }

        public void Update(GameTime gameTime, Paddle paddle)
        {
            // Ball bounces off the left, right, and top of the screen
            if (bounds.Right >= windowSize.Width || bounds.Left <= 0)
            {
                velocity.X *= -1;
            }

            if (bounds.Top <= 0)
            {
                velocity.Y *= -1;
            }

            // Ball bounces off the paddle
            if (bounds.Intersects(paddle.Hitbox))
            {
                velocity.Y *= -1;
            }

            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, new Vector2(bounds.X, bounds.Y), Color.White);
            
        }

    }
}
