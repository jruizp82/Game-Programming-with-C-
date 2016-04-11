using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingAssignment1
{
    /// <summary>
    /// Program that keeps statistics about player performance
    /// </summary>
    class Program
    {
        /// <summary>
        /// Program that keeps statistics about the gold that a player have earned
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // Print a message with the purpose of the program
            Console.WriteLine("This application will calculate your average gold-collecting performance.");
            Console.WriteLine();

            // Print a message asking for the total gold the user have collected
            Console.Write("How much gold have you collected in the game?: ");
            
            // Read in the total gold collected and put the value 
            // into a variable of the appropriate type
            int gold = int.Parse(Console.ReadLine());

            // Print a message asking for the total number of hours the user have played the game
            Console.Write("How many hours have you played the game?: ");
            
            // Read in the total numbers of hours played and put the value 
            // into a variable of the appropriate type
            float hours = float.Parse(Console.ReadLine());
            
            // Declare a constant
            const int MINUTES_PER_HOUR = 60;
            
            // Convert the hours to minutes
            float minutes = hours * MINUTES_PER_HOUR;

            // Calculate the gold per minute statistic
            float goldPerMinute = gold / minutes;

            // Print out the gold, hours played and gold per minute
            Console.WriteLine("Gold: " + gold);
            Console.WriteLine("Hours played: " + hours);
            Console.WriteLine("Gold per minute: " + goldPerMinute);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
