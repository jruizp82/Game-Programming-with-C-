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

namespace GameProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // declare variables used to set Screen Width & Height
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // game state
        GameState gameState = GameState.Menu;

        // Increment 1: opening screen fields
        Texture2D openingScreen;
        Rectangle openingScreenRectangle;

        // Increment 2: the board
        NumberBoard numberBoard;       

        // Random Number field
        Random randomNumber = new Random();

        // audio components
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Increment 1: set window resolution
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            // Increment 1: make the mouse visible            
            IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            // load audio content
            audioEngine = new AudioEngine(@"Content\Sounds.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");

            // Increment 1: load opening screen and set opening screen draw rectangle
            openingScreen = Content.Load<Texture2D>("openingscreen");
            openingScreenRectangle = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            // call the StartGame method
            StartGame();
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
            KeyboardState keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // Increment 2: change game state if game state is GameState.Menu and user presses Enter
            if (gameState == GameState.Menu && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gameState = GameState.Play;
            }

            // if we're actually playing, update mouse state and update board
            if (gameState == GameState.Play)
            {
                bool guessedNumber = numberBoard.Update(gameTime, Mouse.GetState());
                if (guessedNumber)
                {
                    soundBank.PlayCue("newGame");
                    StartGame();                    
                }
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

            // Increments 1 and 2: draw appropriate items here
            spriteBatch.Begin();

            if (gameState == GameState.Menu)
            {
                spriteBatch.Draw(openingScreen, openingScreenRectangle, Color.White);
            }
            
            else
            {
                numberBoard.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);

        }

        /// <summary>
        /// Starts a game
        /// </summary>
        void StartGame()
        {
            // randomly generate new number for game
            int correctNumber = randomNumber.Next(1, 10);

            // create the board object (this will be moved before you're done)
            // The board should be centered in the window and should be smaller 
            // than the window width and height.
            Vector2 center = new Vector2();
            center.X = WINDOW_WIDTH / 2;
            center.Y = WINDOW_HEIGHT / 2;

            // I take away 0.2 part of the height window
            int sideLength = (int)(WINDOW_HEIGHT * 0.8);

            numberBoard = new NumberBoard(Content, center, sideLength, correctNumber, soundBank);
        }
    }
}
