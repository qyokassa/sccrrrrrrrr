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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Users User { get; set; } = new Users { Password = "Пароль", Login = "Логин" };
        public Login()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.Password = passwordBox.Password;
            User = UserRepository.Instance.Login(User);
            if (User.ID == 0)
            {
                MessageBox.Show("не найден");
                return;
            }
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
        }
    }
}
