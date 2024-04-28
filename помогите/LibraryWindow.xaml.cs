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
    /// Логика взаимодействия для LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window

    {
        public void AddBook(Textbook newBook)
        {
            libraryListView.Items.Add(newBook);
        }

        public LibraryWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        public ObservableCollection<Textbook> books = new ObservableCollection<Textbook>();

        public void AddBookToList(Textbook book)
        {
            libraryListView.Items.Add(book);
        }

    }
}
