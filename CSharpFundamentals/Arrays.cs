using OpenQA.Selenium.DevTools.V132.Debugger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class Arrays
    {
        static void Main(string[] args)
        {
            //Arrays stores a collection of elements

            int[] age = new int[4];
            age[0] = 20;
            age[1] = 30;
            age[2] = 50;
            age[3] = 60;
            Console.WriteLine(age[0]);

            String[] name = { "Irvin", "Sibusiso", "Mhlanga", "Samu", "Ona", "QA Engineer", "Khabako" };

            int[] number = { 10, 20, 30 };

            for (int i =0; i<name.Length; i++)
            {

                Console.WriteLine(name[i]);

                
                if (name[i] == "Irvin")
                {
                    Console.WriteLine("Match Found");
                    break;
                }
                else
                {
                    Console.WriteLine("Match Not Found");

                }
               
            }

            ArrayList a = new ArrayList();
            a.Add("Zimkhona");
            a.Add("Anothe");
            a.Add("Sibusiso");
            a.Add("Mhlanga");
            a.Add("Baby Steps");
            a.Add("Khabako");

            foreach (String item in a)
            {
                Console.WriteLine(item);

            }

            Console.WriteLine(a.Contains("Sibusiso"));

            Console.WriteLine("After Sorting");
            a.Sort();
            foreach (String item in a)
            {
                Console.WriteLine(item);
            }

        }

    }
}