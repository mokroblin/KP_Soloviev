using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KP_Soloviev.add_windows;
using KP_Soloviev.AdminPages;
using KP_Soloviev.edit_windows;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;

namespace KP_Soloviev.Views
{
    public partial class Admin_Rating : Page
    {
        private Anime_CollectionContext _context;

        public Admin_Rating()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            LoadRatings();
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

        private void LoadRatings()
        {
            var ratings = _context.Ratings.ToList();
            RatingDataGrid.ItemsSource = ratings;
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var addRatingWindow = new Add_Rating();
            addRatingWindow.ShowDialog();
            LoadRatings(); // Обновить данные после добавления
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedRating = RatingDataGrid.SelectedItem as Rating;
            if (selectedRating != null)
            {
                var editRatingWindow = new Edit_Rating(selectedRating.RatingID, new ObservableCollection<Rating>(_context.Ratings.ToList()));
                editRatingWindow.ShowDialog();
                LoadRatings();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите рейтинг для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedRating = RatingDataGrid.SelectedItem as Rating;
            if (selectedRating != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот рейтинг?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Ratings.Remove(selectedRating);
                    _context.SaveChanges();
                    LoadRatings();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите рейтинг для удаления.");
            }
        }

        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin_Rating());
        }

        private void AnimeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AnimeAdminPage());
        }

        private void FavoritesButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Admin_Favourites());
        }

        private void UsersButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new UserAccounts());
        }

        private void ReviewsButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Admin_Reviews());
        }
    }
}
