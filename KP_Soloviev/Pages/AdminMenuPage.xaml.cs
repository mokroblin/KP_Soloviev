using KP_Soloviev.AdminPages;
using KP_Soloviev.Views;
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
    public partial class AdminMenuPage : Page
    {
        public AdminMenuPage()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserOptions.Visibility = UserOptions.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ChangeAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите сменить аккаунт?", "Подтверждение смены аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Смена аккаунта");
                NavigationService.Navigate(new AuthorizationPage());
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Выход из системы");
                NavigationService.Navigate(new ChoosePage());
            }
        }

        private void AnimeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AnimeAdminPage());
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin_Favourites());
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserAccounts());
        }

        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin_Rating());
        }

        private void ReviewsButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Admin_Reviews());
        }

    }

}
