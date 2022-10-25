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
            //_graphics.IsFullScreen = true;
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
            _texture.SetData(new Color[] { Color.White });
            
            // TODO: Add your initialization logic here
            

            Rectangle windowSize = GraphicsDevice.Viewport.Bounds;

            paddle = new Paddle(new Rectangle(windowSize.Width / 2 - 50, windowSize.Height - 150, 100, 30), Color.Black, _texture);
            brickList = new List<Brick>();

            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Brick currBrick = new Brick(new Rectangle(13 + col * 80, 20 + row * 30, 75, 25));
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

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            ball = new Ball(ballTexture);

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

            ball.Update(gameTime);
            paddle.Update(gameTime);
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

            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].Broken == false)
                {
                    _spriteBatch.Draw(_texture, brickList[i].Hitbox, Color.Red);
                }
            }
            // TODO: Add your drawing code here

            ball.Draw(_spriteBatch);
            paddle.Draw(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}