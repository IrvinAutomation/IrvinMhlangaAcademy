using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
     class ComplexCalculator
    {
        static void Main(string[] args)
        {

            Console.Write("Enter First number: ");
            double number1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter a operator: ");
            String Op = Console.ReadLine();

            Console.Write("Enter Second Number: ");
            double number2 = Convert.ToDouble(Console.ReadLine());

            if (Op == "*")
            {
                Console.WriteLine("Your Multiplication Answer is: "+number1 *  number2);
            }
            else if(Op == "+")
            {
                Console.WriteLine("Your Addition Answer is: " +(number1 + number2));
            }
            else if (Op == "-")
            {
                Console.WriteLine("Your Subtraction Answer is: " +(number1-number2));
            }
            else if(Op == "/")
            {
                Console.WriteLine("Your Division Answer is: " +number1 / number2);
            }
            else
            {
                Console.WriteLine("Invalid operator: Enter a correct one and try again");
            }

        } 
    }
}
