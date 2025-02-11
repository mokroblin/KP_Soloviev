using KP_Soloviev.WatcherPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace KP_Soloviev.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WatcherMenuPage : Page
    {
        public WatcherMenuPage()
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
            NavigationService.Navigate(new AnimeWatcherPage());
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Favourites());
        }

        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Rating());
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Reviews());
        }
    }
}
