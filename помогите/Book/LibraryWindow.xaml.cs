using System;
using System.Collections.Generic;
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

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
