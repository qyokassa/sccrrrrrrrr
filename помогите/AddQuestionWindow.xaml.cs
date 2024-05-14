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
using static помогите_.Test;

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        public Questions Questions { get; set; } = new();
        public AddQuestionWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public AddQuestionWindow(Questions? questions)
        {
            InitializeComponent();
            DataContext = this;
            Questions = questions;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }


    }
}
