using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class TwoDimensionalArray
    {
        public static void Main(string[] args)
        {
            int[,] NumberGrid = {{1,2}, {3,4}, {5,6}};

            //Console.WriteLine(NumberGrid[2,0]);

            int[,] myArray = new int[2, 3];
            myArray[0,0] = 1;
            myArray[0,1] = 2;
            myArray[1,0] = 3;
            myArray[1,1] = 4;
          

            Console.WriteLine(myArray[1,1]);

        }
    }
}
