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
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private Library _library;

        private LibraryDataBase _libraryDatabase;
        private BookData _bookToAdd;

        private BookData.ChapterData? _selectedChapter;

        public AddBookWindow(Library library, LibraryDataBase libraryDatabase)
        {
            _library = library;
            _libraryDatabase = libraryDatabase;
            _bookToAdd = new BookData();

            InitializeComponent();

            btnDeleteChapter.IsEnabled = false;
            lvChapters.ItemsSource = _bookToAdd.Chapters;
            labelChapterCount.Content = _bookToAdd.Chapters.Count.ToString();

            // Подписываемся на событие, которое будет обновлять поле, отображающее количество глав, если количество глав будет изменяться.
            _bookToAdd.Chapters.CollectionChanged += (sender, e) =>
            {
                labelChapterCount.Content = _bookToAdd.Chapters.Count;
            };
        }

        private void AddChapter_Click(object sender, RoutedEventArgs e)
        {
            AddChapterWindow addChapterWindow = new AddChapterWindow(_bookToAdd);
            addChapterWindow.Show();
        }

        private void DeleteChapter_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedChapter == null)
            {
                MessageBox.Show("Глава не выбрана!");
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить главу {_selectedChapter.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                _bookToAdd.Chapters.Remove(_selectedChapter);
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            
            {
                // Создаем список невалидных полей
                List<string> invalidFields = new List<string>();

                // Проверяем заполнение поля "Название"
                if (string.IsNullOrEmpty(txtBookTitle.Text))
                    invalidFields.Add("\"Название\"");

                // Проверяем заполнение поля "Автор"
                if (string.IsNullOrEmpty(txtAuthor.Text))
                    invalidFields.Add("\"Автор\"");

                // Проверяем заполнение поля "Год выпуска"
                if (string.IsNullOrEmpty(txtYear.Text))
                    invalidFields.Add("\"Год выпуска\"");

                // Проверяем заполнение поля "Аннотация"
                if (string.IsNullOrEmpty(txtAnnotation.Text))
                    invalidFields.Add("\"Аннотация\"");

                // Если есть невалидные поля, выводим сообщение и завершаем метод
                if (invalidFields.Count > 0)
                {
                    MessageBox.Show($"Заполните поля {string.Join(", ", invalidFields)}");
                    return;
                }

                // Пытаемся преобразовать год выпуска в число
                if (!int.TryParse(txtYear.Text, out int year))
                {
                    MessageBox.Show($"Вы ввели неверные значения в поле \"Год выпуска\"");
                    return;
                }

                // Заполняем информацию о книге
                _bookToAdd.Title = txtBookTitle.Text;
                _bookToAdd.Author = txtAuthor.Text;
                _bookToAdd.Year = year;
                _bookToAdd.Annotation = txtAnnotation.Text;

                // Добавляем книгу в библиотеку
                _library.Books.Add(_bookToAdd);

                // Выводим сообщение об успешном добавлении книги
                MessageBox.Show("Книга успешно добавлена");

                // Закрываем окно
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvChapters_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvChapters.SelectedItem == null)
            {
                btnDeleteChapter.IsEnabled = false;
                return;
            }

            _selectedChapter = (BookData.ChapterData)lvChapters.SelectedItem;
            btnDeleteChapter.IsEnabled = true;
        }
    }
}
