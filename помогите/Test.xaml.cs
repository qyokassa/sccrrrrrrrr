using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using MySqlConnector;
using System.Data.SqlClient;
using System.IO;
using помогите_.Book;

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window

    {
        TestInfo testInfo;
        private object addQuestion;

        public string UserTitile { get => ActiveUser.Instance.User.Surname; }
        public string TestTitile { get; set; }
        

        public ObservableCollection<Questions> Questions { get; set; } = new();
        public Test()
        {
            InitializeComponent();
            testInfo = new TestInfo();
            DataContext = this;
        }
        public Test(TestInfo edit)
        {
            InitializeComponent();
            testInfo = edit;
            DataContext = this;
        }




        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (testInfo.ID == 0)
            {
                testInfo = TestRepository.Instance.AddTest(TestTitile, ActiveUser.Instance.User);
            }
            AddQuestionWindow addQuestion = new AddQuestionWindow();
            if (addQuestion.ShowDialog() == true)
            {
                TestRepository.Instance.AddQuestion(addQuestion.Questions, testInfo.ID);
                Questions.Add(addQuestion.Questions);
            }

        }

        private void EditQuestion(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            помогите_.Questions questions = button.Tag as Questions;
            AddQuestionWindow addQuestion = new AddQuestionWindow(questions);
            if (addQuestion.ShowDialog() == true)
            {
                TestRepository.Instance.UpdateQuestion(addQuestion.Questions);

            }




        }

        private void Delite_Question(object sender, RoutedEventArgs e)
        {
            if (testInfo.ID == 0)
            {
                testInfo = TestRepository.Instance.AddTest(TestTitile, ActiveUser.Instance.User);
            }
            AddQuestionWindow addQuestion = new AddQuestionWindow();
            if (addQuestion.ShowDialog() == true)
            {
                TestRepository.Instance.AddQuestion(addQuestion.Questions, testInfo.ID);
                Questions.Add(addQuestion.Questions);
            }


        }







        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {

            LibraryTest infoWindow = new LibraryTest();
            infoWindow.SetInfo(TestTitile);


            infoWindow.SetInfo1(question.Text);

            infoWindow.SetInfo2(UserTitile);
          

            infoWindow.Show();
           


            MessageBox.Show("Информация о тесте сохранена успешно!");

        }

        
    }
}


