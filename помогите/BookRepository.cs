using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using помогите_.Book;

namespace помогите_
{
    public class BookRepository
    {
        private BookRepository()
        {

        }

        static BookRepository instance;
        public static BookRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new BookRepository();
                return instance;
            }
        }

        internal IEnumerable<BookData> GetAllBooks(string sql)
        {
            var result = new List<BookData>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                BookData book = null;
                int id;
                while (reader.Read())
                {
                    id = reader.GetInt32("id");
                    book = result.FirstOrDefault(s => s.Id == id);
                    if (book == null)
                    {
                        book = new BookData();
                        result.Add(book);
                        book.Id = id;
                        book.Title = reader.GetString("Title");
                        book.Author = reader.GetString("Author");
                        book.Year = reader.GetInt32("Year");
                        book.Annotation = reader.GetString("Annotation");
                        if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                        {
                           
                        }
                    }
                   
                   
                }
            }

            return result;
        }

        internal void AddBook(BookData book)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = MySqlDB.Instance.GetAutoID("books");

            string sql = "INSERT INTO books VALUES (0, @book, @annotation, @year, @author)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("book", book.Title));
                mc.Parameters.Add(new MySqlParameter("annotation", book.Annotation));
                mc.Parameters.Add(new MySqlParameter("year",   book.Year));
                mc.Parameters.Add(new MySqlParameter("author", book.Author));
              
               
            }
        }

        internal void Remove(BookData book )
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM CrossDrinkTag WHERE idbook = '" + book.Id + "';";
            sql += "DELETE FROM book WHERE id = '" + book.Id + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }

       

        internal void UpdateBook(BookData book )
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM books WHERE idbook = '" + book.Id + "';";
            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();

            sql = "";
           

            sql = "UPDATE books SET book = @book, annotation = @annotation, year = @year, author = @author = " + book.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("book", book.Title));
                mc.Parameters.Add(new MySqlParameter("annotation", book.Annotation));
                mc.Parameters.Add(new MySqlParameter("year", book.Year));
                mc.Parameters.Add(new MySqlParameter("author", book.Author));
               
                mc.ExecuteNonQuery();
            }
        }
    }
}
