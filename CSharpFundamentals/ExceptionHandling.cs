using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class ExceptionHandling
    {

        public static void Main(string[] args)
        {

            try
            {
                Console.Write("Enter a Number: ");
                int Num1 = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter a Second Number: ");
                int Num2 = Convert.ToInt32(Console.ReadLine());

                Console.Write(Num1 / Num2);
                Console.Read();
            }
            catch (Exception e) //divideByZero e and FormatExcept e
            {
                Console.WriteLine(e.Message);
            }
           
          
        }
    }
}
