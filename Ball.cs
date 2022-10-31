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
        //private Rectangle windowSize;
        
        private Rectangle hitbox;
        private Vector2 position;
        private Vector2 velocity;

        private Texture2D texture;
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
        /// <param name="bounds">The rectangle that the ball is located in</param>
        public Ball(Texture2D texture)
        {
            this.texture = texture;

            //windowSize = Game1.game.GraphicsDevice.Viewport.Bounds;
            hitbox = new Rectangle((Game1.game.GraphicsDevice.Viewport.Bounds.Width / 2) - 16, Game1.game.GraphicsDevice.Viewport.Bounds.Height - 200, 32, 32);

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && velocity.X == 0 && velocity.Y == 0 && Game1.game.lives > 0)
            {
                posOrNeg = Game1.game.rng.Next(1, 3);
                if (posOrNeg == 1)
                {
                    Velocity = new Vector2(200, -200);
                }
                else
                {
                    Velocity = new Vector2(-200, -200);
                }
                
            }
            // Ball bounces off the left, right, and top of the screen
            if (hitbox.Right >= Game1.game.GraphicsDevice.Viewport.Bounds.Width || hitbox.Left <= 0)
            {
                velocity.X *= -1;
            }

            if (hitbox.Top <= 0)
            {
                velocity.Y *= -1;
            }

            // Resets the ball and paddle if the ball goes below the window
            if (hitbox.Bottom >= Game1.game.GraphicsDevice.Viewport.Bounds.Height + 50)
            {   
                Game1.game.lives -= 1;
                Game1.game.ResetBall();
                Game1.game.ResetPaddle();
            }

            // Ball bounces off the paddle
            if (hitbox.Intersects(paddle.Hitbox))
            {
                // Checks if the ball is intersecting with the paddle
                Rectangle intersection = Rectangle.Intersect(hitbox, paddle.Hitbox);

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
                if (hitbox.Intersects(brickList[i].Hitbox))
                {
                    Rectangle intersection = Rectangle.Intersect(hitbox, brickList[i].Hitbox);

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
                    Game1.game.score += Game1.game.lives;

                    // Ball speeds up slightly each time it breaks a brick
                    velocity *= 1.01f;
                    brickList.RemoveAt(i);
                    i--;
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
                _spriteBatch.Draw(ballTextureBelowPaddle, new Vector2(hitbox.X, hitbox.Y), Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, new Vector2(hitbox.X, hitbox.Y), Color.White);
            }
            
        }

        /// <summary>
        /// Resets the position of the ball
        /// </summary>
        public void Reset()
        {
            texture = new Texture2D(Game1.game.GraphicsDevice, 1, 1);
            hitbox = new Rectangle((Game1.game.GraphicsDevice.Viewport.Bounds.Width / 2) - 16, (Game1.game.GraphicsDevice.Viewport.Bounds.Height - 220), 32, 32);
        }
    }
}
