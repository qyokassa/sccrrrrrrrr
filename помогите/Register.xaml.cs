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

namespace помогите_
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Users User { get; set; } = new Users { Name = "Имя", Surname = "Фамилия", Password = "Пароль", Login="Логин" };
        public Register()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(User.Password) ||
                string.IsNullOrEmpty(User.Name) ||
                string.IsNullOrEmpty(User.Surname))
            {
                MessageBox.Show("Введите все данные");
                return;
            }
            if (UserRepository.Instance.Register(User) == 0)
                return;

                    ActiveUser.Instance.User = User;


            if (User.Teacher)
            {
                TeacherWindow teacherWindow = new TeacherWindow();
                teacherWindow.Show();
            }
            else
            {
                StudentWindow studentWindow = new StudentWindow();
                studentWindow.Show();
            }
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
