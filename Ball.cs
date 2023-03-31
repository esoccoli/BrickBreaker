using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Devcade.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the ball and related data
    /// </summary>
    public class Ball
    {
        /// <summary>
        /// Hitbox of the ball
        /// </summary>
        private Rectangle hitbox;

        /// <summary>
        /// Position of the ball
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Texture file for the ball
        /// </summary>
        private Texture2D texture;
        
        private Vector2 velocity;

        /// <summary>
        /// Tracks the position and lets other classes access it
        /// </summary>
        public Vector2 Position
        {
            get => position;
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
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        /// <summary>
        /// Sets up the ball object at a given position
        /// </summary>
        /// <param name="texture">The texture that the ball will have</param>
        public Ball(Texture2D texture)
        {
            this.texture = texture;

            hitbox = new Rectangle((Game1.Game.WindowSize.Width / 2) - 16, Game1.Game.WindowSize.Height - 200, 32, 32);
            position = new Vector2(hitbox.X, hitbox.Y);
            Velocity = new Vector2(0, 0);
        }

        /// <summary>
        /// Updates the ball and its attributes
        /// </summary>
        /// <param name="gameTime">The current time in the game</param>
        /// <param name="paddle">The paddle object that is drawn to the screen</param>
        /// <param name="brickList">The list of bricks that are on the screen</param>
        public void Update(GameTime gameTime, Paddle paddle, List<Brick> brickList)
        {
            int posOrNeg = Game1.Game.Rng.Next(1, 3);
            
            // Randomly sets the x-velocity of the ball to positive or negative
            if (Keyboard.GetState().IsKeyDown(Keys.Space) 
                || GetButtonDown(1, ArcadeButtons.A1)
                || GetButtonDown(2, ArcadeButtons.A1))
            {
                
                if (Game1.Game.Lives > 0 && Velocity == Vector2.Zero)
                {
                    Velocity = posOrNeg == 1 ? new Vector2(350f, -350f) : new Vector2(-350f, -350f);
                }
            }

            // Ball bounces off the left, right, and top of the screen
            if (hitbox.Right >= Game1.Game.WindowSize.Width || hitbox.Left <= 0)
            {
                velocity.X *= -1;
            }
            if (hitbox.Top <= 0)
            {
                velocity.Y *= -1;
            }

            // Resets the ball and paddle if the ball goes below the window
            if (hitbox.Bottom >= Game1.Game.WindowSize.Height + 50)
            {
                // Removes a life and resets the ball and paddle
                Game1.Game.Lives -= 1;
                Game1.Game.ResetBall();
                Game1.Game.ResetPaddle();
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
                    Position += new Vector2(0, intersection.Height * (Velocity.Y > 0 ? -1 : 1));
                    velocity.Y *= -1;

                    // If ball hits the leftmost quarter of the paddle and is moving right,
                    // Or if the ball hits the rightmost quarter of the paddle and is moving left
                    // x-velocity is reversed (multiplied by -1)
                    if (hitbox.Left < paddle.Hitbox.Left + paddle.Hitbox.Width / 4 && Velocity.X > 0)
                    {
                        velocity.X *= -1;
                    }

                    if (hitbox.Right > paddle.Hitbox.Right - paddle.Hitbox.Width / 4 && Velocity.X < 0)
                    {
                        velocity.X *= -1;
                    }
                }

                // Runs this block if the ball hit the left or right of the paddle
                else
                {
                    // Moves the ball to directly left/right of the paddle, then reverses its direction
                    Position += new Vector2(intersection.Width * (Velocity.X > 0 ? -1 : 1), 0);
                    velocity.X *= -1;
                }

            }

            #region Breaking Bricks
            // Ball breaks bricks it intersects with
            // When ball intersects a brick, its velocity in the 
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
                    Game1.Game.Score += Game1.Game.Lives;

                    // Ball speeds up slightly each time it breaks a brick
                    Velocity *= 1.03f;

                    // Removes the broken brick from the list
                    // Subtracts 1 from i to keep the bricks at the correct indexes
                    brickList.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draws the ball in the proper position
        /// </summary>
        /// <param name="sb">The sprite manager that is used to draw on screen</param>
        /// <param name="paddle">The paddle object</param>
        /// <param name="textureBelowPaddle">The texture file that the ball will get when it falls below the paddle</param>
        public void Draw(SpriteBatch sb, Paddle paddle, Texture2D textureBelowPaddle)
        {
            if (Velocity == Vector2.Zero)
            {
                sb.DrawString(Game1.Game.Roboto, 
                    "Press the red button to play.", 
                    new Vector2(Game1.Game.WindowSize.Center.X - 170, Game1.Game.WindowSize.Height - 80), 
                    Color.Black);
            }

            sb.Draw(position.Y > paddle.Hitbox.Bottom ? textureBelowPaddle : texture, new Vector2(hitbox.X, hitbox.Y), Color.White);
        }

        /// <summary>
        /// Resets the position & texture of the ball
        /// </summary>
        public void Reset()
        {
            texture = new Texture2D(Game1.Game.GraphicsDevice, 1, 1);
            hitbox = new Rectangle((Game1.Game.WindowSize.Width / 2) - 16, (Game1.Game.WindowSize.Height - 220), 32, 32);
        }
    }
}
