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
        public PauseMenu(SpriteBatch sb, 
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
        
        /// <summary>
        /// Draws the content of the pause menu
        /// </summary>
        public void DrawPauseMenu()
        {
            SB.DrawString(PaytoneOne, "Game Paused", new Vector2(Window.Center.X - 160, Window.Center.Y - 50), Color.White);
            
            SB.DrawString(NotoSans, "Press ", new Vector2(Window.Center.X - 130, Window.Center.Y + 50), Color.White);
            SB.Draw(WhiteButton, new Rectangle(Window.Center.X - 50, Window.Center.Y + 45, 50, 50), Color.BlueViolet);
            SB.DrawString(NotoSans, "4", new Vector2(Window.Center.X - 33, Window.Center.Y + 50), Color.White);
            SB.DrawString(NotoSans, " to unpause", new Vector2(Window.Center.X + 10, Window.Center.Y + 50), Color.White);
        }
    }
}