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

        SpriteFont font,scoreFont;
        GraphicsDevice device;
        Texture2D goalTexture; // Texture for the goals
        Texture2D fieldTexture; // Texture for the goals
        Rectangle blueplayerGoal,redplayerGoal; // goal 1,2 Rectangle
        Rectangle fieldRectnagle;
        int screenWidth;
        int screenHeight;
        Player bluePlayer, redPlayer;
        int playerTurn;
        Frizbi frizbi;

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
            bluePlayer = new Player();
            redPlayer = new Player();
            frizbi = new Frizbi();
            playerTurn = 1;
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
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            //font
            font = Content.Load<SpriteFont>(@"font");
            scoreFont = Content.Load<SpriteFont>(@"ScoreFont");
            // field
            goalTexture = Content.Load<Texture2D>(@"goal");
            fieldTexture = Content.Load<Texture2D>(@"Football_field");
            blueplayerGoal = new Rectangle(42, (screenHeight / 2) - 80, 32, 160);
            redplayerGoal = new Rectangle(screenWidth - 70, (screenHeight / 2) - 80, 32, 160);
            fieldRectnagle = new Rectangle(0, 0, screenWidth, screenHeight);
            // Load the player resources 
            redPlayer.Initialize(Content.Load<Texture2D>(@"playerR"), new Vector2(screenWidth-211,screenHeight/2),Color.Red,font,screenHeight,screenWidth,blueplayerGoal);
            bluePlayer.Initialize(Content.Load<Texture2D>(@"playerL"), new Vector2(105, screenHeight / 2), Color.Blue, font, screenHeight, screenWidth,redplayerGoal);
            frizbi.Initialize(Content.Load<Texture2D>(@"FRIZBI"),new Vector2(105, (screenHeight / 2)-15),screenHeight, screenWidth);
            bluePlayer.moveChosenToFrizbi(frizbi);
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

            if (playerTurn != 1)
            {
                if (!frizbi.getAtPlayerHand) 
                    redPlayer.moveChosenToFrizbi(frizbi);
                playerTurn *= redPlayer.Update(frizbi);
            }
            else
            {
                if (!frizbi.getAtPlayerHand)
                    bluePlayer.moveChosenToFrizbi(frizbi);
                playerTurn *= bluePlayer.Update(frizbi);
            }
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
            spriteBatch.DrawString(scoreFont, bluePlayer.Score + ":" + redPlayer.Score, new Vector2((screenWidth/2)-33,15), Color.Black);
            redPlayer.Draw(spriteBatch);
            bluePlayer.Draw(spriteBatch);
            frizbi.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawField() 
        {
            spriteBatch.Draw(fieldTexture, fieldRectnagle, Color.White);
            spriteBatch.Draw(goalTexture, blueplayerGoal, Color.Blue);
            spriteBatch.Draw(goalTexture, redplayerGoal, Color.Red);
            }
    }
}
