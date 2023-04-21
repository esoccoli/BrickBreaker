using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;

namespace BrickBreaker
{
    public class Win : State
    {
        
        /// <summary>
        /// Sets up the pause menu object with useful variables
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        /// <param name="graphics">Graphics device object</param>
        /// <param name="game">Game1 class object</param>
        /// <param name="notoSans">NotoSans font, size 20</param>
        /// <param name="notoSansSmall">NotoSans font, size 16</param>
        /// <param name="paytoneOne">PaytoneOne font, size 20</param>
        /// <param name="redButton">Texture of the A1 button on the cabinet</param>
        /// <param name="blueButton">Texture of the A2 button on the cabinet</param>
        /// <param name="greenButton">Texture of the A3 button on the cabinet</param>
        /// <param name="whiteButton">Texture of the A4 button on the cabinet</param>
        public Win(SpriteBatch sb, 
            GraphicsDevice graphics, 
            Game1 game, 
            SpriteFont notoSans, 
            SpriteFont notoSansSmall, 
            SpriteFont paytoneOne, 
            Texture2D redButton, 
            Texture2D blueButton, 
            Texture2D greenButton, Texture2D whiteButton)
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
        }

        public void UpdateWin()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M) ||
                GetButtonDown(1, ArcadeButtons.A1) ||
                GetButtonDown(1, ArcadeButtons.A2) ||
                GetButtonDown(1, ArcadeButtons.A3) ||
                GetButtonDown(1, ArcadeButtons.A4) ||
                GetButtonDown(1, ArcadeButtons.B1) ||
                GetButtonDown(1, ArcadeButtons.B2) ||
                GetButtonDown(1, ArcadeButtons.B3) ||
                GetButtonDown(1, ArcadeButtons.B4) ||
                GetButtonDown(2, ArcadeButtons.A1) ||
                GetButtonDown(2, ArcadeButtons.A2) ||
                GetButtonDown(2, ArcadeButtons.A3) ||
                GetButtonDown(2, ArcadeButtons.A4) ||
                GetButtonDown(2, ArcadeButtons.B1) ||
                GetButtonDown(2, ArcadeButtons.B2) ||
                GetButtonDown(2, ArcadeButtons.B3) ||
                GetButtonDown(2, ArcadeButtons.B4))
            {
                Game.currState = GameState.Menu;
            }
        }
        
        /// <summary>
        /// Draws the text on the game over screen
        /// </summary>
        public void DrawWinScreen()
        {
            SB.DrawString(
                PaytoneOne, 
                "You Win!", 
                new Vector2(
                    Window.Center.X - PaytoneOne.MeasureString("You Win").X / 2, 
                    Window.Center.Y - PaytoneOne.MeasureString("You Win").Y / 2), 
                Color.White);
            
            SB.DrawString(
                NotoSans,
                "Score: " + Game.score,
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Score: " + Game.score).X / 2,
                    Window.Center.Y - NotoSans.MeasureString("Score: " + Game.score).Y / 2 + 50),
                Color.White);
            
            SB.DrawString(
                NotoSansSmall,
                "Press any button to continue",
                new Vector2(
                    Window.Center.X - NotoSansSmall.MeasureString("Press any button to continue").X / 2,
                    Window.Center.Y - NotoSansSmall.MeasureString("Press any button to continue").Y / 2 + 100),
                Color.White);
                    
        }
    }
}