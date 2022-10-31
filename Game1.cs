﻿using Microsoft.Xna.Framework;
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
        public static Game1 game;
        public Rectangle windowSize;
        public Random rng;

        // Items that manage the content
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private Texture2D _texture;
        
        // Data related to bricks
        internal List<Brick> brickList;

        // Data related to the ball
        private Ball ball;
        private Texture2D ballTextureBelowPaddle;
        private Texture2D ballTexture;

        // Data related to the paddle
        private Paddle paddle;
        private Rectangle startPaddlePos;

        // Misc data
        public int score;
        public int lives;
        internal SpriteFont arial;
        #endregion

        /// <summary>
        /// Sets up the content and window for the game
        /// </summary>
        public Game1()
        {
            game = this;
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the necessary fields
        /// </summary>
        protected override void Initialize()
        {
            // Updates the window size to 9:21 aspect ratio
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;

            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // Initializes the Random object
            rng = new Random();

            // Stores the current window size in a variable for easy access
            windowSize = GraphicsDevice.Viewport.Bounds;

            // Creates a default texture to allow drawing of colored rectangles
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            #region Paddle Start Rectangle
            int paddleWidth = windowSize.Width / 10;
            int paddleHeight = windowSize.Height / 50;

            int paddleStartX = (windowSize.Width / 2) - (paddleWidth / 2);
            int paddleStartY = windowSize.Height - 150;
            #endregion
            // Defines the starting position of the paddle
            startPaddlePos = new Rectangle(paddleStartX, paddleStartY, paddleWidth, paddleHeight);

            // Creates a paddle object
            paddle = new Paddle(_texture, startPaddlePos, Color.Black);

            // Resets the game
            ResetGame();

            base.Initialize();
        }
        
        /// <summary>
        /// Loads content from the content manager
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads the texture of the ball and uses it to create a ball object
            ballTexture = Content.Load<Texture2D>("ball");
            ball = new Ball(ballTexture);

            // Loads the texture that the ball gets when it falls below the paddle
            ballTextureBelowPaddle = Content.Load<Texture2D>("surprised-pikachu");

            // Loads the font used to draw text on the screen
            arial = Content.Load<SpriteFont>("arial");
        }

        /// <summary>
        /// Loops every frame and updates the game window
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            // Exits the game if the escape key or back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Loops through the list of bricks, and removes any broken ones
            for (int i = 0; i < brickList.Count; i++)
            {
                brickList[i].Update(gameTime, ball);
                if (brickList[i].Broken == true)
                {
                    brickList.Remove(brickList[i]);
                }
            }

            // Resets the game if the 'R' key is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                ResetGame();
            }

            // Updates ball and paddle
            ball.Update(gameTime, paddle, brickList);
            if (ball.Velocity != new Vector2(0f, 0f))
            {
                paddle.Update(gameTime);
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
            
            _spriteBatch.Begin();
            if (lives == 0)
            {
                // Clears the background and removes all objects from screen
                GraphicsDevice.Clear(Color.CornflowerBlue);
                brickList.Clear();

                _spriteBatch.DrawString(arial, "Game Over!", new Vector2(windowSize.Center.X - 60, windowSize.Center.Y - 30), Color.Black);

                _spriteBatch.DrawString(arial, "Press 'R' to start a new game.", new Vector2(windowSize.Center.X - 150, windowSize.Center.Y + 30), Color.Black);
            }
            else if (lives != 0 && brickList.Count == 0)
            {
                _spriteBatch.DrawString(arial, "You Win!", new Vector2(windowSize.Center.X - 60, windowSize.Center.Y - 30), Color.Black);

                _spriteBatch.DrawString(arial, "Press 'R' to start a new game.", new Vector2(windowSize.Center.X - 150, windowSize.Center.Y + 30), Color.Black);
            }
            

            // Draws all bricks in the list that are not broken
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].Broken == false)
                {
                    _spriteBatch.Draw(_texture, brickList[i].Hitbox, Color.Red);
                }
            }

            // Draws lives and score
            _spriteBatch.DrawString(arial, $"Score: {score}", new Vector2(windowSize.Width - 150, windowSize.Top + 30), Color.Black);

            _spriteBatch.DrawString(arial, $"Lives: {lives}", new Vector2(50, windowSize.Top + 30), Color.Black);

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

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets the state of all the bricks and sets the ball to the initial position
        /// </summary>
        public void ResetBricks()
        {
            #region Useful Variables
            // Number of rows/columns of bricks
            int numRows = 15;
            int numCols = 8;

            // Number of pixels between bricks
            int brickSpacing = 5;

            // Greatest y-coordinate (farthest down) that bricks can be drawn
            int brickAreaHeight = windowSize.Height / 2;

            // Number of pixels away from top of screen that the first brick is drawn
            int brickAreaTopOffset = 80;

            // The width of each brick (should scale based on window size)
            int brickWidth = (windowSize.Width - ((numCols * brickSpacing) + brickSpacing)) / numCols;

            // Height of each brick (should scale based on window size)
            int brickHeight = (brickAreaHeight - ((numRows * brickSpacing) + brickSpacing)) / numRows;

            #endregion

            // Clears the list of bricks
            brickList = new List<Brick>();

            // Adds 60 bricks to the list, in 15 rows and 4 columns
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    // brickSpacing = whatever you want
                    // brickAreaHeight = whatever you want i suggest maybe screenheight/3
                    // brickAreaOffsetFromTop = whatever you want, maybe 80
                    // brickwidth = (screenwidth - ((cols*brickSpacing) +brickSpacing))/cols
                    // brickHeight = (brickAreaHeight - ((rows*brickSpacing) +brickSpacing))/rows
                    // brickx = 5 + (col*(brickwidth+brickSpacing))
                    // bricky = brickAreaOffsetFromTop + (row*(brickheight+spacing))
                    Brick currBrick = new Brick(new Rectangle(5 + (col * (brickWidth + brickSpacing)), brickAreaTopOffset + (row * (brickHeight + brickSpacing)), brickWidth, brickHeight));
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