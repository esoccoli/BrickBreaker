using Microsoft.Xna.Framework;
using static Devcade.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    /// <summary>
    /// </summary>
    public class GameOver : State
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
        public GameOver(SpriteBatch sb, 
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
        /// Checks for input to return to the main menu 
        /// </summary>
        public void UpdateGameOver()
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
        public void DrawGameOver()
        {
            #if DEBUG
            
            SB.DrawString(
                PaytoneOne, 
                "Game Over", 
                new Vector2(
                    Window.Center.X - PaytoneOne.MeasureString("Game Over").X / 2f, 
                    Window.Center.Y - PaytoneOne.MeasureString("Game Over").Y / 2f - 100), 
                Color.White);
            
            SB.DrawString(
                    NotoSans,
                    "Score: " + Game.score,
                    new Vector2(
                        Window.Center.X - NotoSans.MeasureString("Score: " + Game.score).X / 2f,
                        Window.Center.Y - NotoSans.MeasureString("Score: " + Game.score).Y / 2f),
                    Color.White);
            
            SB.DrawString(
                NotoSans,
                "Press any button to continue",
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Press any button to continue").X / 2f,
                    Window.Center.Y - NotoSans.MeasureString("Press any button to continue").Y / 2f + 50),
                Color.White);

            #else
            
            SB.DrawString(
                PaytoneOneCabinet, 
                "Game Over", 
                new Vector2(
                    Window.Center.X - PaytoneOneCabinet.MeasureString("Game Over").X / 2f, 
                    Window.Center.Y - PaytoneOneCabinet.MeasureString("Game Over").Y / 2f - 200), 
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet,
                "Score: " + Game.score,
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Score: " + Game.score).X / 2f,
                    Window.Center.Y - NotoSansCabinet.MeasureString("Score: " + Game.score).Y / 2f),
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet,
                "Press any button to continue",
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Press any button to continue").X / 2f,
                    Window.Center.Y - NotoSansCabinet.MeasureString("Press any button to continue").Y / 2f + 75),
                Color.White);
            
            #endif
        }
    }
}