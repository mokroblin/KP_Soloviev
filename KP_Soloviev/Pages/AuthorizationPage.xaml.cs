using KP_Soloviev.AdminPages;
using KP_Soloviev.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP_Soloviev.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private readonly Anime_CollectionContext _context;

        public AuthorizationPage()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Password;

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == login && u.UserPass == password);
                if (user != null)
                {
                    MessageBox.Show("Добро пожаловать.");
                    CurrentUser.UserName = login; // Сохранение имени пользователя
                    if (login == "mokroblin")
                    {
                        NavigationService.Navigate(new UserAccounts());
                    }
                    else
                    {
                        NavigationService.Navigate(new WatcherMenuPage());
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
