using KP_Soloviev.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace KP_Soloviev.Pages
{
    public partial class RegistrationPage : Page
    {
        private readonly Anime_CollectionContext _context;
        private static readonly Random random = new Random();

        public RegistrationPage()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string login = txtNewLogin.Text;
            string email = txtNewEmail.Text;
            string password = txtNewPassword.Password;

            if (IsPasswordValid(password))
            {
                try
                {
                    if (_context.Users.Any(u => u.UserName == login))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Генерация случайного числа для нового UserID
                    int newUserId;
                    do
                    {
                        newUserId = random.Next(1, int.MaxValue);
                    } while (_context.Users.Any(u => u.UserID == newUserId)); // Проверка на уникальность

                    // Создание нового пользователя
                    var newUser = new User
                    {
                        UserID = newUserId,
                        UserName = login,
                        UserMail = email,
                        UserPass = password,
                        UserRole = "Watcher" // Роль пользователя поумолчанию
                    };
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно!");

                    // Переход к окну авторизации после успешной регистрации
                    NavigationService.Navigate(new AuthorizationPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при регистрации: {ex.Message}\nВнутреннее исключение: {ex.InnerException?.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пароль не соответствует требованиям безопасности.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (!IsPasswordValid(passwordBox.Password))
            {
                passwordBox.ToolTip = "Пароль должен содержать не менее 6 символов, включая как минимум одну прописную букву, одну цифру и один из символов: !@#$%^.";
                passwordBox.Background = new SolidColorBrush(Colors.LightCoral);
            }
            else
            {
                passwordBox.ToolTip = null;
                passwordBox.Background = new SolidColorBrush(Colors.White);
            }
        }

        private bool IsPasswordValid(string password)
        {
            return password.Length >= 6 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"\d") &&
                   Regex.IsMatch(password, @"[!@#$%^]");
        }
    }
}
