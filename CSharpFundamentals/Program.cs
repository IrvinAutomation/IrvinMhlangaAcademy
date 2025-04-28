using System.Diagnostics;

namespace CSharpFundamentals
{
    class Program : Program4
    {
        String name;
        String LastName;

        //method default constructor
        //constructor takes the name of your class
        //We use Constructor to initialize variables
        // Contructors and and objects should be in sync
        //WHen you initialize a contrcuctor, the object needs to be initialized as well
        //Should never put methods inside the main metho
        public Program(String name)
        {
            this.name = name;
        }

        public Program(String name, String LastName)
        {
            this.LastName = LastName;
        }

        public void getName()
        {
            Console.WriteLine("My First before the second one is" +this.name); 
            Console.WriteLine("My LastName is " +this.LastName);
        }

        public void getData()
        {
            Console.WriteLine("I am inside the first METHOD, METHOD, METHOD,METHOD");
        }


        static void Main(string[] args)
        {
            

            Program p = new Program("Irvin");
            p.getData();

            //Coming from Program4
            p.SetData();

            p.getName();

            Program p1 = new Program("Irvin", "Mhlanga");
            p1.getName();


            Console.WriteLine("Hello, Irvin!");

            int a = 35;
            Console.WriteLine("number is " +a);

            String name = "Irvin";
            Console.WriteLine("My name is " +name);

            Console.WriteLine($"My name is {name}");

            var age = 15;
            Console.WriteLine($"My age is {age}");

            dynamic height = 10.3;
            Console.WriteLine($"My height is {height}");

            height = "20.5";
            Console.WriteLine($"My height is {height}");
        }
    }

}

