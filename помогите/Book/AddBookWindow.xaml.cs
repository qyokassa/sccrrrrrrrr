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
using Microsoft.Win32;

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

            txtAuthor.Text = ActiveUser.Instance.User.Name;

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
           

            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show($"Вы ввели неверные значения в поле \"Год выпуска\"");
                return;
            }

            _bookToAdd.Title = txtBookTitle.Text;
            _bookToAdd.AuthorID = ActiveUser.Instance.User.ID;
            _bookToAdd.Year = year;
            _bookToAdd.Annotation = txtAnnotation.Text;

           
            
           _library.Books.Add(_bookToAdd);


            MessageBox.Show("Книга успешно добавлена");

            Close();
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



        private void Назадд_Click(object sender, RoutedEventArgs e)
        {
            new TeacherWindow().Show();
            Close();
        }

        
    }
}
