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
            int[] LuckyNumber = { 20, 40, 60, 80, 100 };

            for (int i = 0; i < LuckyNumber.Length; i++)
            {
                Console.WriteLine(LuckyNumber[i]);
                
            }
           
           
        }
    }
}
