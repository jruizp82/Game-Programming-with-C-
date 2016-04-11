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

using XnaCards;

namespace ProgrammingAssignment6
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        
        // keep track of game state and current winner
        static GameState gameState = GameState.Play;
        Player currentWinner = Player.None;

        // hands and battle piles for the players
        WarHand handPlayer1;
        WarHand handPlayer2;
        WarBattlePile pilePlayer1;
        WarBattlePile pilePlayer2;

        // winner messages for players
        WinnerMessage winnerPlayer1;
        WinnerMessage winnerPlayer2;

        // menu buttons
        MenuButton flipCards;
        MenuButton quitGame;
        MenuButton collectWinnings;             
         
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // make mouse visible and set resolution
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
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
            // TODO: Add your initialization logic here

            base.Initialize();
            // collectWinnings.Visible = false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create the deck object and shuffle
            Deck deck = new Deck(Content, 0, 0);
            deck.Shuffle();

            // create the player hands and fully deal the deck into the hands
            // WINDOW_HEIGHT / 6 and WINDOW_HEIGHT * 5/6
            handPlayer1 = new WarHand(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 500);
            handPlayer2 = new WarHand(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 100);
            
            //while (!deck.Empty)
            for (int i = 0; i < 2; i++)
            {
                handPlayer1.AddCard(deck.TakeTopCard());
                handPlayer2.AddCard(deck.TakeTopCard());
            }

            // create the player battle piles
            // WINDOW_HEIGHT / 3 and WINDOW_HEIGHT * 2/3
            pilePlayer1 = new WarBattlePile(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 380);
            pilePlayer2 = new WarBattlePile(WINDOW_WIDTH / 2, WINDOW_HEIGHT - 220);

            // create the player winner messages
            // WINDOW_WIDHT * 3 / 4, WINDOW_HEIGHT / 6 AND WINDOW_HEIGHT * 5 / 6
            winnerPlayer1 = new WinnerMessage(Content, WINDOW_WIDTH - 200, WINDOW_HEIGHT - 500);
            winnerPlayer2 = new WinnerMessage(Content, WINDOW_WIDTH - 200, WINDOW_HEIGHT - 100);

            // create the menu buttons  
            // WINDOW_WIDTH / 5, WINDOW_HEIGHT / 4
            flipCards = new MenuButton(Content, "flipbutton", WINDOW_WIDTH - 650, 
                WINDOW_HEIGHT - 450, GameState.Flip);
            // WINDOW_WIDTH / 5, WINDOW_HEIGHT * 3 / 4
            quitGame = new MenuButton(Content, "quitbutton", WINDOW_WIDTH - 650, 
                WINDOW_HEIGHT - 150, GameState.Quit);
            // WINDOW_WIDTH / 5, WINDOW_HEIGHT / 2
            collectWinnings = new MenuButton(Content, "collectwinningsbutton", WINDOW_WIDTH - 650,
                WINDOW_HEIGHT - 300, GameState.Play); // GameState.CollectWinnings
            collectWinnings.Visible = false;
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


            // update the menu buttons
            MouseState mouse = Mouse.GetState();
            flipCards.Update(mouse);
            collectWinnings.Update(mouse);
            quitGame.Update(mouse);
 
            // update based on game state
            if (gameState == GameState.Flip)
            {
                // FLIP CARDS INTO BATTLE PILES
                Card player1Card = handPlayer1.TakeTopCard();
                player1Card.FlipOver();
                pilePlayer1.AddCard(player1Card);
                Card player2Card = handPlayer2.TakeTopCard();
                player2Card.FlipOver();
                pilePlayer2.AddCard(player2Card);

                //Otherwise
                //pilePlayer1.AddCard(handPlayer1.TakeTopCard());
                //pilePlayer1.GetTopCard().FlipOver();
                //pilePlayer2.AddCard(handPlayer2.TakeTopCard());
                //pilePlayer2.GetTopCard().FlipOver();

                // FIGURE OUT WINNER AND SHOW MESSAGE

                if (player1Card.WarValue > player2Card.WarValue)
                {
                    winnerPlayer1.Visible = true;
                    currentWinner = Player.Player1;
                }
                else if (player1Card.WarValue < player2Card.WarValue)
                {
                    winnerPlayer2.Visible = true;
                    currentWinner = Player.Player2;
                }
                else
                {
                    currentWinner = Player.None;
                }

                // Otherwise
                //if (pilePlayer1.GetTopCard().WarValue > pilePlayer2.GetTopCard().WarValue)
                //{
                //    winnerPlayer1.Visible = true;
                //}
                //else if (pilePlayer1.GetTopCard().WarValue < pilePlayer2.GetTopCard().WarValue)
                //{
                //    winnerPlayer2.Visible = true;
                //}

                // gameState = GameState.Play;

                // adjust button visibility
                flipCards.Visible = false;
                collectWinnings.Visible = true;

                // wait for player to collect winnings
                gameState = GameState.CollectWinnings;
            }

            else if (gameState == GameState.Play)
            {
                // distribute battle piles into appropiate hands and hide message
                if (currentWinner == Player.Player1)
                {
                    handPlayer1.AddCards(pilePlayer1);
                    handPlayer1.AddCards(pilePlayer2);
                    winnerPlayer1.Visible = false;
                }
                else if (currentWinner == Player.Player2)
                {
                    handPlayer2.AddCards(pilePlayer1);
                    handPlayer2.AddCards(pilePlayer2);
                    winnerPlayer2.Visible = false;
                }
                else
                {
                    handPlayer1.AddCards(pilePlayer1);
                    handPlayer2.AddCards(pilePlayer2);
                }
                currentWinner = Player.None;

                //Otherwise
                //if (pilePlayer1.GetTopCard().WarValue > pilePlayer2.GetTopCard().WarValue)
                //{
                //    handPlayer1.AddCards(pilePlayer1);
                //    handPlayer1.AddCards(pilePlayer2);
                //    winnerPlayer1.Visible = false;
                //}
                //else if (pilePlayer1.GetTopCard().WarValue < pilePlayer2.GetTopCard().WarValue)
                //{
                //    handPlayer2.AddCards(pilePlayer1);
                //    handPlayer2.AddCards(pilePlayer2);
                //    winnerPlayer2.Visible = false;
                //}
                //else
                //{
                //    handPlayer1.AddCards(pilePlayer1);
                //    handPlayer2.AddCards(pilePlayer2);
                //}

                // set flip button visibility
                flipCards.Visible = true;
                collectWinnings.Visible = false;
                
                //gameState = GameState.Play;

                // check for game over

                if (handPlayer1.Empty || handPlayer2.Empty)
                {
                    flipCards.Visible = false;
                    gameState = GameState.GameOver;
                    if (handPlayer1.Empty)
                    {
                        winnerPlayer2.Visible = true;
                    }
                    else
                    {
                        winnerPlayer1.Visible = true;
                    }
                }
            }

            //Otherwise
            //if (gameState == GameState.GameOver)
            //{
            //    collectWinnings.Visible = false;
            //    flipCards.Visible = false;
            //    if (handPlayer1.Empty)
            //    {
            //        winnerPlayer2.Visible = true;
            //    }
            //    else 
            //    {
            //        winnerPlayer1.Visible = true;
            //    }
            //}

            else if (gameState == GameState.Quit)
            {
                Exit();
            }

            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);

            spriteBatch.Begin();

            // draw the game objects
            handPlayer1.Draw(spriteBatch);
            handPlayer2.Draw(spriteBatch);
            pilePlayer1.Draw(spriteBatch);
            pilePlayer2.Draw(spriteBatch);

            // draw the winner messages
            winnerPlayer1.Draw(spriteBatch);
            winnerPlayer2.Draw(spriteBatch);
 
            // draw the menu buttons
            flipCards.Draw(spriteBatch);
            quitGame.Draw(spriteBatch);
            collectWinnings.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            gameState = newState;
        }
    }
}
