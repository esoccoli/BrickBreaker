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
        Playing,
        GameOver,
        Pause,
        Instructions
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
        /// Font file for the menu and info text
        /// </summary>
        private SpriteFont paytoneOne;
        private SpriteFont notoSans20;
        private SpriteFont notoSans16;

        public GameState currState;

        private Menu gameMenu;
        private Instructions instructionsScreen;
        private PauseMenu pauseMenu;
        private MainGame mainGame;
        
        private Texture2D redButton;
        private Texture2D blueButton;
        private Texture2D greenButton;
        private Texture2D whiteButton;

        internal Texture2D surprisedPikachu;
        
        private int score;
        private int lives;

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

            score = 0;
            lives = 5;
            
            Input.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Loads content from the content manager
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            surprisedPikachu = Content.Load<Texture2D>("surprised-pikachu");
            
            paytoneOne = Content.Load<SpriteFont>("PaytoneOne");
            notoSans20 = Content.Load<SpriteFont>("NotoSans");
            notoSans16 = Content.Load<SpriteFont>("NotoSansSmall");
            
            redButton = Content.Load<Texture2D>("red-button");
            blueButton = Content.Load<Texture2D>("blue-button");
            greenButton = Content.Load<Texture2D>("green-button");
            whiteButton = Content.Load<Texture2D>("white-button");
            
            gameMenu = new Menu(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            instructionsScreen = new Instructions(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            pauseMenu = new PauseMenu(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            mainGame = new MainGame(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton,
                score,
                lives);
        }

        /// <summary>
        /// Loops every frame and updates the game window
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            // Adds a keybind to exit the game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)
                || GetButtonDown(1, ArcadeButtons.Menu)
                && GetButtonDown(2, ArcadeButtons.Menu))
            {
                Exit();
            }

            switch (currState)
            {
                case GameState.Menu:
                    gameMenu.UpdateMenu();
                    break;
                
                case GameState.Instructions:
                    instructionsScreen.UpdateInstructions();
                    break;
                
                case GameState.Pause:
                    if (Keyboard.GetState().IsKeyDown(Keys.U) ||
                        GetButtonDown(1, ArcadeButtons.B3) ||
                        GetButtonDown(2, ArcadeButtons.B3))
                    {
                        currState = GameState.Playing;
                    }
                    break;
                
                case GameState.Playing:
                    if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                        GetButtonDown(1, ArcadeButtons.B4) ||
                        GetButtonDown(2, ArcadeButtons.B4))
                    {
                        currState = GameState.Pause;
                    }
                    break;
            }
            Input.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Loops every frame and draws the content on the screen
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin();

            switch (currState)
            {
                case GameState.Menu:
                    gameMenu.DrawText();
                    break;
                
                case GameState.Instructions:
                    instructionsScreen.DrawText();
                    break;
                
                case GameState.Pause:
                    pauseMenu.DrawPauseMenu();
                    break;
                
                case GameState.Playing:
                    if (lives > 0)
                    {
                        mainGame.DrawGameInfo();
                    }
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}