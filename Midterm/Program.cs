using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Midterm
{
    class Program
    {
        public static List<Book> BookList { get; set; }  // *******************method*****************BookList is a property of Program.cs

        public static List<Book> CreateBookList()  // ******************method*********************FROM VIDEO
        {
            string filePath = @"C:\Users\Gsoltis\source\repos\GRAND CIRCUS - Labs\Midterm\Midterm\booklist.txt";  // literal path to the file on the disk

            List<Book> booklist = new List<Book>();  //<T> initilizing to empty list of Book List<Book>

            List<string> lines = File.ReadAllLines(filePath).ToList();  // HOW TO READ FROM A FILE ReadAllLines (SYSTEM.IO) AND GET A PATH(filePath) Will return an array of strings. 
                                                                        //allows to add to the list. Adding to string[] is more difficlut
                                                                        //ToList converts List<string> to a list. Does this which allows to add to the list. Adding to string[] array is more difficult so does List,string>

            foreach (string line in lines) // will loop over every line in List<string>. Reading all the lines from the text file. I'm converting to a list. Putting in this 
                                           //variable
            {
                string[] entries = line.Split(',');

                Book newBook = new Book();

                newBook.Title = entries[0];
                newBook.Author = entries[1];
                newBook.BookStatus = int.Parse(entries[2]);

                if (!string.IsNullOrEmpty(entries[3]) && entries[3] != "null")  // 
                {
                    newBook.DueDate = DateTime.Parse(entries[3]);
                }
                else
                {
                    newBook.DueDate = null;
                }

                booklist.Add(newBook);

                //Console.WriteLine(line);
            }
            return booklist;

        }
        // METHOD to display menu

        #region Options() Method to Display option questions in console
        public static void Options()
        {
            Console.WriteLine("\nWelcome to the library. Please choose a number from the following menu");
            Console.WriteLine("1. Search for a book to check out by AUTHOR NAME.");
            Console.WriteLine("2. Search for a book to check out by a WORD IN THE TITLE.");
            Console.WriteLine("3. Return a book.");
            Console.WriteLine("4. Display Current Book List.");
            Console.WriteLine("5. Exit the library app");
        }
        #endregion

        #region METHOD to Show Book List in Book List
        public static void DisplayBookList(List<Book> booklist)
        {
            Console.WriteLine($"{"Title",-40} {"Author",-20} {"Status",-15} {"Due Date",-10}");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (Book b in booklist)
            {
                string dateDue = "";
                string status;

                if (b.BookStatus == 0)
                {
                    status = "On Shelf";
                }
                else
                {
                    status = "Checked Out";
                }

                if (b.DueDate != null)
                {
                    dateDue = b.DueDate.Value.ToString("MM/dd/yyyy");
                }
                Console.WriteLine($"{b.Title,-40} {b.Author,-20} {status,-15} {dateDue,-10}");
            }

        }
        #endregion

        #region METHOD to search for book by Author
        public static Book GetBookByAuthorName(string titleByAuthor)
        {
            foreach (Book b in BookList)
            {
                if (b.Author.Contains(titleByAuthor))
                {
                    return b;  // returns book found by author name
                }
            }
            //if we are here we did not find the book; so return null
            return null;

        }
        #endregion

        #region Method to search for book by Keyword  -- One Book
        public static Book GetBookByKeyword(string titleByKeyword)
        { 
            foreach (Book b in BookList)
            {
                if (b.Title.Contains(titleByKeyword))
                {
                    return b;
                }
            }
            return null;
        }
        #endregion

        #region Method to search for book by Keyword -- List<Books>
        public static List<Book> GetBookListByKeyword(string titleByKeyword)
        {
            List<Book> MatchingBooks = new List<Book>();
            
            foreach (Book b in BookList)
            {
                if (b.Title.Contains(titleByKeyword))
                {
                    MatchingBooks.Add(b);
                }
            }
            return MatchingBooks;
        }
        #endregion


        static void Main(string[] args) // ************MAIN METHOD**************

        {
            #region Call CreateBookList Method here
            BookList = CreateBookList();  // ******************METHOD *************initialize the class property called  (List<Book> with CreateBookList Method)
            #endregion

            #region Call Method "DisplayBookList"
            DisplayBookList(BookList);
            #endregion

            //Call Method to display Options
            Options();
            
            int optionInput = int.Parse(Console.ReadLine());

            while (optionInput != 5)
            {
                if (optionInput == 1)
                {
                    #region CALL METHOD to check out book by Author Name
                    Console.WriteLine("Choose a book to check out by the Author's Name.");
                    string userInput = Console.ReadLine();
                    Book myBook = GetBookByAuthorName(userInput);

                    if (myBook != null)
                    {
                        Console.WriteLine($"FOUND BOOK:  {myBook.Title}");

                        if (myBook.BookStatus == 0)  // 0 = not checked out
                        {
                            Console.WriteLine("");

                            Console.WriteLine($"YOU ARE CHECKING OUT:   {myBook.Title}.");
                            Console.WriteLine($"The due date for {myBook.Title} is:   {DateTime.Now.AddDays(14).ToString("MM.dd.yyyy")}");

                            myBook.BookStatus = 1;  // updating book status to being checked out
                            myBook.DueDate = DateTime.Now.AddDays(14);  // creating due date for this newly checked out book

                            Console.WriteLine("");
                            Console.WriteLine("The updated Book List is below:\n");
                            DisplayBookList(BookList);
                            Console.WriteLine("");

                        }
                        else  //1 = book is already checked out 
                        {
                            Console.WriteLine($"{myBook.Title} is currently checked out. It is due back on {myBook.DueDate}.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Could not find book.");
                    }
                    #endregion
                }
                else if (optionInput == 2)
                {
                    #region CALL METHOD to GetBookListByKeyword
                    Console.WriteLine("Choose a book to check out by a word or letter in the book's title.");
                    string bookSearch = Console.ReadLine();
                    List<Book> MatchingBooklist = GetBookListByKeyword(bookSearch);

                    if (MatchingBooklist != null && MatchingBooklist.Count > 0)
                    {
                        Console.WriteLine($"\nBooks matching your search term '{bookSearch}' are listed below.");

                        DisplayBookList(MatchingBooklist);

                        foreach (Book b in MatchingBooklist)
                        {
                            if (b.BookStatus == 0)
                            {
                                Console.WriteLine($"Would you like to check out {b.Title}? yes or no");
                                string userReply = Console.ReadLine();

                                if (userReply == "yes")
                                {
                                    Console.WriteLine($"You are checking out:   {b.Title}.");
                                    Console.WriteLine($"The due date for {b.Title} is:   {DateTime.Now.AddDays(14).ToString("MM.dd.yyyy")}");

                                    b.BookStatus = 1;  // updating book status to being checked out
                                    b.DueDate = DateTime.Now.AddDays(14);  // creating due date for this newly checked out book
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{b.Title} is already checked out.");
                            }
                        }
                        Console.WriteLine("");
                        Console.WriteLine("The updated Book List is below:\n");
                        DisplayBookList(BookList);
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Could not find book.");
                    }
                }
                #endregion

                else if (optionInput == 3)
                {
                    #region RETURN BOOK
                    Console.WriteLine("Please enter the Title of the book you are returning?");
                    string bookReturn = Console.ReadLine();
                    Book returnBook = GetBookByKeyword(bookReturn);

                    if (returnBook != null)
                    {
                        returnBook.BookStatus = 0;
                        returnBook.DueDate = null;
                        Console.WriteLine($"{returnBook.Title} has been returned.");
                    }
                    else
                    {
                        Console.WriteLine("That book is not recognized.");
                    }
                }
                #endregion
                else if (optionInput == 4)
                {
                    DisplayBookList(BookList);
                }
                Options();
                optionInput = int.Parse(Console.ReadLine());
            }
            //User Exit - Write BookList back to text file
            



        }
    }
}

