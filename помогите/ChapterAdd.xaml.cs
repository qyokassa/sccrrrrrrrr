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

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для InfoBook.xaml
    /// </summary>
    public partial class ChapterAdd : Window
    {

       


        public ChapterAdd()
        {
            InitializeComponent();
        }

        public string? TitleChapter { get; set; }
        public string? DescriptionChapter { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
        Createbook createbook = new Createbook();

            



        }
    }
}
