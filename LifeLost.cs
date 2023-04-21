using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Devcade.Input;

namespace BrickBreaker
{
    public class LifeLost : State
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
        public LifeLost(SpriteBatch sb, 
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
        
        public void DrawLifeLost()
        {
            SB.DrawString(PaytoneOne, "Life Lost!", new Vector2(Window.Center.X - 100, Window.Center.Y - 75), Color.White);
            
            SB.DrawString(NotoSans, "Press ", new Vector2(Window.Center.X - 50, Window.Center.Y), Color.White);
            SB.Draw(RedButton, new Rectangle(Window.Center.X, Window.Center.Y, 50, 50), Color.White);
            SB.DrawString(NotoSans, " to continue", new Vector2(Window.Center.X, Window.Center.Y), Color.White);
        }
    }
}