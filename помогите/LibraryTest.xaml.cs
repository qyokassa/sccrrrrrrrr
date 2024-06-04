using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Documents;

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для LibraryTest.xaml
    /// </summary>
    public partial class LibraryTest : Window
    {
        public LibraryTest()
        {
            
            InitializeComponent();

            


        }

        public void SetInfo(string info)
        {
            txtTestTitle.Content = info;
            
    
        
        }

        public void SetInfo2(string info)
        {
           author.Content = info;



        }


        public void SetInfo1(string info)
        {
            
            txtNunQ.Content = info;
        }

        private void lvQ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
           
        }

       
    }

}
