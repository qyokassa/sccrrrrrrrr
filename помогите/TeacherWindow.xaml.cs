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
using помогите_.Book;

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
        }

        private void ViewTestResultsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextbookLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            LibraryWindow libraryWindow = new LibraryWindow();
            libraryWindow.Show();
            this.Close();

        }

        private void CreateTestButton_Click(object sender, RoutedEventArgs e)
        {
            Test test = new Test();
            test.Show();
            this.Close();
        }

        private void CreateTextbookButton_Click(object sender, RoutedEventArgs e)
        {

          LibraryWindow libraryWindow = new LibraryWindow();
            libraryWindow.Show();
               

        }
    }
}
