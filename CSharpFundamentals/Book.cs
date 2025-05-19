using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals
{
     class Book
    {
        public string Title;
        public string author;
        private int pages;
       

        public Book(string aTitle, string aAuthor, int aPages) 
        {
            Title = aTitle;
            author = aAuthor;
            Pages = aPages;

        }
        public int Pages
        {
            get { return pages;}
            set {if (value == 105 || value == 250 || value == 350 || value == 450)
                {
                    pages = value;
                }
                else 
                {
                    pages = 0;

                }
                
           }
            



        }
    }

}
