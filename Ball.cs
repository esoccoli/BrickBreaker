using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //private Texture2D ballTextureBelowPaddle;

        private int xPos;
        private int yPos;
        #endregion

        #region Properties
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

        /// <summary>
        /// Accesses or modifies the current x-position of the ball
        /// </summary>
        public int X { get { return xPos; } set { xPos = value; } }

        /// <summary>
        /// Accesses or modifies the current y-position of the ball
        /// </summary>
        public int Y { get { return yPos; } set { yPos = value; } }
        #endregion

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

        /// <summary>
        /// Updates the ball and its attributes
        /// </summary>
        /// <param name="gameTime">The current time in the game</param>
        /// <param name="paddle">The paddle object that is drawn to the screen</param>
        /// <param name="brickList">The list of bricks that are on the screen</param>
        public void Update(GameTime gameTime, Paddle paddle, List<Brick> brickList)
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
                // Checks if the ball is intersecting with the paddle
                Rectangle intersection = Rectangle.Intersect(bounds, paddle.Hitbox);

                // If the ball hits the top or bottom of the paddle
                if (intersection.Width > intersection.Height)
                {
                    // Moves the ball to right above/below the paddle, then reverses its direction
                    Position += new Vector2(0, intersection.Height * (velocity.Y > 0 ? -1 : 1));
                    velocity.Y *= -1;
                }
                else
                {
                    // Moves the ball to directly left/right of the paddle, then reverses its direction
                    Position += new Vector2(intersection.Width * (velocity.X > 0 ? -1 : 1), 0);
                    velocity.X *= -1;
                }
                
            }

            #region Breaking Bricks
            // Ball breaks bricks it intersects with
            // When ball intsersects a brick, its velocity in the 
            // direction of the collision is reversed
            for (int i = 0; i < brickList.Count; i++)
            {
                if (bounds.Intersects(brickList[i].Hitbox))
                {
                    Rectangle intersection = Rectangle.Intersect(bounds, brickList[i].Hitbox);

                    if (intersection.Width > intersection.Height)
                    {
                        velocity.Y *= -1;
                    }
                    else
                    {
                        velocity.X *= -1;
                    }

                    brickList[i].Broken = true;
                    
                    // Removes the broken brick from the list
                    // Subtracts 1 from i to keep the bricks at the correct indexes
                    brickList.RemoveAt(i);
                    Game1.game.score += 1;
                    i--;

                    // Ball speeds up slightly each time it breaks a brick
                    velocity *= 1.01f;
                }
            }
            #endregion
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the ball on screen at the proper position
        /// </summary>
        /// <param name="_spriteBatch">Allows textures to be drawn in the window</param>
        public void Draw(SpriteBatch _spriteBatch, Paddle paddle, Texture2D ballTextureBelowPaddle)
        {
            if (position.Y > paddle.Hitbox.Bottom)
            {
                _spriteBatch.Draw(ballTextureBelowPaddle, new Vector2(bounds.X, bounds.Y), Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, new Vector2(bounds.X, bounds.Y), Color.White);
            }
            
        }

        // Resets the position of the ball
        public void Reset()
        {
            bounds = new Rectangle((windowSize.Width / 2) - 16, (windowSize.Height - 200), 32, 32);
        }
    }
}
