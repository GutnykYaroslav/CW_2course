using CW_2course.Models;
using CW_2course.Services;
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

namespace CW_2course
{

    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        
        private void RegisterConfirm_Click(object sender, RoutedEventArgs e)
        {
            string login = RegLoginTextBox.Text.Trim();
            string password = RegPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            if (password != confirmPassword)
            {
                MessageBox.Show("Паролі не збігаються!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            List<UserModel> users = FileService.LoadUsers();

           
            if (users.Any(u => u.Login.ToLower() == login.ToLower()))
            {
                MessageBox.Show("Користувач з таким логіном вже існує!", "Помилка реєстрації", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            UserModel newUser = new UserModel
            {
                Login = login,
                Password = password 
            };
            users.Add(newUser);

            
            FileService.SaveUsers(users);

            MessageBox.Show("Реєстрація успішна! Тепер ви можете увійти.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

            
            BackToLogin_Click(sender, e);
        }

        
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
