using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class Program4
    {
        String Name;
        String Surname;

        public Program4(String Name)
        {
            this.Name = Name;  
        }

        public Program4()
        {
            
        }
        public Program4(String Name, String Surname)
        {
            this.Surname = Surname;
            this.Name = Name;
        }
        public void GetName()
        {
           Console.WriteLine("Using the Constructor that I learned, name is " +this.Name);
           Console.WriteLine("Using the Constructor that I learned, Surname is " +this.Surname); 
        }
        public void SetData()
        {
            Console.WriteLine("I am in the Parent method of program 4, about to be called in PROGRAM2");
            Console.WriteLine("Below is the data from PROGRAM4");

            var age = 25;
            Console.WriteLine($"My age is {age}");
            age = 15;
            Console.WriteLine($"My new age is {age}");
            dynamic height = 15.2;
            Console.WriteLine($"My height is {height}");
            height = "Tall";
            Console.WriteLine($"My new height is {height}");
            height = 200;
            Console.WriteLine("My latest height is " +height);

            Console.WriteLine("Using the Constructor that I learned, Midddle Name is " +this.Name);
              
        }
    }
  
}
