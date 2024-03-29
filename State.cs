using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{
    /// <summary>
    /// Base class for all of the game states that stores useful variables as properties
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Noto Sans font, size 48
        /// </summary>
        protected SpriteFont NotoSansCabinet { get; set; }
        
        /// <summary>
        /// Noto Sans font, size 36
        /// </summary>
        protected SpriteFont NotoSansCabinetSmall { get; set; }
        
        /// <summary>
        /// Noto Sans font, size 20
        /// </summary>
        protected SpriteFont NotoSans { get; set; }
        
        /// <summary>
        /// Noto Sans font, size 16
        /// </summary>
        protected SpriteFont NotoSansSmall { get; set; }
        
        /// <summary>
        /// Paytone One font, size 48
        /// </summary>
        protected SpriteFont PaytoneOneCabinet { get; set; }
        
        /// <summary>
        /// Paytone One font, size 36
        /// </summary>
        protected SpriteFont PaytoneOne { get; set; }
        
        /// <summary>
        /// Spritebatch object for drawing
        /// </summary>
        protected SpriteBatch SB { get; set; }
        
        /// <summary>
        /// Graphics device for managing game window
        /// </summary>
        protected GraphicsDevice Graphics { get; set; }
        
        /// <summary>
        /// Game object for the actual game
        /// </summary>
        protected Game1 Game { get; set; }
        
        /// <summary>
        /// Rectangle storing the game window bounds
        /// </summary>
        protected Rectangle Window { get; set; }
        
        /// <summary>
        /// Texture of the A1 button on the cabinet
        /// </summary>
        protected Texture2D RedButton { get; set; }
        
        /// <summary>
        /// Texture for the A2 button on the cabinet
        /// </summary>
        protected Texture2D BlueButton { get; set; }
        
        /// <summary>
        /// Texture for the A3 button on the cabinet
        /// </summary>
        protected Texture2D GreenButton { get; set; }
        
        /// <summary>
        /// Texture for the A4 button on the cabinet
        /// </summary>
        protected Texture2D WhiteButton { get; set; }

        /// <summary>
        /// Initializes all the variables that are needed within the game states
        /// </summary>
        /// <param name="sb">Spritebatch object</param>
        /// <param name="graphics">GraphicsDevice object</param>
        /// <param name="game">Game object</param>
        /// <param name="notoSansCabinet">NotoSans font, size 48</param>
        /// <param name="notoSansCabinetSmall">NotoSans font, size 36</param>
        /// <param name="notoSans">NotoSans font, size 20</param>
        /// <param name="notoSansSmall">NotoSans font, size 16</param>
        /// <param name="paytoneOneCabinet">PaytoneOne font, size</param>
        /// <param name="paytoneOne">PaytoneOne font, size 20</param>
        /// <param name="redButton">A1 button texture</param>
        /// <param name="blueButton">A2 button texture</param>
        /// <param name="greenButton">A3 button tetxure</param>
        /// <param name="whiteButton">A4 button texture</param>
        protected State(SpriteBatch sb, 
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
        {
            NotoSansCabinet = notoSansCabinet;
            NotoSansCabinetSmall = notoSansCabinetSmall;
            
            NotoSans = notoSans;
            NotoSansSmall = notoSansSmall;
            
            PaytoneOneCabinet = paytoneOneCabinet;
            PaytoneOne = paytoneOne;
            
            SB = sb;
            Graphics = graphics;
            
            Game = game;
            
            Window = Graphics.Viewport.Bounds;
            
            RedButton = redButton;
            BlueButton = blueButton;
            GreenButton = greenButton;
            WhiteButton = whiteButton;
        }
    }
}