using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;

namespace BrickBreaker
{
    public class LifeLost : State
    {
        
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
        public LifeLost(SpriteBatch sb, 
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
            Texture2D whiteButton) 
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
            PaytoneOneCabinet = paytoneOneCabinet;
            PaytoneOne = paytoneOne;
            
            NotoSansCabinet = notoSansCabinet;
            NotoSansCabinetSmall = notoSansCabinetSmall;
            
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
        }
        
        /// <summary>
        /// Checks for input to continue the game
        /// </summary>
        public void UpdateLifeLost()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                GetButtonDown(1, ArcadeButtons.A1) ||
                GetButtonDown(2, ArcadeButtons.A1))
            {
                Game.ResetPaddle();
                Game.ResetBall();
                Game.currState = GameState.Playing;
            }
        }
        
        /// <summary>
        /// Draws the contents of the life lost screen
        /// </summary>
        public void DrawLifeLost()
        {
            SB.DrawString(PaytoneOne, "Life Lost!", new Vector2(Window.Center.X - 100, Window.Center.Y - 45), Color.White);
            
            SB.DrawString(NotoSans, "Press ", new Vector2(Window.Center.X - 130, Window.Center.Y + 75), Color.White);
            SB.Draw(RedButton, new Rectangle(Window.Center.X - 50, Window.Center.Y + 75, 50, 50), Color.White);
            SB.DrawString(NotoSans, " to continue", new Vector2(Window.Center.X, Window.Center.Y + 75), Color.White);
        }
    }
}