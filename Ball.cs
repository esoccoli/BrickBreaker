using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    public class Ball
    {
        private Vector2 position;
        private Vector2 velocity;
        private Rectangle bounds;
        private Paddle paddle;
        private Rectangle window;
        
        /// <summary>
        /// Texture of the ball
        /// </summary>
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// Alternate texture of the ball
        /// </summary>
        public Texture2D AltTexture { get; set; }
        
        /// <summary>
        /// Velocity of the ball
        /// </summary>
        public Vector2 Velocity { get; set; }
        
        /// <summary>
        /// Stores the position of the ball
        /// </summary>
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
        /// Creates the ball with the necessary data
        /// </summary>
        /// <param name="texture">Texture of the ball</param>
        /// <param name="altTexture">Alternate ball texture</param>
        /// <param name="position">Position of the ball</param>
        /// <param name="velocity">Velocity of the ball</param>
        /// <param name="paddle">Paddle object</param>
        /// <param name="window">Rectangle storing the bounds of the game window</param>
        public Ball(Texture2D texture, Texture2D altTexture, Vector2 position, Vector2 velocity, Paddle paddle, Rectangle window)
        {
            Texture = texture;
            AltTexture = altTexture;
            Position = position;
            Velocity = velocity;
            this.paddle = paddle;
            this.window = window;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        
        /// <summary>
        /// Updates the data of the ball
        /// </summary>
        public void UpdateBall()
        {
            position.X += Velocity.X;
            position.Y += velocity.Y;
            
            bounds = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
            
            // Ball bounces off the paddle
            if (bounds.Intersects(paddle.Bounds))
            {
                Rectangle overlap = Rectangle.Intersect(bounds, paddle.Bounds);
                
                // Determines which side of the paddle the ball collided with
                // and bounces the ball off in the appropriate direction
                if (overlap.Width > overlap.Height)
                {
                    velocity.Y *= -1;
                    
                    // Ball will reverse x direction if it hits the leftmost or rightmost quarter of the top of the paddle
                    if (bounds.Left < paddle.Bounds.Left + paddle.Bounds.Width / 4 && velocity.X > 0)
                    {
                        velocity.X *= -1;
                    }
                    if (bounds.Right > paddle.Bounds.Right - paddle.Bounds.Width / 4 && velocity.X < 0)
                    {
                        velocity.X *= -1;
                    }
                }
                
                else if (overlap.Height > overlap.Width)
                {
                    position += new Vector2(overlap.Width * (velocity.X > 0 ? -1 : 1), 0);
                    velocity.X *= -1;
                }
                
                else if (overlap.Height == overlap.Width)
                {
                    velocity.X *= -1;
                    velocity.Y *= -1;
                }
            }
            
            // Ball bounces off the left and right of the screen
            if (bounds.Left < 0)
            {
                velocity.X *= -1;
                position.X = 1;
            }
            if (bounds.Right > window.Width)
            {
                velocity.X *= -1;
                position.X = window.Width - bounds.Width - 1;
            }
            
            // Ball bounces off the top of the screen
            if (position.Y < 0)
            {
                velocity.Y *= -1;
                position.Y = 1;
            }
        }
        
        /// <summary>
        /// Draws the ball to the screen
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        public void DrawBall(SpriteBatch sb)
        {
            sb.Draw(Texture, position, Color.White);
        }
        
    }
}