using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static Game1 game;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        
        private Brick refBrick;
        private Ball ball;
        private Paddle paddle;

        private Texture2D ballTextureBelowPaddle;
        private Texture2D ballTexture;
        internal List<Brick> brickList;

        public int score;
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
            //_graphics.IsFullScreen = true;
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

            // Creates a default texture to allow drawing of colored rectangles
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            
            Rectangle windowSize = GraphicsDevice.Viewport.Bounds;

            paddle = new Paddle(new Rectangle(windowSize.Width / 2 - 50, windowSize.Height - 150, 115, 20), Color.Black, _texture);
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

            ballTextureBelowPaddle = Content.Load<Texture2D>("surprised-pikachu");

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
            ball.Draw(_spriteBatch, paddle, ballTextureBelowPaddle);
            paddle.Draw(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}