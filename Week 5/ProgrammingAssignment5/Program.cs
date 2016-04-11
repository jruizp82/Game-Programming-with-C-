using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// Programming Assignment 5: Game of War
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // Declare the variables
            Random randomNumber = new Random();
            int rollPlayer1;
            int rollPlayer2 = 0;
            int winsPlayer1;
            int winsPlayer2;
            bool playGame = true;

            Console.WriteLine();
            Console.WriteLine();
            // Print a "welcome" message
            Console.WriteLine("Welcome to War!");
            Console.WriteLine();
            Console.WriteLine();

            // keeps playing games of war
            while (playGame)
            {
                // Set both player win counts to 0
                winsPlayer1 = 0;
                winsPlayer2 = 0;
                // Loop for 21 battles
                for (int i = 0; i < 21; i++)
                {
                    // roll the die for player1 and player2
                    rollPlayer1 = randomNumber.Next(1, 14);
                    rollPlayer2 = randomNumber.Next(1, 14);
                    // rolls the die again whenever there is a tie 
                    while (rollPlayer1 == rollPlayer2)
                    {
                        Console.WriteLine("WAR!\tP1:" + rollPlayer1 + "\tP2:" + rollPlayer2);
                        rollPlayer1 = randomNumber.Next(1, 14);
                        rollPlayer2 = randomNumber.Next(1, 14);
                    }
                    Console.Write("BATTLE:\tP1:" + rollPlayer1 + "\tP2:" + rollPlayer2);
                    // determines battle winner
                    if (rollPlayer1 > rollPlayer2)
                    {
                        Console.WriteLine("\tP1 Wins!");
                        winsPlayer1++;
                    }
                    else
                    {
                        Console.WriteLine("\tP2 Wins!");
                        winsPlayer2++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                // determines war winner
                if (winsPlayer1 > winsPlayer2)
                {
                    Console.WriteLine("P1 is the overall Winner with " + winsPlayer1 + " battles!");
                }
                else
                {
                    Console.WriteLine("P2 is the overall Winner with " + winsPlayer2 + " battles!");
                }
                Console.WriteLine();
                Console.WriteLine();
                // prompt for and get whether the player wants to play another game
                Console.Write("Do you want to play again (y/n)? ");
                char answer = char.Parse(Console.ReadLine());
                if (answer == 'n' || answer == 'N')
                {
                    playGame = false;
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
