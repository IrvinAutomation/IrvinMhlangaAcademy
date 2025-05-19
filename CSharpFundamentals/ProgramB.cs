using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
     class ProgramB
    {
        public static void Main(string[] args)
        {
            Book book1 = new Book("Irvin Mhlanga", "How to be a best Autonation QA", 105);

            Book book2 = new Book("Sibusiso Mhlanga", "Autonation QA the best one", 455);
            book1.Pages = 500;
            Console.Write(book1.Pages);


        }
    }
}
