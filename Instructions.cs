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
            #if DEBUG
            
            SB.DrawString(
                PaytoneOne, 
                "Controls", 
                new Vector2(
                    Window.Center.X - PaytoneOne.MeasureString("Controls").X / 2f, 
                    Window.Center.Y - PaytoneOne.MeasureString("Controls").Y / 2f - 350), 
                Color.White);
            
            SB.DrawString(
                NotoSans, 
                "Joystick moves the paddle", 
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Joystick moves the paddle").X / 2f, 
                    Window.Center.Y - NotoSans.MeasureString("Joystick moves the paddle").Y / 2f - 150), 
                Color.White);
            
            SB.DrawString(
                NotoSans, 
                "Break all the bricks to win", 
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Break all the bricks to win").X / 2f, 
                    Window.Center.Y - NotoSans.MeasureString("Break all the bricks to win").Y / 2f - 100), 
                Color.White);
            
            SB.DrawString(
                NotoSans, 
                "Try to win with all 5 lives", 
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Try to win with all 5 lives").X / 2f, 
                    Window.Center.Y - NotoSans.MeasureString("Try to win with all 5 lives").Y / 2f - 50), 
                Color.White);
            
            SB.DrawString(
                NotoSans, 
                "Return to Menu", 
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Return to Menu").X / 2f + 50, 
                    Window.Center.Y - NotoSans.MeasureString("Return to Menu").Y / 2f + 120),
                Color.White);
            
            SB.Draw(
                GreenButton, 
                new Rectangle(
                    (int)(Window.Center.X - NotoSans.MeasureString("Return to Menu").X / 2f - 50), 
                    (int)(Window.Center.Y - NotoSans.MeasureString("Return to Menu").Y / 2f + 100),
                    75, 
                    75),
                Color.White);
            
            #else
            
            SB.DrawString(
                PaytoneOneCabinet, 
                "Controls", 
                new Vector2(
                    Window.Center.X - PaytoneOneCabinet.MeasureString("Controls").X / 2f, 
                    Window.Center.Y - PaytoneOneCabinet.MeasureString("Controls").Y / 2f - 350), 
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet, 
                "Joystick moves the paddle", 
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Joystick moves the paddle").X / 2f, 
                    Window.Center.Y - NotoSansCabinet.MeasureString("Joystick moves the paddle").Y / 2f - 175), 
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet, 
                "Break all the bricks to win", 
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Break all the bricks to win").X / 2f, 
                    Window.Center.Y - NotoSansCabinet.MeasureString("Break all the bricks to win").Y / 2f - 50), 
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet, 
                "Try to win with all 5 lives", 
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Try to win with all 5 lives").X / 2f, 
                    Window.Center.Y - NotoSansCabinet.MeasureString("Try to win with all 5 lives").Y / 2f +75), 
                Color.White);
            
            SB.DrawString(
                NotoSansCabinet, 
                "Return to Menu", 
                new Vector2(
                    Window.Center.X - NotoSansCabinet.MeasureString("Return to Menu").X / 2f + 50, 
                    Window.Center.Y - NotoSansCabinet.MeasureString("Return to Menu").Y / 2f + 250),
                Color.White);
            
            SB.Draw(
                GreenButton, 
                new Rectangle(
                    (int)(Window.Center.X - NotoSansCabinet.MeasureString("Return to Menu").X / 2f - 75), 
                    (int)(Window.Center.Y - NotoSansCabinet.MeasureString("Return to Menu").Y / 2f + 250),
                    100, 
                    100),
                Color.White);
            
            #endif
        }
    }
}