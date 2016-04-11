using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCards;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// Programming Assignment 3 - Blackjack
    /// </summary>
    class Program
    {
        /// <summary>
        /// Console application that plays Blackjack
        /// </summary>
        /// <param name="args">command-line args</param>

        static void Main(string[] args)
        {
            // Create a deck of cards
            Deck deck = new Deck();

            // Create blackjack hands for the dealer and the player
            BlackjackHand playerHand = new BlackjackHand("Player");
            BlackjackHand dealerHand = new BlackjackHand("Dealer");

            // Print a “welcome” message to the user
            Console.WriteLine("Welcome player. The program will play a single hand of Blackjack.");
            Console.WriteLine();

            // Shuffle the deck of cards
            deck.Shuffle();

            // Deal 2 cards to the player and dealer
            // Otherwise
            // Card card; // Create a card
            // card = deck.TakeTopCard();
            // playerHand.AddCard(card);
            // card = deck.TakeTopCard();
            // playerHand.AddCard(card);
            // card = deck.TakeTopCard();
            // dealerHand.AddCard(card);
            // card = deck.TakeTopCard();
            // dealerHand.AddCard(card);
            playerHand.AddCard(deck.TakeTopCard());
            dealerHand.AddCard(deck.TakeTopCard());
            playerHand.AddCard(deck.TakeTopCard());
            dealerHand.AddCard(deck.TakeTopCard());

            // Make all the player’s cards face up
            playerHand.ShowAllCards();

            //Make the dealer’s first card face up
            dealerHand.ShowFirstCard();
            
            // Print both the player’s hand and the dealer’s hand
            playerHand.Print();
            dealerHand.Print();
            
            // Let the player hit if they want to
            // Otherwhise
            // Console.Write("Hit or stand? (h or s): ");
            // char answer = Console.ReadLine()[0];
            // Console.WriteLine();
            //
            // if (answer == 'h')
            // {
            //     card = deck.TakeTopCard();
            //     playerHand.AddCard(card);
            //     playerHand.ShowAllCards();
            // }
            playerHand.HitOrNot(deck);
            
            // Make all the dealer’s cards face up
            dealerHand.ShowAllCards();

            // Print both the player’s hand and the dealer’s hand
            playerHand.Print();
            dealerHand.Print();
            
            // Print the scores for both hands
            // Otherwhise
            // int playerScore = playerHand.Score;
            // Console.WriteLine("The player score is: " + playerScore);
            // int dealerScore = dealerHand.Score;
            // Console.WriteLine("The dealer score is: " + dealerScore);
            Console.WriteLine("The player score is: " + playerHand.Score);
            Console.WriteLine("The dealer score is: " + dealerHand.Score);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
