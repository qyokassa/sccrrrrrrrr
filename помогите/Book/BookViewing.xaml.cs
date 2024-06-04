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
    /// Логика взаимодействия для BookViewing.xaml
    /// </summary>
    public partial class BookViewing : Window
    {
        private BookData _bookData;

        public BookViewing(BookData bookData)
        {
            _bookData = bookData;

            InitializeComponent();

            lvChapters.ItemsSource = _bookData.Chapters;

            txtBookTitle.Content = bookData.Title;
            txtAuthor.Content = bookData.Author;
            txtYear.Content = bookData.Year;
            txtAnnotation.Content = bookData.Annotation;
            labelChapterCount.Content = bookData.Chapters.Count;
        }
    }
}
