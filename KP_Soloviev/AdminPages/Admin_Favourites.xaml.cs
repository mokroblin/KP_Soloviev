using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;
using KP_Soloviev.Views;

namespace KP_Soloviev.AdminPages
{
    public partial class Admin_Favourites : Page
    {
        public Admin_Favourites()
        {
            InitializeComponent();
            LoadFavourites();
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

        private void LoadFavourites()
        {
            using (var context = new Anime_CollectionContext())
            {
                var favourites = context.Anime_Favourites
                    .Where(af => af.Favourite.User.UserName == CurrentUser.UserName)
                    .Select(af => af.Anime)
                    .ToList();

                FavouritesDataGrid.ItemsSource = favourites;
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

        private void AddToFavourites_Click(object sender, RoutedEventArgs e)
        {
            string animeTitle = txtAnimeTitle.Text;

            if (string.IsNullOrEmpty(animeTitle))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Anime_CollectionContext())
            {
                var anime = context.Animes.FirstOrDefault(a => a.Title == animeTitle);
                if (anime == null)
                {
                    MessageBox.Show("Аниме не найдено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = context.Users.FirstOrDefault(u => u.UserName == CurrentUser.UserName);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var favourite = context.Favourites.FirstOrDefault(f => f.UserID == user.UserID);
                if (favourite == null)
                {
                    favourite = new Favourite
                    {
                        NameOfList = "Избранное",
                        CreationDate = DateOnly.FromDateTime(DateTime.Now),
                        UserID = user.UserID
                    };
                    context.Favourites.Add(favourite);
                    context.SaveChanges();
                }

                // Проверка, существует ли уже запись в избранном
                var existingFavourite = context.Anime_Favourites
                    .FirstOrDefault(af => af.AnimeID == anime.AnimeID && af.FavouriteID == favourite.FavouriteID);
                if (existingFavourite != null)
                {
                    MessageBox.Show("Данное аниме уже есть в вашем списке избранного.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var animeFavourite = new Anime_Favourite
                {
                    AnimeID = anime.AnimeID,
                    FavouriteID = favourite.FavouriteID
                };

                context.Anime_Favourites.Add(animeFavourite);
                context.SaveChanges();
            }

            // Обновить DataGrid
            LoadFavourites();

            // Очистить поля ввода
            txtAnimeTitle.Text = "";
        }
    }
}
