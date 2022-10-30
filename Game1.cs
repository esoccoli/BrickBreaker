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
        public static Game1 game;
        public Rectangle windowSize;

        // Items that manage the content
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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
            // Updates the windiw size to 9:21 aspect ratio
            _graphics.PreferredBackBufferWidth = 420;
            _graphics.PreferredBackBufferHeight = 980;
            _graphics.ApplyChanges();

            // Stores the current window size in a variable for easy access
            windowSize = GraphicsDevice.Viewport.Bounds;

            // Creates a default texture to allow drawing of colored rectangles
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            // Defines the starting position of the paddle
            startPaddlePos = new Rectangle(windowSize.Width / 2 - 50, windowSize.Height - 150, 115, 20);

            // Creates a paddle object
            paddle = new Paddle(_texture, startPaddlePos, Color.Black);
            
            // Resets the game
            Reset();

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

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Reset();
            }

            // Updates ball and paddle
            ball.Update(gameTime, paddle, brickList);
            paddle.Update(gameTime);
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

            // Draws all bricks in the list that are not broken
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].Broken == false)
                {
                    _spriteBatch.Draw(_texture, brickList[i].Hitbox, Color.Red);
                }
            }

            // Draws the ball and paddle
            _spriteBatch.DrawString(arial, $"Score: {score}", new Vector2(300f, 20f), Color.Black);
            ball.Draw(_spriteBatch, paddle, ballTextureBelowPaddle);
            paddle.Draw(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets the state of all the bricks and sets the ball to the initial position
        /// </summary>
        public void Reset()
        {
            // Clears the list of bricks
            brickList = new List<Brick>();

            // Adds 60 bricks to the list, in 15 rows and 4 columns
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Brick currBrick = new Brick(new Rectangle(13 + col * 80, 65 + row * 30, 75, 25));
                    brickList.Add(currBrick);
                }
            }

            // Resets the position of the ball
            ball = new Ball(ballTexture);
            paddle = new Paddle(_texture, startPaddlePos, Color.Black);

            // Sets the score to 0 at the start of the game
            score = 0;

        }
    }
}