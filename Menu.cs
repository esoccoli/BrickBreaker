using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the things that happen while the game is in the menu state
    /// </summary>
    public class Menu : State
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
        public Menu(SpriteBatch sb, 
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
        /// Updates the game menu and checks for button presses
        /// </summary>
        public void UpdateMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) ||
                Input.GetButtonDown(1, Input.ArcadeButtons.A1) ||
                Input.GetButtonDown(2, Input.ArcadeButtons.A1))
            {
                Game.ResetPaddle();
                Game.ResetBall();
                Game.ResetBricks();
                Game.score = 0;
                Game.lives = 1;
                Game.currState = GameState.Playing;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.I) ||
                     Input.GetButtonDown(1, Input.ArcadeButtons.A3) || 
                     Input.GetButtonDown(2, Input.ArcadeButtons.A3))
            {
                Game.currState = GameState.Instructions;
            }
        }
        
        /// <summary>
        /// Draws the content of the game menu to the screen
        /// </summary>
        public void DrawText()
        {
            SB.DrawString(
                PaytoneOne, 
                "Brick Breaker", 
                new Vector2(
                    Window.Center.X - PaytoneOne.MeasureString("Brick Breaker").X / 2f + 10, 
                    Window.Center.Y - 200), 
                Color.White);
            
            
            SB.DrawString(
                NotoSans, 
                "Start Game", 
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Start Game").X / 2f + 50, 
                    Window.Center.Y + 50), 
                Color.White);
            
            SB.Draw(
                RedButton, 
                new Rectangle(
                    (int)(Window.Center.X - NotoSans.MeasureString("Start Game").X / 2f - 40), 
                    Window.Center.Y + 30, 
                    75, 
                    75), 
                Color.White);

            SB.DrawString(
                NotoSans,
                "Instructions",
                new Vector2(
                    Window.Center.X - NotoSans.MeasureString("Instructions").X / 2f + 50,
                    Window.Center.Y + 175),
                Color.White);
            
            SB.Draw(
                BlueButton, 
                new Rectangle(
                    (int)(Window.Center.X - NotoSans.MeasureString("Instructions").X / 2f - 40), 
                    Window.Center.Y + 155, 
                    75, 
                    75),
                Color.White);
        }
    }
}