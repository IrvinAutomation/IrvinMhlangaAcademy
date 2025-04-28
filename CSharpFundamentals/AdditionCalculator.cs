using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
     class AdditionCalculator
    {
        static void Main(string[] args)
        {
            
            Console.Write("Enter a Number: ");
            double Num1 =Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter a Operator: ");
            string Operator = Console.ReadLine();

            Console.Write("Enter a Second Number: ");
            double Num2= Convert.ToDouble(Console.ReadLine());

             if (Operator == "+")
             {
                 Console.WriteLine($"Your Results are: {Num1 + Num2}");
             }
             else if (Operator == "-")
             {
                 Console.WriteLine($"Your Results are: {Num1 - Num2}");
             }
             else if (Operator == "*")
             {
                 Console.WriteLine($"Your Results are: {Num1 * Num2}");
             }
             else if (Operator == "/")
             {
                 Console.WriteLine($"Your Results are: {Num1 / Num2}");
             }
             else
             {
                 Console.WriteLine("Please Enter a VALID Operator between * , / , + , - ");
             }

        } 
    }
}
