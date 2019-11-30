using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm
{
    class Book
    {
        public string Title { get; set; } 
        public string Author { get; set; }
        public int BookStatus { get; set; }
        public DateTime? DueDate { get; set; } // nullable date time - the question mark makes it nullable (normally date time can't be null)

        public Book()
        {

        }

        public Book(string title, string author, int bookstatus, DateTime? duedate)
        {
            Title = title;
            Author = author;
            BookStatus = bookstatus;
            DueDate = duedate;
        }

        internal bool contains(object keyword)
        {
            throw new NotImplementedException();
        }
    }

}
