using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    public class Brick
    {
        private Rectangle bounds;
        private Vector2 position;
        private Ball ball;
        private Game1 game;

        /// <summary>
        /// Tracks whether or not the brick is broken
        /// </summary>
        public bool Broken { get; set; }

        /// <summary>
        /// Texture of the brick
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Color of the brick
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Bounds of the brick
        /// </summary>
        public Rectangle Bounds
        {
            get => bounds;
            set
            {
                bounds = value;
                position = new Vector2(bounds.X, bounds.Y);
            }
        }

        /// <summary>
        /// Creates a brick object with the necessary data
        /// </summary>
        /// <param name="texture">Texture of the brick</param>
        /// <param name="bounds">Bounds of the brick</param>
        /// <param name="color">Color of the brick</param>
        /// <param name="ball">Ball object</param>
        /// <param name="window">Bounds of the game window</param>
        public Brick(Texture2D texture, Rectangle bounds, Color color, Ball ball, Rectangle window, Game1 game)
        {
            Texture = texture;
            Bounds = bounds;
            Color = color;

            this.ball = ball;
            this.game = game;
            
            Broken = false;
        }

        /// <summary>
        /// Updates the current brick
        /// </summary>
        public void UpdateBricks(List<Brick> brickList)
        {
            for (int i = 0; i < brickList.Count; i++)
            {
                if (ball.Bounds.Intersects(brickList[i].Bounds)
                    && !brickList[i].Broken)
                {
                    Rectangle overlap = Rectangle.Intersect(ball.Bounds, brickList[i].Bounds);
                    
                    // Collides with the top or bottom of the brick
                    if (overlap.Width >= overlap.Height)
                    {
                        ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
                    }
                    else
                    {
                        ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
                    }
                    brickList[i].Broken = true;
                    game.score += 5 * game.lives;
                    return;
                }
            }
        }
        
        /// <summary>
        /// Draws the current brick if it is not broken
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        public void DrawBrick(SpriteBatch sb)
        {
            if (!Broken)
            {
                sb.Draw(Texture, Bounds, Color);
            }
        }
        
        /// <summary>
        /// Checks whether all the bricks are broken
        /// </summary>
        /// <param name="brickList">List of bricks</param>
        /// <returns>True if all bricks are broken, false otherwise</returns>
        public bool AllBricksBroken(List<Brick> brickList)
        {
            for (int i = 0; i < brickList.Count; i++)
            {
                if (!brickList[i].Broken)
                {
                    return false;
                }
            }
            return true;
        }
    }
}