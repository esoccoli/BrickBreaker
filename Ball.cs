using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    public class Ball
    {
        private Texture2D texture;
        private Texture2D altTexture;
        private Vector2 position;
        private Rectangle bounds;
        private Vector2 velocity;
        private Paddle paddle;
        private Rectangle window;
        
        public Ball(Texture2D texture, Texture2D altTexture, Vector2 position, Vector2 velocity, Paddle paddle, Rectangle window)
        {
            this.texture = texture;
            this.altTexture = altTexture;
            this.position = position;
            this.velocity = velocity;
            this.paddle = paddle;
            this.window = window;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void UpdateBall()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
            if (bounds.Intersects(paddle.Bounds))
            {
                Rectangle overlap = Rectangle.Intersect(bounds, paddle.Bounds);
                
                if (overlap.Width > overlap.Height)
                {
                    velocity.Y *= -1;

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
                else
                {
                    velocity.X *= -1;
                    velocity.Y *= -1;
                }
            }

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

            if (position.Y < 0)
            {
                velocity.Y *= -1;
                position.Y = 1;
            }
        }

        public void DrawBall(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
        
    }
}