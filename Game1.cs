using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Devcade;
using static Devcade.Input;

namespace BrickBreaker
{
    /// <summary>
    /// Tracks the state of the game to allow menus to be drawn
    /// </summary>
    public enum GameState
    {
        Menu,
        Game,
        GameOver,
        Pause
    }
    
    /// <summary>
    /// Manages the game and game updates
    /// </summary>
    public class Game1 : Game
    {
        public Game1 game;
        
        /// <summary>
        /// Manages the graphics calls in the game
        /// </summary>
        private GraphicsDeviceManager _graphics;
        
        /// <summary>
        /// Manages the draw calls for the game
        /// </summary>
        private SpriteBatch _spriteBatch;
        
        /// <summary>
        /// Texture of the brick, made from a Rectangle object instead of a file
        /// </summary>
        private Texture2D brickTexture;
        
        /// <summary>
        /// Texture of the paddle, made from a Rectangle object instead of a file
        /// </summary>
        private Texture2D paddleTexture;
        
        /// <summary>
        /// Default texture of the ball
        /// </summary>
        private Texture2D ballTexture;
        
        /// <summary>
        /// Texture of the ball when it falls below the paddle
        /// </summary>
        private Texture2D ballTextureAlt;
        
        /// <summary>
        /// Font file for the menu and info text
        /// </summary>
        private SpriteFont roboto24;

        private GameState currState;
        
        public Paddle paddle { get; set; }

        /// <summary>
        /// Sets up the content and window for the game
        /// </summary>
        public Game1()
        {
            game = this;
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Initializes the necessary fields
        /// </summary>
        protected override void Initialize()
        {
            #region Setting Window Size
#if DEBUG
            _graphics.PreferredBackBufferWidth = 420;
            _graphics.PreferredBackBufferHeight = 980;
            _graphics.ApplyChanges();
#else
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
            #endregion
            
            currState = GameState.Menu;
            
            paddleTexture = new Texture2D(GraphicsDevice, 1, 1);
            
            paddle = new Paddle(paddleTexture, new Rectangle(
                GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height - 150,
                100,
                20), 
                Color.White);
            
            Input.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Loads content from the content manager
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            roboto24 = Content.Load<SpriteFont>("Roboto24");
            ballTexture = Content.Load<Texture2D>("ball");
            ballTextureAlt = Content.Load<Texture2D>("surprised-pikachu");
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
            
            paddle.UpdatePaddle(gameTime);
            Input.Update();
            
            base.Update(gameTime); }

        /// <summary>
        /// Loops every frame and draws the content on the screen
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            paddle.DrawPaddle(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}