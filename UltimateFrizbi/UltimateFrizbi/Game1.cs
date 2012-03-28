using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace UltimateFrizbi
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GraphicsDevice device;
        Texture2D goalTexture; // Texture for the goals
        Texture2D fieldTexture; // Texture for the goals
        Rectangle player1Goal,player2Goal; // goal 1,2 Rectangle
        Rectangle fieldRectnagle;
        int screenWidth;
        int screenHeight;
        player bluePlayer, redPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region screenInit
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 640;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "UltimateFrizibi";
            #endregion

            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();
            bluePlayer = new player();
            redPlayer = new player();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            goalTexture = Content.Load<Texture2D>(@"goal");
            fieldTexture = Content.Load<Texture2D>(@"Football_field");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            player1Goal = new Rectangle(20, (screenHeight / 2) - 64, 32, 128);
            player2Goal = new Rectangle(screenWidth - 52, (screenHeight / 2) - 64, 32, 128);
            fieldRectnagle = new Rectangle(0, 0, screenWidth, screenHeight);
            // Load the player resources 
            redPlayer.Initialize(Content.Load<Texture2D>(@"player"), new Vector2(50,screenHeight/2),Color.OrangeRed);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawField();
            redPlayer.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawField() 
        {
            spriteBatch.Draw(fieldTexture, fieldRectnagle, Color.White);
            spriteBatch.Draw(goalTexture, player1Goal, Color.Blue);
            spriteBatch.Draw(goalTexture, player2Goal, Color.Red);
        }
    }
}
