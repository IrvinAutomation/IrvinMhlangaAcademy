using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class GuessGame
    {
        static void Main(string[] args)
        {
            string secretWord = "Anothe";
            string guessWord = "";
            int guessCount = 0;
            int guessLimit = 3;
            bool outOfGuesses = false;

            while (guessWord != secretWord && !outOfGuesses)
            {
                if (guessCount < guessLimit)
                {
                    Console.Write("Please Enter Secret Word: ");
                    guessWord = Console.ReadLine();
                    guessCount++;
                }
                else
                {
                    outOfGuesses = true;
                }
            }
            if (outOfGuesses)
            {
                Console.WriteLine("You Lose: ");
            }
            else
            {
                Console.WriteLine("You Win: ");
            }
        }
    }
}




