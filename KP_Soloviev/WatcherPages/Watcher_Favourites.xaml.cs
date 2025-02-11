using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;

namespace KP_Soloviev.WatcherPages
{
    public partial class Watcher_Favourites : Page
    {
        private readonly string _userName;

        public Watcher_Favourites()
        {
            InitializeComponent();
            _userName = CurrentUser.UserName;
            LoadFavourites(_userName);
            LoadAnimeNames();
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

        private void LoadFavourites(string userName)
        {
            using (var context = new Anime_CollectionContext())
            {
                var favourites = context.Favourites
                    .Where(f => f.User.UserName == userName)
                    .Select(f => new
                    {
                        Title = f.Anime_Favourites.Any() ? f.Anime_Favourites.First().Anime.Title : null,
                        Photo = f.Anime_Favourites.Any() ? f.Anime_Favourites.First().Anime.Photo : null,
                        UserName = f.User.UserName,
                        UserRole = f.User.UserRole
                    })
                    .ToList();

                // Удаление пустых строк
                favourites = favourites.Where(f => f.Title != null).ToList();

                FavouritesDataGrid.ItemsSource = favourites;
            }
        }


        private void LoadAnimeNames()
        {
            using (var context = new Anime_CollectionContext())
            {
                var animeNames = context.Animes.Select(a => a.Title).ToList();
                cmbAnimeName.ItemsSource = animeNames;
            }
        }

        private void AddAnime_Click(object sender, RoutedEventArgs e)
        {
            string animeName = cmbAnimeName.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(animeName))
            {
                MessageBox.Show("Пожалуйста, выберите аниме.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Anime_CollectionContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == _userName);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var anime = context.Animes.FirstOrDefault(a => a.Title == animeName);
                if (anime == null)
                {
                    MessageBox.Show("Данного аниме не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var favourite = context.Favourites.FirstOrDefault(f => f.UserID == user.UserID && f.Anime_Favourites.Any(af => af.AnimeID == anime.AnimeID));
                if (favourite != null)
                {
                    MessageBox.Show("Это аниме уже добавлено для пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newFavourite = new Favourite
                {
                    NameOfList = "Избранное",
                    CreationDate = DateOnly.FromDateTime(DateTime.Now), // Преобразование DateTime в DateOnly
                    UserID = user.UserID
                };

                context.Favourites.Add(newFavourite);
                context.SaveChanges();

                var animeFavourite = new Anime_Favourite
                {
                    AnimeID = anime.AnimeID,
                    FavouriteID = newFavourite.FavouriteID
                };

                context.Anime_Favourites.Add(animeFavourite);
                context.SaveChanges();
            }

            LoadFavourites(_userName);

            cmbAnimeName.SelectedItem = null;
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
