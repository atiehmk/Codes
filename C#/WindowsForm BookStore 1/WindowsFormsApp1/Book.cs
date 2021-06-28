using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{  /// <summary>
///  The Book class contains the book properties and methods 
/// </summary>
    class Book
    {   
       
        private string _title;     // Declare a title field of type string
        private string _author;   // Declare an author field of type string
        private string _ISBN;    // Declare a ISBN field of type string
        private double _price;   // Declare a price field of type double

        /// <summary>
        /// Define constructor for Book class 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="ISBN"></param>
        /// <param name="price"></param>
        public Book(string title, string author,string ISBN, double price)
        {
            _title = title;
            _author = author;
            _ISBN = ISBN;
            _price = price;

        }


        public Book()
        {

        }

        // Define accessors ( get and set accessors ) 

        public string Title   // property
        {
            get { return _title; }   // get method
            set { _title = value; }  // set method
        }

        public string Author   // property
        {
            get { return _author; }   // get method
            set { _author = value; }  // set method
        }
        public string ISBN   // property
        {
            get { return _ISBN; }   // get method
            set { _ISBN = value; }  // set method
        }

        public double Price   // property
        {
            get { return _price; }   // get method
            set { _price = value; }  // set method
        }


    }
}
