using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the game and game updates
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// Object for managing all graphics operations
        /// </summary>
        public GraphicsDeviceManager Graphics { get; set; }
        
        /// <summary>
        /// Manages all content operations and draw calls
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Sets up the content and window for the game
        /// </summary>
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Initializes the necessary fields
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// Loads content from the content manager
        /// </summary>
        protected override void LoadContent()
        {
            
        }

        /// <summary>
        /// Loops every frame and updates the game window
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            // Adds a keybind to exit the game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)
                || (GamePad.GetState(PlayerIndex.One).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.Menu)
                && GamePad.GetState(PlayerIndex.Two).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.Menu)))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Loops every frame and draws the content on the screen
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clears the window each frame
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(gameTime);
        }
    }
}