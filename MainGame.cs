using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    /// <summary>
    /// Manages everything that happens in the playing state
    /// </summary>
    public class MainGame : State
    {
        private int score;
        private int lives;
        
        /// <summary>
        /// Sets up the playing state and initializes useful variables
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        /// <param name="graphics">Graphics manager</param>
        /// <param name="game">Game1 class object</param>
        /// <param name="notoSans">NotoSans font, size 20</param>
        /// <param name="notoSansSmall">NotoSans font, size 16</param>
        /// <param name="paytoneOne">PaytoneOne font, size 20</param>
        /// <param name="redButton">Texture of the red button on the cabinet</param>
        /// <param name="blueButton">Texture of the blue button on the cabinet</param>
        /// <param name="greenButton">Texture of the green button on the cabinet</param>
        /// <param name="whiteButton">Texture of the white button on the cabinet</param>
        /// <param name="score">Current score</param>
        /// <param name="lives">Number of lives remaining</param>
        public MainGame(SpriteBatch sb, 
            GraphicsDevice graphics, 
            Game1 game, 
            SpriteFont notoSans, 
            SpriteFont notoSansSmall, 
            SpriteFont paytoneOne, 
            Texture2D redButton, 
            Texture2D blueButton, 
            Texture2D greenButton, 
            Texture2D whiteButton,
            int score,
            int lives)
            : base(sb, 
                graphics, 
                game, 
                notoSans, 
                notoSansSmall, 
                paytoneOne, 
                redButton, 
                blueButton,
                greenButton, 
                whiteButton)
        {
            SB = sb;
            Graphics = graphics;
            Game = game;
            
            PaytoneOne = paytoneOne;
            NotoSans = notoSans;
            NotoSansSmall = notoSansSmall;
            
            Window = Graphics.Viewport.Bounds;
            
            RedButton = redButton;
            BlueButton = blueButton;
            GreenButton = greenButton;
            WhiteButton = whiteButton;
            
            this.score = score;
            this.lives = lives;
        }
        
        /// <summary>
        /// Displays score and number of lives on the screen
        /// </summary>
        public void DrawGameInfo()
        {
            SB.DrawString(NotoSansSmall, $"Score: {score}", new Vector2(Window.Left + 15, Window.Top + 10), Color.White);
            SB.DrawString(NotoSansSmall, $"Lives: {lives}", new Vector2(Window.Right - 85, Window.Top + 10), Color.White);
        }

        /// <summary>
        /// Updates the game objects
        /// </summary>
        /// <param name="paddle">Paddle object</param>
        /// <param name="ball">Ball object</param>
        /// <param name="brickList">List of brick objects</param>
        /// <param name="brickList">List of bricks</param>
        public void UpdateGame(Paddle paddle, Ball ball, List<Brick> brickList)
        {
            brickList[0].UpdateBricks(brickList);
            paddle.UpdatePaddle();
            ball.UpdateBall();
            
            for (int i = 0; i < brickList.Count; i++)
            {
                brickList[i].UpdateBricks(brickList);
            }
        }

        /// <summary>
        /// Draws the game objects
        /// </summary>
        /// <param name="paddle">Paddle object</param>
        /// <param name="ball">Ball object</param>
        /// <param name="brickList">List of brick objects</param>
        /// <param name="brickList">List of bricks in the game</param>
        public void DrawGame(Paddle paddle, Ball ball, List<Brick> brickList)
        {
            DrawGameInfo();

            for (int i = 0; i < brickList.Count; i++)
            {
                if (!brickList[i].Broken)
                {
                    brickList[i].DrawBrick(SB);
                }
            }
            
            paddle.DrawPaddle(SB);
            ball.DrawBall(SB);
            
            for (int i = 0; i < brickList.Count; i++)
            {
                brickList[i].DrawBrick(SB);
            }
        }
    }
}