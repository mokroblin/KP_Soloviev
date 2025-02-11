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
using KP_Soloviev.Views;

namespace KP_Soloviev.AdminPages
{
    public partial class Admin_Reviews : Page
    {
        private Anime_CollectionContext _context;

        public Admin_Reviews()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            LoadReviews();
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

        private void LoadReviews()
        {
            var reviews = _context.Reviews.ToList();
            ReviewsDataGrid.ItemsSource = reviews;
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedReview = ReviewsDataGrid.SelectedItem as Review;
            if (selectedReview != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот отзыв?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Reviews.Remove(selectedReview);
                    _context.SaveChanges();
                    LoadReviews(); // Обновить данные после удаления
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите отзыв для удаления.");
            }
        }

        private void RatingButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
