using Devcade;
using Microsoft.Xna.Framework;
using static Devcade.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Manages things that happen when the game is paused
    /// </summary>
    public class PauseMenu : State
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
        public PauseMenu(SpriteBatch sb, 
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
        /// Draws the content of the pause menu
        /// </summary>
        public void DrawPauseMenu()
        {
            SB.DrawString(
                PaytoneOne, 
                "Game Paused", 
                new Vector2(
                    Window.Center.X - PaytoneOne.MeasureString("Game Paused").X / 2f, 
                    Window.Center.Y - PaytoneOne.MeasureString("Game Paused").Y / 2f - 100), 
                Color.White);

            SB.DrawString(NotoSans,
                "Unpause",
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Unpause").X / 2f + 50,
                    Window.Center.Y - NotoSans.MeasureString("Unpause").Y / 2f + 45),
                Color.White);
            
            SB.Draw(
                WhiteButton,
                new Rectangle(
                    (int)(Window.Center.X - NotoSans.MeasureString("Unpause").X / 2f - 40),
                    (int)(Window.Center.Y - NotoSans.MeasureString("Unpause").Y / 2f + 25),
                    75,
                    75),
                Color.BlueViolet);
        }
    }
}