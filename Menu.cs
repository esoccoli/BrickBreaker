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
        /// <param name="notoSans">NotoSans font, size 20</param>
        /// <param name="notoSansSmall">NotoSans font, size 16</param>
        /// <param name="paytoneOne">PaytoneOne font, size 20</param>
        /// <param name="redButton">Texture of the red button on the cabinet</param>
        /// <param name="blueButton">Texture of the blue button on the cabinet</param>
        /// <param name="greenButton">Texture of the green button on the cabinet</param>
        /// <param name="whiteButton">Texture of the white button on the cabinet</param>
        public Menu(SpriteBatch sb, 
            GraphicsDevice graphics, 
            Game1 game, 
            SpriteFont notoSans, 
            SpriteFont notoSansSmall,
            SpriteFont paytoneOne,
            Texture2D redButton,
            Texture2D blueButton,
            Texture2D greenButton,
            Texture2D whiteButton) 
            : base(sb, graphics, game, notoSans, notoSansSmall, paytoneOne, redButton, blueButton, greenButton, whiteButton)
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
            SB.DrawString(PaytoneOne, "Brick Breaker", new Vector2(50, Window.Height / 2f - 100), Color.White);
            
            SB.DrawString(NotoSans, "Press ", new Vector2(100, Window.Height / 2f + 50), Color.White);
            SB.Draw(RedButton, new Rectangle(175, Window.Height / 2 + 50, 50, 50), Color.White);
            SB.DrawString(NotoSans, " to Start", new Vector2(230, Window.Height / 2f + 50), Color.White);
            
            SB.DrawString(NotoSans, "Press ", new Vector2(50, Window.Height / 2f + 125), Color.White);
            SB.Draw(BlueButton, new Rectangle(125, Window.Height / 2 + 120, 50, 50), Color.White);
            SB.DrawString(NotoSans, " for Instructions", new Vector2(180, Window.Height / 2f + 125), Color.White);
        }
    }
}