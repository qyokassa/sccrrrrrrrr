using System;
using Microsoft.Data.Sqlite;
using MySqlConnector;


namespace помогите_.Book
{
    public class LibraryDataBase

    {


        static  LibraryDataBase instance;
        public static LibraryDataBase Instance
        {
            get
            {
                if (instance == null)
                    instance = new LibraryDataBase();
                return instance;
            }
        }



        private MySqlConnection? connection;

        public LibraryDataBase() { }

        public MySqlConnection Connection { get; set; }

        public Library Library { get; set; }

        public LibraryDataBase(Connection MySqlDBconnection, Library library)
        {
            Connection = connection;
            Library = library;

            try
            {
                Connection.Open();

                string MySqlDB = Connection.Database;

                SQLiteCommand command;

                //// Создаем базу данных, если её нет
                //SQLiteCommand command = new SQLiteCommand($"CREATE DATABASE IF NOT EXISTS {databaseName};", Connection);
                //command.ExecuteNonQuery();

                //// Переключаемся на созданную базу данных
                Connection.ChangeDatabase(MySqlDB);

                // Создаем таблицу для книг, если её нет
                command = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS books (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    author TEXT NOT NULL,
                    release_year INTEGER UNSIGNED,
                    annotation TEXT
                );
                ", Connection);
                command.ExecuteNonQuery();

                // Создаем таблицу для глав книг, если её нет
                command = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS chapters (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    book_id INTEGER,
                    title TEXT NOT NULL,
                    content TEXT,
                    FOREIGN KEY (book_id) REFERENCES books(id)
                );
                ", Connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                Connection?.Close();
            }
        }

        public void ReadAll()
        {
            try
            {
                Connection.Open();

                SQLiteCommand command = new SQLiteCommand(
                @"SELECT books.*, chapters.title AS chapter_title, chapters.content AS chapter_content
                FROM books
                LEFT JOIN chapters ON books.id = chapters.book_id;
                ", Connection);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int bookIndex = (int)(long)reader["id"] - 1;

                    BookData bookData;
                    if (bookIndex >= Library.Books.Count)
                    {
                        bookData = new BookData();
                        bookData.Title = (string)reader["title"];
                        bookData.Author = (string)reader["author"];
                        bookData.Year = (int)(long)reader["release_year"];
                        bookData.Annotation = (string)reader["annotation"];

                        Library.Books.Add(bookData);
                    }

                    
                }

                reader.Close();
            }
            finally
            {
                Connection?.Close();
            }
        }

        public void WriteAll()
        {
            try
            {
                Connection.Open();

                for (int bookIndex = 0; bookIndex < Library.Books.Count; bookIndex++)
                {
                    BookData bookData = Library.Books[bookIndex];

                    SQLiteCommand command = new SQLiteCommand("DELETE FROM books; DELETE FROM chapters;", Connection);
                    command.ExecuteNonQuery();

                    command = new SQLiteCommand(
                    @"INSERT INTO books (title, author, release_year, annotation) 
                    VALUES (@title, @author, @release_year, @annotation);
                    ", Connection);

                    

                    command.ExecuteNonQuery();

                    for (int chapterIndex = 0; chapterIndex < bookData.Chapters.Count; chapterIndex++)
                    {
                        BookData.ChapterData chapterData = bookData.Chapters[chapterIndex];

                        command = new SQLiteCommand(
                        @"INSERT INTO chapters (book_id, title, content)
                        VALUES (@book_id, @title, @content);
                        ", Connection);

                       

                        command.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                Connection?.Close();
            }
        }
    }

    public class Connection
    {
    }
}
