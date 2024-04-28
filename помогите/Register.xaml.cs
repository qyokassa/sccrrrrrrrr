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
            ActiveUser.Instance.Id = UserRepository.Instance.Register(User);
            if (ActiveUser.Instance.Id == 0)
                return;
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
    }
}
