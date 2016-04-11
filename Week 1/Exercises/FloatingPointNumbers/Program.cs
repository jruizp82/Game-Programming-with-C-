using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FloatingPointNumbers
{
    /// <summary>
    /// Demonstrates floating point numbers
    /// </summary>
    class Program
    {
        /// <summary>
        /// Demonstrates floating point numbers
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // declare variables
            int score = 1356;
            int totalSecondsPlayed = 10000;

            // calculate and display result
            float pointsPerSecond = (float)score / totalSecondsPlayed;
            Console.WriteLine("Points per second: " + pointsPerSecond);

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
