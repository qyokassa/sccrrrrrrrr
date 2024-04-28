using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для Createbook.xaml
    /// </summary>
    public partial class Createbook : Window
    {
        public Createbook()
        {
            InitializeComponent();
            textbooksListView.ItemsSource = books;
            DataContext = this;



        }
        public ObservableCollection<Textbook> books = new ObservableCollection<Textbook>();

        private TextBox GetYearTextBox()
        {
            return YearTextBox;
        }

       


        private void GoToLibrary_Click(object sender, RoutedEventArgs e)
        {
            LibraryWindow libraryWindow = new LibraryWindow();
            libraryWindow.Show();
            this.Close();

        }





        public ObservableCollection<Textbook> Books { get; set; } = new ObservableCollection<Textbook>();
       

        private void AddToLibrary_Click(object sender, RoutedEventArgs e)
        {
            if (textbooksListView.SelectedItem is Textbook selectedTexbook)
            {
                LibraryWindow LibraryWindow = new LibraryWindow();
                LibraryWindow.AddBookToList(selectedTexbook);
                LibraryWindow.Show();

            }
        }

        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            Textbook book = new Textbook
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                Year = int.Parse(YearTextBox.Text),
                Content = ContentTextBox.Text



            };
            books.Add(book);

           
            DataContext = this;


        }

        private void AddChapter_Click(object sender, RoutedEventArgs e)
        {


            ChapterAdd chapter = new ChapterAdd();
            chapter.Show();


        }
    }
}

    
