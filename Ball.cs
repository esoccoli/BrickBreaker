using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
        private Rectangle windowSize;   // Current window size

        private Rectangle hitbox;       // Hitbox of ball
        private Vector2 position;       // Position of ball
        private Vector2 velocity;       // Velocity of ball

        private Texture2D texture;      // Texture of ball
        #endregion

        #region Properties
        /// <summary>
        /// Tracks the hitbox and lets other classes access it
        /// </summary>
        public Rectangle Hitbox { get { return hitbox; } }

        /// <summary>
        /// Tracks the position and lets other classes access it
        /// </summary>
        public Vector2 Position
        { 
            get { return position; } 
            set 
            {
                position = value;
                hitbox.X = (int)position.X;
                hitbox.Y = (int)position.Y;
            }
        }

        /// <summary>                        
        /// Tracks the velocity and lets other classes access it
        /// </summary>
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        #endregion

        /// <summary>
        /// Sets up the ball object at a given position
        /// </summary>
        /// <param name="texture">The texture that the ball will have</param>
        public Ball(Texture2D texture)
        {
            this.texture = texture;

            // Sets local window size variable to value of Game1's window size variable
            windowSize = Game1.game.windowSize;

            hitbox = new Rectangle((windowSize.Width / 2) - 16, windowSize.Height - 200, 32, 32);
            position = new Vector2(hitbox.X, hitbox.Y);
            velocity = new Vector2(0, 0);
        }

        /// <summary>
        /// Updates the ball and its attributes
        /// </summary>
        /// <param name="gameTime">The current time in the game</param>
        /// <param name="paddle">The paddle object that is drawn to the screen</param>
        /// <param name="brickList">The list of bricks that are on the screen</param>
        public void Update(GameTime gameTime, Paddle paddle, List<Brick> brickList)
        {
            int posOrNeg = 0;

            // Randomly sets the x-velocity of the ball to positive or negative
            if (Keyboard.GetState().IsKeyDown(Keys.Space)                                       // Spacebar on keyboard
                || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)                    // 'A' button on controller
                || GamePad.GetState(1).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A1)    // Player 1 A1 button on devcade
                || GamePad.GetState(2).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A1))   // Player 2 A1 button on devcade
            {
                // Ball will only move if player has live(s) left AND the ball is not moving
                if (Game1.game.lives > 0 && velocity == new Vector2(0f,0f)) 
                {
                    // Randomly chooses whether the ball moves left or right to start
                    posOrNeg = Game1.game.rng.Next(1, 3);
                    if (posOrNeg == 1)
                    {
                        Velocity = new Vector2(200, -200); // Ball moves right
                    }
                    else
                    {
                        Velocity = new Vector2(-200, -200); // Ball moves left
                    }
                }
                
            }

            // Ball bounces off the left, right, and top of the screen
            if (hitbox.Right >= windowSize.Width || hitbox.Left <= 0)
            {
                velocity.X *= -1;
            }
            if (hitbox.Top <= 0)
            {
                velocity.Y *= -1;
            }

            // Resets the ball and paddle if the ball goes below the window
            if (hitbox.Bottom >= windowSize.Height + 50)
            {   
                Game1.game.lives -= 1;      // Lose a life
                Game1.game.ResetBall();     // Resets ball position & texture
                Game1.game.ResetPaddle();   // Resets paddle position
            }

            // Ball bounces off the paddle
            if (hitbox.Intersects(paddle.Hitbox))
            {
                // Checks if the ball is intersecting with the paddle
                Rectangle intersection = Rectangle.Intersect(hitbox, paddle.Hitbox);

                // Runs this block if the ball hit the top or bottom of the paddle
                if (intersection.Width > intersection.Height)
                {
                    // Moves the ball to right above/below the paddle, then reverses its direction
                    Position += new Vector2(0, intersection.Height * (velocity.Y > 0 ? -1 : 1));
                    velocity.Y *= -1;

                    // If ball hits the leftmost quarter of the paddle and is moving right,
                    // Or if the ball hits the rightmost quarter of the paddle and is moving left
                    // x-velocity is reversed (multiplied by -1)
                    if (hitbox.Left < paddle.Hitbox.Left + paddle.Hitbox.Width / 4 && velocity.X > 0)
                    {
                        velocity.X *= -1;
                    }

                    if ((hitbox.Right > paddle.Hitbox.Right - paddle.Hitbox.Width / 4) && velocity.X < 0)
                    {
                        velocity.X *= -1;
                    }
                }

                // Runs this block if the ball hit the left or right of the paddle
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
                // Checks if the ball collides with the brick
                if (hitbox.Intersects(brickList[i].Hitbox))
                {
                    // Stores a rectangle of the overlapping area
                    Rectangle intersection = Rectangle.Intersect(hitbox, brickList[i].Hitbox);

                    // Runs if the ball hits the top or bottom of the brick
                    if (intersection.Width > intersection.Height)
                    {
                        velocity.Y *= -1;
                    }

                    // Runs if the ball hits the left or right of the brick
                    else
                    {
                        velocity.X *= -1;
                    }

                    brickList[i].Broken = true;
                    
                    // Increases score by number of lives left each time a brick is broken
                    // The more lives the player has left, the more points they get from each brick
                    Game1.game.score += Game1.game.lives;

                    // Ball speeds up slightly each time it breaks a brick
                    velocity *= 1.01f;

                    // Removes the broken brick from the list
                    // Subtracts 1 from i to keep the bricks at the correct indexes
                    brickList.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the ball in the proper position
        /// </summary>
        /// <param name="_spriteBatch">The sprite manager that is used to draw on screen</param>
        /// <param name="paddle">The paddle object</param>
        /// <param name="ballTextureBelowPaddle">The texture file that the ball will get when it falls below the paddle</param>
        public void Draw(SpriteBatch _spriteBatch, Paddle paddle, Texture2D ballTextureBelowPaddle)
        {
            if (position.Y > paddle.Hitbox.Bottom)
            {
                _spriteBatch.Draw(ballTextureBelowPaddle, new Vector2(hitbox.X, hitbox.Y), Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, new Vector2(hitbox.X, hitbox.Y), Color.White);
            }
            
        }

        /// <summary>
        /// Resets the position & texture of the ball
        /// </summary>
        public void Reset()
        {
            texture = new Texture2D(Game1.game.GraphicsDevice, 1, 1);
            hitbox = new Rectangle((windowSize.Width / 2) - 16, (windowSize.Height - 220), 32, 32);
        }
    }
}
