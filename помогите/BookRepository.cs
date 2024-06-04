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

        internal IEnumerable<BookData> GetAllBooks(int userID)
        {
            string sql = "SELECT books.id, books.book, books.annotation, books.`year`, books.author, users.firstname FROM books INNER JOIN users ON books.author = users.id";
            if (userID != 0)
                sql += " where books.author = " + userID;

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
                    book = new BookData();
                    result.Add(book);
                    book.Id = id;
                    book.Title = reader.GetString("book");
                    book.Author = reader.GetString("firstname");
                    book.AuthorID = reader.GetInt32("author");
                    book.Year = reader.GetInt32("year");
                    book.Annotation = reader.GetString("annotation");
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
                mc.Parameters.Add(new MySqlParameter("year", book.Year));
                mc.Parameters.Add(new MySqlParameter("author", book.AuthorID));
                mc.ExecuteNonQuery();

                if (book.Chapters.Count > 0)
                {
                    sql = "INSERT INTO chapter VALUES (0, @chapter_name, @info_chapter, @id_book)";
                    foreach (var chapter in book.Chapters)
                    {
                        using (var mcc = new MySqlCommand(sql, connect))
                        {
                            mcc.Parameters.Add(new MySqlParameter("chapter_name", chapter.Name));
                            mcc.Parameters.Add(new MySqlParameter("info_chapter", chapter.Content));
                            mcc.Parameters.Add(new MySqlParameter("id_book", id));

                            mcc.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        internal void Remove(BookData book)
        {
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "DELETE FROM CrossDrinkTag WHERE idbook = '" + book.Id + "';";
            sql += "DELETE FROM book WHERE id = '" + book.Id + "';";

            using (var mc = new MySqlCommand(sql, connect))
                mc.ExecuteNonQuery();
        }



        internal void UpdateBook(BookData book)
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

        internal void FillChapters(BookData selectedBook)
        {
            string sql = "SELECT chapter.id, chapter.chapter_name, chapter.info_chapter FROM chapter WHERE chapter.id_book = " + selectedBook.Id;

            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                selectedBook.Chapters.Clear();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    BookData.ChapterData ch = new BookData.ChapterData(reader.GetString("chapter_name"), reader.GetString("info_chapter"));
                    ch.Id = id;
                    selectedBook.Chapters.Add(ch);
                }
            }
        }
    }
}
