using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using KP_Soloviev.add_windows;
using KP_Soloviev.edit_windows;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;
using KP_Soloviev.Views;

namespace KP_Soloviev.AdminPages
{
    public partial class AnimeAdminPage : Page
    {
        private readonly Anime_CollectionContext _context;
        private ObservableCollection<Anime> _animes;

        public AnimeAdminPage()
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

        private void LoadAnimeData()
        {
            var animes = _context.Animes.ToList();
            AnimeDataGrid.ItemsSource = animes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Add_Anime addAnimeWindow = new Add_Anime();
            addAnimeWindow.ShowDialog();
            LoadAnimeData(); // Обновление списка после добавления
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите аниме для редактирования.");
                return;
            }

            Anime selectedAnime = (Anime)AnimeDataGrid.SelectedItem;
            Edit_Anime editAnimeWindow = new Edit_Anime(selectedAnime.AnimeID, _animes);
            editAnimeWindow.Closed += (s, args) => LoadAnimeData();
            editAnimeWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите аниме для удаления.");
                return;
            }

            Anime selectedAnime = (Anime)AnimeDataGrid.SelectedItem;
            try
            {
                // Удаление всех зависимостей
                var animeFavourites = _context.Anime_Favourites.Where(af => af.AnimeID == selectedAnime.AnimeID).ToList();
                _context.Anime_Favourites.RemoveRange(animeFavourites);

                var animeRatings = _context.Anime_Ratings.Where(ar => ar.AnimeID == selectedAnime.AnimeID).ToList();
                _context.Anime_Ratings.RemoveRange(animeRatings);

                var reviews = _context.Reviews.Where(r => r.AnimeID == selectedAnime.AnimeID).ToList();
                _context.Reviews.RemoveRange(reviews);

                // Удаление аниме
                _context.Animes.Remove(selectedAnime);
                _context.SaveChanges();
                MessageBox.Show("Аниме удалено.");
                LoadAnimeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления аниме: {ex.Message}");
            }
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
