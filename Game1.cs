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
        Win,
        Pause,
        Instructions,
        LifeLost
    }
    
    /// <summary>
    /// Manages the game and game updates
    /// </summary>
    public class Game1 : Game
    {
        public Game1 game { get; set; }

        private Random rng;
        
        /// <summary>
        /// Manages the graphics calls in the game
        /// </summary>
        private GraphicsDeviceManager _graphics;
        
        /// <summary>
        /// Manages the draw calls for the game
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Paytone One font, size 20
        /// </summary>
        private SpriteFont paytoneOne;
        
        /// <summary>
        /// Noto Sans font, size 20
        /// </summary>
        private SpriteFont notoSans20;
        
        /// <summary>
        /// Noto Sans font, size 16
        /// </summary>
        private SpriteFont notoSans16;
        
        /// <summary>
        /// Current state of the game
        /// </summary>
        public GameState currState;
        
        /// <summary>
        /// Boumds of the game window
        /// </summary>
        private Rectangle window;

        /// <summary>
        /// Menu screen object
        /// </summary>
        private Menu gameMenu;
        
        /// <summary>
        /// Instructions screen object
        /// </summary>
        private Instructions instructionsScreen;
        
        /// <summary>
        /// Pause screen object
        /// </summary>
        private PauseMenu pauseMenu;
        
        /// <summary>
        /// Main game object
        /// </summary>
        private MainGame mainGame;
        
        /// <summary>
        /// Life lost screen object
        /// </summary>
        private LifeLost lifeLostScreen;
        
        /// <summary>
        /// Game over screen object
        /// </summary>
        private GameOver gameOverScreen;
        
        /// <summary>
        /// Win screen object
        /// </summary>
        private Win winScreen;
        
        /// <summary>
        /// Red button on the cabinet
        /// </summary>
        private Texture2D redButton;
        
        /// <summary>
        /// Blue button on the cabinet
        /// </summary>
        private Texture2D blueButton;
        
        /// <summary>
        /// Green button on the cabinet
        /// </summary>
        private Texture2D greenButton;
        
        /// <summary>
        /// White button on the cabinet
        /// </summary>
        private Texture2D whiteButton;
        
        /// <summary>
        /// Paddle object
        /// </summary>
        private Paddle paddle;
        
        /// <summary>
        /// Texture of the paddle
        /// </summary>
        private Texture2D paddleTexture;

        /// <summary>
        /// Ball object
        /// </summary>
        private Ball ball;
        
        /// <summary>
        /// Texture of the ball
        /// </summary>
        private Texture2D ballTexture;
        
        /// <summary>
        /// Alternate texture of the ball
        /// </summary>
        internal Texture2D altBallTexture;
        
        /// <summary>
        /// List of bricks
        /// </summary>
        private List<Brick> brickList;
        
        /// <summary>
        /// Texture of the bricks
        /// </summary>
        private Texture2D brickTexture;
        
        /// <summary>
        /// List of colors the bricks can have
        /// </summary>
        private Color[] brickColors;
        
        /// <summary>
        /// Player's current score
        /// </summary>
        public int score;
        
        /// <summary>
        /// Number of lives remaining
        /// </summary>
        public int lives;

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

            rng = new Random();
            
            currState = GameState.Menu;
            window = GraphicsDevice.Viewport.Bounds;
            
            // Sets up the game objects and the textures
            paddleTexture = new Texture2D(GraphicsDevice, 1, 1);
            paddleTexture.SetData(new Color[] { Color.White });
            
            brickTexture = new Texture2D(GraphicsDevice, 1, 1);
            brickTexture.SetData(new Color[] { Color.White });
            
            // Initializes the list of bricks
            brickList = new List<Brick>();
            
            // Initializes the list of colors the bricks can have
            brickColors = new Color[]
            { 
                Color.Red, Color.Orange, 
                Color.Yellow, Color.LawnGreen,
                Color.Green, Color.DarkGreen, 
                Color.Aqua, Color.CornflowerBlue, 
                Color.DodgerBlue, Color.Magenta, 
                Color.Purple, Color.Pink
            };

            paddle = new Paddle(paddleTexture, new Rectangle(window.Center.X - 50, window.Bottom - 100, 150, 20), Color.Gray, window);

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
            
            ballTexture = Content.Load<Texture2D>("ball");
            altBallTexture = Content.Load<Texture2D>("surprised-pikachu");

            paytoneOne = Content.Load<SpriteFont>("PaytoneOne");
            notoSans20 = Content.Load<SpriteFont>("NotoSans");
            notoSans16 = Content.Load<SpriteFont>("NotoSansSmall");
            
            // Cabinet button textures
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
            
            lifeLostScreen = new LifeLost(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            gameOverScreen = new GameOver(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            winScreen = new Win(_spriteBatch, 
                GraphicsDevice, 
                game, 
                notoSans20, 
                notoSans16, 
                paytoneOne, 
                redButton, 
                blueButton, 
                greenButton, 
                whiteButton);
            
            Vector2 vel = new Vector2(0, 0);
            
            int randNum = rng.Next(2);
            
            if (randNum == 0)
            {
                vel = new Vector2(-3, -5);
            }
            else
            {
                vel = new Vector2(3, -5);
            }
            
            ball = new Ball(ballTexture,
                altBallTexture,
                new Vector2(paddle.Bounds.Center.X - ballTexture.Width / 2f, paddle.Position.Y - ballTexture.Height - 10),
                vel,
                paddle,
                window);
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
                    
                    // Ball is below the bottom of the screen
                    if (ball.Position.Y > window.Bottom + 50)
                    {
                        lives -= 1;

                        if (lives <= 0)
                        {
                            currState = GameState.GameOver;
                        }
                        else
                        {
                            currState = GameState.LifeLost;
                        }
                    }
                    
                    // Checks if all bricks are broken
                    else if (brickList[0].AllBricksBroken(brickList))
                    {
                        currState = GameState.Win;
                    }
                    
                    else
                    {
                        mainGame.UpdateGame(paddle, ball, brickList);
                    }
                    break;
                
                case GameState.LifeLost:
                    lifeLostScreen.UpdateLifeLost();
                    break;
                
                case GameState.GameOver:
                    gameOverScreen.UpdateGameOver();
                    break;
                
                case GameState.Win:
                    winScreen.UpdateWin();
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
                        mainGame.DrawGame(paddle, ball, brickList);
                    }
                    break;
                
                case GameState.LifeLost:
                    lifeLostScreen.DrawLifeLost();
                    break;
                
                case GameState.GameOver:
                    gameOverScreen.DrawGameOver();
                    break;
                
                case GameState.Win:
                    winScreen.DrawWinScreen();
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        
        /// <summary>
        /// Resets the state of all the bricks and sets the ball to the initial position
        /// </summary>
        public void ResetBricks()
        {
            #region Brick Sizes

            // There will always be 15 rows & 8 columns of bricks
            // The size of the bricks will scale to fit the window
            int numRows = 12;
            int numCols = 5;
            
            // Number of pixels between bricks
            int brickSpacing = 5;

            // Greatest y-coordinate (farthest down) that bricks can be drawn
            int brickAreaHeight = window.Height / 2;

            // First row of bricks is 80 pixels below top of window
            int brickAreaTopOffset = 80;

            // Width and height of each brick (scales based on window size
            int brickWidth = (window.Width - ((numCols * brickSpacing) + brickSpacing)) / numCols;
            int brickHeight = (brickAreaHeight - ((numRows * brickSpacing) + brickSpacing)) / numRows;
            #endregion
            
            // Adds all the bricks to the list
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    Brick currBrick = new Brick(brickTexture, new Rectangle(
                        5 + (col * (brickWidth + brickSpacing)),
                        brickAreaTopOffset + (row * (brickHeight + brickSpacing)),
                        brickWidth,
                        brickHeight),
                        brickColors[row],
                        ball,
                        window
                    );
                    
                    brickList.Add(currBrick);
                }
            }
        }

        /// <summary>
        /// Resets the ball to the initial state
        /// </summary>
        public void ResetBall()
        {
            ball.Position = new Vector2(paddle.Bounds.Center.X - ballTexture.Width / 2f, paddle.Position.Y - ballTexture.Height - 10);
            ball.Velocity = rng.Next(0, 2) == 0 ? new Vector2(-3, -5) : new Vector2(3, -5);
            ball.Bounds = new Rectangle((int)ball.Position.X, (int)ball.Position.Y, ballTexture.Width, ballTexture.Height);
        }
        
        /// <summary>
        /// Resets the paddle to the initial state
        /// </summary>
        public void ResetPaddle()
        {
            paddle.Position = new Vector2(window.Center.X - paddle.Bounds.Width / 2f, window.Height - 100);
            paddle.Bounds = new Rectangle((int)paddle.Position.X, (int)paddle.Position.Y, 150, 20);
        }
        
        /// <summary>
        /// Resets the game to the initial state
        /// </summary>
        public void ResetGame()
        {
            ResetBricks();
            ResetBall();
            ResetPaddle();
            score = 0;
            lives = 5;
        }
    }
}