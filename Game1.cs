using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BrickBreaker
{
    /// <summary>
    /// Manages the game and game updates
    /// </summary>
    public class Game1 : Game
    {
        #region Fields
        public static Game1 game;                   // Used to access game class data in other classes
        public Rectangle windowSize;                // Stores the current window size
        public Random rng;                          // Initializes a random object

        public GraphicsDeviceManager _graphics;     // Graphics manager
        public SpriteBatch _spriteBatch;            // Sprite manager
        private Texture2D _texture;                 // Blank texture (used to draw rectangles for bricks and paddle)

        internal List<Brick> brickList;             // List of bricks on the screen

        private Ball ball;                          // Ball object
        private Texture2D ballTexture;              // Default ball texture
        private Texture2D ballTextureBelowPaddle;   // Texture ball gets when it falls below the paddle

        private Paddle paddle;                      // Paddle object
        private Rectangle startPaddlePos;           // Position and size of the paddle when the game starts

        public int score;                           // Current score
        public int lives;                           // Number of lives remaining
        internal SpriteFont Roboto;                  // Font file used to display text in the game window
        #endregion

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
            // Tells the game to run in borderless full screen
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;

            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // Initializes the Random object
            rng = new Random();

            // Stores the current window size in a variable for easy access
            windowSize = GraphicsDevice.Viewport.Bounds;

            // Creates a default texture to allow drawing of colored rectangles
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            #region Paddle Start Rectangle
            int paddleWidth = windowSize.Width / 10;                            // Sets paddle with to 10% of window size
            int paddleHeight = 21;                                              // Sets paddle height to 20, regardless of window height

            int paddleStartX = (windowSize.Width / 2) - (paddleWidth / 2);      // Centers the paddle horizontally at the start of game
            int paddleStartY = windowSize.Height - 150;                         // Paddle y-pos is always 150 pixels above bottom of window
            #endregion

            // Defines the starting position of the paddle using the variables defined above
            startPaddlePos = new Rectangle(paddleStartX, paddleStartY, paddleWidth, paddleHeight);

            // Creates a paddle object with the start position as defined above
            paddle = new Paddle(_texture, startPaddlePos, Color.Black);

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads ball texture from content editor and creates a new
            // ball object with that texture
            ballTexture = Content.Load<Texture2D>("ball");
            ball = new Ball(ballTexture);

            // Loads alternate ball texture from content editor
            // This texture will be active when the ball is below the paddle
            ballTextureBelowPaddle = Content.Load<Texture2D>("surprised-pikachu");

            // Loads the font used to draw text on the screen
            Roboto = Content.Load<SpriteFont>("Roboto");
        }

        /// <summary>
        /// Loops every frame and updates the game window
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            // Adds keybind to exit the game
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)                                                       // Escape key on keyboard
                || (GamePad.GetState(PlayerIndex.One).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.Menu)    // Player 1 button A4 on devcade
                && GamePad.GetState(PlayerIndex.Two).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.Menu)))   // Player 2 button A4 on devcade
            {
                Exit();
            }

            // Loops through and removes all broken bricks from the list
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].Broken == true)
                {
                    brickList.Remove(brickList[i]);
                }
            }

            // Adds keybind to reset the game
            if (Keyboard.GetState().IsKeyDown(Keys.R)                                                         // 'R' key on keyboard
                || GamePad.GetState(PlayerIndex.One).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A3)    // Player 1 A3 button on devcade
                || GamePad.GetState(PlayerIndex.Two).IsButtonDown((Buttons)Devcade.Input.ArcadeButtons.A3))   // Player 2 A3 button on devcade
            {
                ResetGame();
            }

            // Updates ball and paddle
            ball.Update(gameTime, paddle, brickList);
            
            // Paddle can't move while ball isn't moving
            if (ball.Velocity != new Vector2(0f, 0f))
            {
                paddle.Update(gameTime);
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
            _spriteBatch.Begin();

            // Ends the game if no lives are left
            if (lives == 0)
            {
                // Clears the background and removes all objects from screen
                GraphicsDevice.Clear(Color.CornflowerBlue);
                brickList.Clear();

                _spriteBatch.DrawString(Roboto, "Game Over!", new Vector2(windowSize.Center.X - 60, windowSize.Center.Y - 30), Color.Black);

                _spriteBatch.DrawString(Roboto, "Press 'A3' to start a new game.", new Vector2(windowSize.Center.X - 170, windowSize.Center.Y + 30), Color.Black);
            }

            // Tells user they won if they have any lives and no bricks left
            else if (lives != 0 && brickList.Count == 0)
            {
                _spriteBatch.DrawString(Roboto, "You Win!", new Vector2(windowSize.Center.X - 60, windowSize.Center.Y - 30), Color.Black);

                _spriteBatch.DrawString(Roboto, "Press 'A3' to start a new game.", new Vector2(windowSize.Center.X - 170, windowSize.Center.Y + 30), Color.Black);
            }
            

            // Draws all bricks in the list that are not broken
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].Broken == false)
                {
                    _spriteBatch.Draw(_texture, brickList[i].Hitbox, new Color(217, 15, 42));
                }
            }

            // Displays lives and score
            _spriteBatch.DrawString(Roboto, $"Score: {score}", new Vector2(windowSize.Width - 150, windowSize.Top + 30), Color.Black);

            _spriteBatch.DrawString(Roboto, $"Lives: {lives}", new Vector2(50, windowSize.Top + 30), Color.Black);

            // Draws the ball and paddle only if lives is above 0
            if (lives > 0)
            {
                ball.Draw(_spriteBatch, paddle, ballTextureBelowPaddle);
                paddle.Draw(_spriteBatch);
            }

            // Clears the ball and paddle if no bricks are left
            if (brickList.Count == 0)
            {
                ball.Reset();
                paddle.Reset();
            }

            // Finishes gathering draw calls, and draws them all at once
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
            int numRows = 15;
            int numCols = 8;

            // Number of pixels between bricks
            int brickSpacing = 5;

            // Greatest y-coordinate (farthest down) that bricks can be drawn
            int brickAreaHeight = windowSize.Height / 2;

            // First row of bricks is 80 pixels below top of window
            int brickAreaTopOffset = 100;

            // Width and height of each brick (scales based on window size
            int brickWidth = (windowSize.Width - ((numCols * brickSpacing) + brickSpacing)) / numCols;
            int brickHeight = (brickAreaHeight - ((numRows * brickSpacing) + brickSpacing)) / numRows;
            #endregion

            // Clears the list of bricks
            brickList = new List<Brick>();

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

                    brickList.Add(currBrick);
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
            ball = new Ball(ballTexture);
        }

        /// <summary>
        /// Resets the position of the paddle
        /// </summary>
        public void ResetPaddle()
        {
            paddle = new Paddle(_texture, startPaddlePos, Color.Black);
        }

        /// <summary>
        /// Resets the score to 0
        /// </summary>
        public void ResetScore()
        {
            score = 0;
        }

        /// <summary>
        /// Resets lives to 5
        /// </summary>
        public void ResetLives()
        {
            lives = 5;
        }
    }
}