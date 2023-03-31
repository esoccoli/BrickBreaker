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
        #region Fields
        
        /// <summary>
        /// Object variable to allow other classes to access the game data
        /// </summary>
        public static Game1 Game { get; set; }
        
        /// <summary>
        /// Stores the bounds of the game window
        /// </summary>
        public Rectangle WindowSize { get; set; }
        
        /// <summary>
        /// Initializes a random object for generating random numbers
        /// </summary>
        public Random Rng { get; set; }
        
        /// <summary>
        /// Object for managing all graphics operations
        /// </summary>
        public GraphicsDeviceManager Graphics { get; set; }
        
        /// <summary>
        /// Manages all content operations and draw calls
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Default blank texture used for drawing rectangles for bricks and paddle
        /// </summary>
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// List of all bricks in the game
        /// </summary>
        public List<Brick> BrickList { get; set; }
        
        /// <summary>
        /// Ball that is used in the game
        /// </summary>
        public Ball Ball { get; set; }

        /// <summary>
        /// Default texture of the ball
        /// </summary>
        public Texture2D BallTexture { get; set; }
        
        /// <summary>
        /// Texture of the ball when it is below the paddle
        /// </summary>
        public Texture2D BallTextureBelowPaddle { get; set; }
        
        /// <summary>
        /// Paddle object used in the game
        /// </summary>
        public Paddle Paddle { get; set; }
        
        /// <summary>
        /// Position of the paddle at the start of the game
        /// </summary>
        public Rectangle StartPaddlePos { get; set; }

        /// <summary>
        /// Player's current score
        /// </summary>
        public int Score { get; set; }
        
        /// <summary>
        /// Number of lives remaining
        /// </summary>
        public int Lives { get; set; }
        
        /// <summary>
        /// Font file for displaying game info on screen
        /// </summary>
        public SpriteFont Roboto { get; set; }
        #endregion

        /// <summary>
        /// Sets up the content and window for the game
        /// </summary>
        public Game1()
        {
            Game = this;
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Initializes the necessary fields
        /// </summary>
        protected override void Initialize()
        {
            // Tells the game to run in borderless full screen
            Graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;

            Graphics.ApplyChanges();

            // Initializes the Random object
            Rng = new Random();

            // Stores the current window size in a variable for easy access
            WindowSize = GraphicsDevice.Viewport.Bounds;

            // Creates a default texture to allow drawing of colored rectangles
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.White });

            #region Paddle Start Rectangle
            int paddleWidth = WindowSize.Width / 10;                            // Sets paddle with to 10% of window size
            int paddleHeight = 21;                                              // Sets paddle height to 20, regardless of window height

            int paddleStartX = (WindowSize.Width / 2) - (paddleWidth / 2);      // Centers the paddle horizontally at the start of game
            int paddleStartY = WindowSize.Height - 150;                         // Paddle y-pos is always 150 pixels above bottom of window
            #endregion

            // Defines the starting position of the paddle using the variables defined above
            StartPaddlePos = new Rectangle(paddleStartX, paddleStartY, paddleWidth, paddleHeight);

            // Creates a paddle object with the start position as defined above
            Paddle = new Paddle(Texture, StartPaddlePos, Color.Black);

            ResetGame();

            // Initializes the custom devcade controls
            Devcade.Input.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// Loads content from the content manager
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads ball texture from content editor and creates a new
            // ball object with that texture
            BallTexture = Content.Load<Texture2D>("ball");
            Ball = new Ball(BallTexture);

            // Loads alternate ball texture from content editor
            // This texture will be active when the ball is below the paddle
            BallTextureBelowPaddle = Content.Load<Texture2D>("surprised-pikachu");

            // Loads the font used to draw text on the screen
            Roboto = Content.Load<SpriteFont>("Roboto");
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

            // Loops through and removes all broken bricks from the list
            for (int i = 0; i < BrickList.Count; i++)
            {
                if (BrickList[i].Broken)
                {
                    BrickList.Remove(BrickList[i]);
                }
            }

            // Adds keybind to reset the game
            if ((Lives == 0 || BrickList.Count == 0)
                && (Keyboard.GetState().IsKeyDown(Keys.R)
                || GamePad.GetState(PlayerIndex.One).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A3)
                || GamePad.GetState(PlayerIndex.Two).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A3)))
            {
                ResetGame();
            }

            // Updates ball and paddle
            Ball.Update(gameTime, Paddle, BrickList);

            // Paddle can't move while ball isn't moving
            if (Ball.Velocity != new Vector2(0f, 0f))
            {
                Paddle.Update(gameTime);
            }

            // Updates the devcade inputs each frame
            // This allows the input checks to work properly
            Devcade.Input.Update();

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

            // Gathers all the draw calls and performs them all at once
            SpriteBatch.Begin();

            // Ends the game if no lives are left
            if (Lives == 0)
            {
                // Clears the background and removes all objects from screen
                GraphicsDevice.Clear(Color.CornflowerBlue);
                BrickList.Clear();

                SpriteBatch.DrawString(Roboto, 
                    "Game Over!", 
                    new Vector2(WindowSize.Center.X - 60, WindowSize.Center.Y - 30), 
                    Color.Black);

                SpriteBatch.DrawString(Roboto, 
                    "Press the green button to start a new game.", 
                    new Vector2(WindowSize.Center.X - 230, WindowSize.Center.Y + 30), 
                    Color.Black);
            }

            // Tells user they won if they have any lives and no bricks left
            else if (Lives != 0 && BrickList.Count == 0)
            {
                SpriteBatch.DrawString(Roboto, 
                    "You Win!", 
                    new Vector2(WindowSize.Center.X - 60, WindowSize.Center.Y - 30), 
                    Color.Black);

                SpriteBatch.DrawString(Roboto, 
                    "Press the green button to start a new game.", 
                    new Vector2(WindowSize.Center.X - 210, WindowSize.Center.Y + 30), 
                    Color.Black);
            }


            // Draws all bricks in the list that are not broken
            for (int i = 0; i < BrickList.Count; i++)
            {
                if (!BrickList[i].Broken)
                {
                    SpriteBatch.Draw(Texture, BrickList[i].Hitbox, new Color(217, 15, 42));
                }
            }

            // Displays lives and score
            SpriteBatch.DrawString(Roboto, 
                $"Score: {Score}", 
                new Vector2(WindowSize.Width - 200, WindowSize.Top + 30), 
                Color.Black);

            SpriteBatch.DrawString(Roboto, 
                $"Lives: {Lives}", 
                new Vector2(50, WindowSize.Top + 30), 
                Color.Black);

            // Draws the ball and paddle only if lives is above 0
            if (Lives > 0)
            {
                Ball.Draw(SpriteBatch, Paddle, BallTextureBelowPaddle);
                Paddle.Draw(SpriteBatch);
            }

            // Clears the ball and paddle if no bricks are left
            if (BrickList.Count == 0)
            {
                Ball.Reset();
                Paddle.Reset();
            }

            // Finishes gathering draw calls, and draws them all at once
            SpriteBatch.End();
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
            int numRows = 15;
            int numCols = 8;

            // Number of pixels between bricks
            int brickSpacing = 5;

            // Greatest y-coordinate (farthest down) that bricks can be drawn
            int brickAreaHeight = WindowSize.Height / 2;

            // First row of bricks is 80 pixels below top of window
            int brickAreaTopOffset = 100;

            // Width and height of each brick (scales based on window size
            int brickWidth = (WindowSize.Width - ((numCols * brickSpacing) + brickSpacing)) / numCols;
            int brickHeight = (brickAreaHeight - ((numRows * brickSpacing) + brickSpacing)) / numRows;
            #endregion

            // Clears the list of bricks
            BrickList = new List<Brick>();

            // Adds all the bricks to the list
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    Brick currBrick = new Brick(new Rectangle(
                        5 + (col * (brickWidth + brickSpacing)),                    // x-position of brick
                        brickAreaTopOffset + (row * (brickHeight + brickSpacing)),  // y-position of brick
                        brickWidth,                                                 // Width of brick
                        brickHeight));                                              // Height of brick

                    BrickList.Add(currBrick);
                }
            }
        }

        /// <summary>
        /// Resets all elements of the game
        /// </summary>
        public void ResetGame()
        {
            ResetBricks();
            ResetBall();
            ResetPaddle();
            ResetScore();
            ResetLives();
        }

        /// <summary>
        /// Resets the position of the ball
        /// </summary>
        public void ResetBall()
        {
            Ball = new Ball(BallTexture);
        }

        /// <summary>
        /// Resets the position of the paddle
        /// </summary>
        public void ResetPaddle()
        {
            Paddle = new Paddle(Texture, StartPaddlePos, Color.Black);
        }

        /// <summary>
        /// Resets the score to 0
        /// </summary>
        public void ResetScore()
        {
            Score = 0;
        }

        /// <summary>
        /// Resets lives to 5
        /// </summary>
        public void ResetLives()
        {
            Lives = 5;
        }
    }
}