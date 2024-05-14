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
    /// Логика взаимодействия для AddChapterWindow.xaml
    /// </summary>
    public partial class AddChapterWindow : Window
    {
        private BookData _bookData;

        public AddChapterWindow(BookData bookData)
        {
            _bookData = bookData;

            InitializeComponent();
        }

        private void AddChapter_Click(object sender, RoutedEventArgs e)
        {
            List<string> invalidFields = new List<string>();

            if (string.IsNullOrEmpty(txtName.Text))
                invalidFields.Add("\"Название\"");

            if (string.IsNullOrEmpty(txtContent.Text))
                invalidFields.Add("\"Содержание\"");

            if (invalidFields.Count > 0)
            {
                MessageBox.Show($"Заполните поля {string.Join(", ", invalidFields)}");
                return;
            }

            BookData.ChapterData chapterData = new BookData.ChapterData(txtName.Text, txtContent.Text);

            _bookData.Chapters.Add(chapterData);

            MessageBox.Show("Глава успешно добавлена");

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
