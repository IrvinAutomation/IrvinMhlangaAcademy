using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
    public class WhileLoop
    {
        static void Main(string[] args)
        {
            int index = 20;
            while (index < 30)
            {
                Console.WriteLine(index);
                index++;
            }
        }
    }
}
