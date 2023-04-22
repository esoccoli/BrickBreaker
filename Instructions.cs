using Microsoft.Xna.Framework;
using static Devcade.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Manages things that happen while the game is in the instructions screen
    /// </summary>
    public class Instructions : State
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
        public Instructions(SpriteBatch sb, 
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
        /// Determines whether the state should be updated
        /// </summary>
        public void UpdateInstructions()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B) ||
                GetButtonDown(1, ArcadeButtons.A3) ||
                GetButtonDown(2, ArcadeButtons.A3))
            {
                Game.currState = GameState.Menu;
            }
        }
        
        /// <summary>
        /// Draws the text for the instructions screen
        /// </summary>
        public void DrawText()
        {
            SB.DrawString(PaytoneOne, "Controls", new Vector2(110, 30), Color.White);
            
            SB.DrawString(NotoSansSmall, "Move the joystick to move the paddle", new Vector2(Window.Center.X - 185, Window.Center.Y - 300), Color.White);
            SB.DrawString(NotoSansSmall, "Break all the bricks to win", new Vector2(Window.Center.X - 125, Window.Center.Y - 200), Color.White);
            SB.DrawString(NotoSansSmall, "Try to win with all 5 lives", new Vector2(Window.Center.X - 125, Window.Center.Y - 100), Color.White);
            
            SB.DrawString(NotoSansSmall, "Press ", new Vector2(Window.Center.X - 150, Window.Center.Y), Color.White);
            SB.Draw(GreenButton, new Rectangle(Window.Center.X - 80, Window.Center.Y - 10, 50, 50), Color.White);
            SB.DrawString(NotoSansSmall, " to return to menu", new Vector2(Window.Center.X - 20, Window.Center.Y), Color.White);
            
            SB.Draw(Game.altBallTexture, new Rectangle(0, Window.Center.Y, Window.Width, Window.Height / 2), Color.White);
        }
    }
}