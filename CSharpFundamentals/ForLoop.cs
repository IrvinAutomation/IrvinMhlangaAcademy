using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class ForLoop
    {
        static void Main(string[] args)
        {
            int[] luckyNumbers = { 10, 20, 30, 40, 50, 60, 70, 80, 90}; 

            for (int index = 0; index < luckyNumbers.Length; index++)
            {
                Console.WriteLine(luckyNumbers[8]);
                break;
            }
        }
    }
}
