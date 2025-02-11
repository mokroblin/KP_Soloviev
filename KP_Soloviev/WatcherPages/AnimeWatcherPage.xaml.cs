using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DocumentFormat.OpenXml.ExtendedProperties;
using KP_Soloviev.add_windows;
using KP_Soloviev.edit_windows;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;
using KP_Soloviev.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace KP_Soloviev.WatcherPages
{
    public partial class AnimeWatcherPage : Page
    {
        private readonly Anime_CollectionContext _context;
        private ObservableCollection<Anime> _animes;
        public AnimeWatcherPage()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            LoadAnimeData();
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

        private void ReviewsButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Reviews());
        }

        private void LoadAnimeData()
        {
            var animes = _context.Animes.ToList();
            AnimeDataGrid.ItemsSource = animes;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            var filteredAnimes = _context.Animes.Where(a => a.Title.Contains(searchText)).ToList();
            AnimeDataGrid.ItemsSource = filteredAnimes;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAnimeData();
        }
    }
}
