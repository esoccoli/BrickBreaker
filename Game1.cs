using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BrickBreakerV2
{
    /// <summary>
    /// Manages the game and game updates
    /// </summary>
    public class Game1 : Game
    {
        public static Game1 game;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        
        private Brick refBrick;
        private Ball ball;
        private Paddle paddle;

        private Texture2D ballTexture;
        private List<Brick> brickList;

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
            _graphics.PreferredBackBufferWidth = 420;
            _graphics.PreferredBackBufferHeight = 980;
            _graphics.ApplyChanges();

            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Red });
            
            // TODO: Add your initialization logic here
            refBrick = new Brick(50, 70);
            ball = new Ball();
            paddle = new Paddle(200, 750, 90, 20, Color.White);

            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    brickList.Add(new Brick(5 + col * 80, 5 + row * 30));
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

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
        }

        /// <summary>
        /// Loops every frame and updates the game window
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            ball.X += 5;
            ball.Y += 5;
            base.Update(gameTime);
        }

        /// <summary>
        /// Loops every frame and draws the content on the screen
        /// </summary>
        /// <param name="gameTime">The time elapsed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            /*
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Brick currBrick = new(refBrick.X, refBrick.Y);
                    // Make condition to only draw the brick if it isnt broken
                    if (currBrick.Broken == false)
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(currBrick.X, currBrick.Y, currBrick.Width, currBrick.Height), Color.Red);
                    }
                    refBrick.X += 80;
                }
                refBrick.Y += 30;
                refBrick.X = 50;
            }
            refBrick.Y = 70;
            */

            for (int i = 0; i < brickList.Count; i++)
            {
                // Draw bricks
            }
            // TODO: Add your drawing code here

            _spriteBatch.Draw(ballTexture, new Vector2(ball.X, ball.Y), Color.White);
            _spriteBatch.Draw(_texture, new Rectangle(paddle.X, paddle.Y, paddle.Width, paddle.Height), Color.Green);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}