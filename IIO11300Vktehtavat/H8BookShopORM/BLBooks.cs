using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H8BookShopORM
{
    [Serializable]
    public class Book
    {
        #region PROPERTIES

        private int id;
        public int Id
        {
            //huom readonly
            get { return id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private int year;
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        #endregion
        #region CONSTRUCTORS
        public Book(int id)
        {
            this.id = id;
        }

        public Book(int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.country = country;
            this.year = year;
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return name + " written by " + author;
        }
        #endregion
    }
    public class BookShop
    {
        private static string cs = H8BookShopORM.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            //metodi palauttaa kokoelman Book-olioita
            List<Book> temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(2, "Anna Karenina", "Leo Tolstoi", "Venäjä", 1877));
            return temp;
        }

        public static List<Book> GetBooks(Boolean useDB)
        {
            try
            {
                DataTable dt;
                List<Book> temp = new List<Book>();
                //haetaan kirjoja, db-kerroksen palauttama datatable mapataan olioiksi eli tehdään ORM
                if (useDB)
                {
                    dt = DBBooks.GetBooks(cs);
                }
                else
                {
                   dt = DBBooks.GetTestData();
                }
                //tehdään ORM eli DataTablen rivit muutetaan olioiksi
                Book book;
                foreach (DataRow dr in dt.Rows)
                {
                    book = new Book((int)dr[0]);
                    book.Author = dr["author"].ToString();
                    book.Name = dr["name"].ToString();
                    book.Country = dr["country"].ToString();
                    book.Year = (int)dr["year"];
                    //olio lisätään kokoelmaan
                    temp.Add(book);
                }
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateBook(Book book)
        {
            try
            {
                int lkm = DBBooks.UpdateBook(cs, book.Id, book.Name, book.Author, book.Country, book.Year);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
