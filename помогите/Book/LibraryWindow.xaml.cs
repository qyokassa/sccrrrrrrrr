using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace помогите_.Book
{
    /// <summary>
    /// Логика взаимодействия для LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window

    {
        private Library _library;

        private BookData? _selectedBook;

        private LibraryDataBase _libraryDatabase;

        public LibraryWindow()
        {
            InitializeComponent();

            btnAddBook.IsEnabled = ActiveUser.Instance.User.Teacher;
            btnDeleteBook.IsEnabled = ActiveUser.Instance.User.Teacher;
            btnSave.IsEnabled = ActiveUser.Instance.User.Teacher;

            _library = new Library();

            lvBooks.ItemsSource = _library.Books;





        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {

            AddBookWindow addBookWindow = new AddBookWindow(_library, _libraryDatabase);
            addBookWindow.Show();


        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedBook == null)
                return;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить книгу {_selectedBook.Title}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                _library.Books.Remove(_selectedBook);


        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
        }

        private void lvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (lvBooks.SelectedItem == null)
            {
                btnDeleteBook.IsEnabled = false;
                btnWatchBook.IsEnabled = false;
                return;
            }

            _selectedBook = (BookData)lvBooks.SelectedItem;
        
            btnWatchBook.IsEnabled = true;

            if (ActiveUser.Instance.User.Teacher)
                btnDeleteBook.IsEnabled = true;
        }



        private void Назад_Click1(object sender, RoutedEventArgs e)
        {
            if (ActiveUser.Instance.User.Teacher)
                new TeacherWindow().Show();
            Close();
        }

        private void WatchBook_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook == null)
                return;

            BookRepository.Instance.FillChapters(_selectedBook);

            BookViewing bookViewing = new BookViewing(_selectedBook);
            bookViewing.Show();
        }
    }
}

