using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    public class Menu
    {
        private SpriteFont notoSans;
        private SpriteFont paytoneOne;
        private SpriteBatch sb;
        private GraphicsDevice graphics;
        private Game1 game;
        private Rectangle window;

        public Menu(SpriteFont paytoneOne, SpriteFont notoSans, SpriteBatch sb, GraphicsDevice graphics, Game1 game)
        {
            this.paytoneOne = paytoneOne;
            this.notoSans = notoSans;
            this.sb = sb;
            this.graphics = graphics;
            this.game = game;
            window = this.graphics.Viewport.Bounds;
        }

        public void UpdateMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) ||
                Input.GetButtonDown(1, Input.ArcadeButtons.A1) ||
                Input.GetButtonDown(2, Input.ArcadeButtons.A1))
            {
                game.currState = GameState.Game;
            }
        }
        public void DrawText()
        {
            sb.DrawString(paytoneOne, "Brick Breaker", new Vector2(50, window.Height / 2f - 100), Color.Black);
            sb.DrawString(notoSans, "Press the Red Button to Start", new Vector2(30, window.Height / 2f + 50), Color.Black);
        }
    }
}