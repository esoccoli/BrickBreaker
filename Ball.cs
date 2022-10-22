using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreakerV2
{
    /// <summary>
    /// Manages the ball and related data
    /// </summary>
    internal class Ball
    {
        private Rectangle bounds;
        private Vector2 position;
        private Vector2 velocity;
        private int xPos;
        private int yPos;

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
        public Vector2 Vector2 { get { return velocity; } }

        public int X { get { return xPos; } set { xPos = value; } }
        public int Y { get { return yPos; } set { yPos = value; } }

        /// <summary>
        /// Sets up the ball object at a given position
        /// </summary>
        /// <param name="bounds">The rectangle that the ball is located in</param>
        public Ball()
        {
            bounds = new Rectangle((Game1.game.GraphicsDevice.Viewport.Bounds.Width / 2) - 16, (Game1.game.GraphicsDevice.Viewport.Bounds.Height - 200), 32, 32);
            position = new Vector2(bounds.X, bounds.Y);
            velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {

        }

    }
}
