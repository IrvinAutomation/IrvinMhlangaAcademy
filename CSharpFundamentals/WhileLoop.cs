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
            int index = 1;
            while (index <= 6)
            {
                Console.WriteLine(index);
                index++;
            }
        }
    }
}
