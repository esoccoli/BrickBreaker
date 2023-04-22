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
        /// Sets up a menu object with useful variables
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        /// <param name="graphics">Graphics manager</param>
        /// <param name="game">Game1 class object</param>
        /// <param name="notoSansCabinet">NotoSans font, size 48</param>
        /// <param name="notoSansCabinetSmall">NotoSans font, size 36</param>
        /// <param name="notoSans">NotoSans font, size 20</param>
        /// <param name="notoSansSmall">NotoSans font, size 16</param>
        /// <param name="paytoneOneCabinet">PaytoneOne font, size 72</param>
        /// <param name="paytoneOne">PaytoneOne font, size 36</param>
        /// <param name="redButton">Texture of the red button on the cabinet</param>
        /// <param name="blueButton">Texture of the blue button on the cabinet</param>
        /// <param name="greenButton">Texture of the green button on the cabinet</param>
        /// <param name="whiteButton">Texture of the white button on the cabinet</param>
        public MainGame(SpriteBatch sb, 
            GraphicsDevice graphics, 
            Game1 game, 
            SpriteFont notoSansCabinet,
            SpriteFont notoSansCabinetSmall,
            SpriteFont notoSans, 
            SpriteFont notoSansSmall,
            SpriteFont paytoneOneCabinet,
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
                notoSansCabinet,
                notoSansCabinetSmall,
                notoSans, 
                notoSansSmall, 
                paytoneOneCabinet,
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton)
        {
            PaytoneOne = paytoneOne;
            NotoSans = notoSans;
            NotoSansSmall = notoSansSmall;
            SB = sb;
            Graphics = graphics;
            Game = game;
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
        public void DrawGameInfo(int score, int lives)
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
        public void DrawGame(Paddle paddle, Ball ball, List<Brick> brickList)
        {
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