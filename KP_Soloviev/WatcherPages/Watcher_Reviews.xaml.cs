using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KP_Soloviev.add_windows;
using KP_Soloviev.WatcherPages;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;
using KP_Soloviev.Views;

namespace KP_Soloviev.WatcherPages
{
    public partial class Watcher_Reviews : Page
    {
        private Anime_CollectionContext _context;

        public Watcher_Reviews()
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
            var reviews = _context.Reviews
                .Select(r => new
                {
                    r.ReviewID,
                    r.Title,
                    r.Text,
                    r.AnimeID,
                    AnimeTitle = _context.Animes.FirstOrDefault(a => a.AnimeID == r.AnimeID) != null ? _context.Animes.FirstOrDefault(a => a.AnimeID == r.AnimeID).Title : "Неизвестно",
                    r.Date,
                    r.Author,
                    r.UserID
                })
                .ToList();

            ReviewsDataGrid.ItemsSource = reviews;
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Add_Review addReviewWindow = new Add_Review();
            addReviewWindow.ShowDialog();
            LoadReviews();
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedReview = ReviewsDataGrid.SelectedItem as dynamic;
            if (selectedReview != null)
            {
                if (selectedReview.UserID == _context.Users.FirstOrDefault(u => u.UserName == CurrentUser.UserName)?.UserID)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот отзыв?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var reviewToDelete = _context.Reviews.Find(selectedReview.ReviewID);
                        if (reviewToDelete != null)
                        {
                            _context.Reviews.Remove(reviewToDelete);
                            _context.SaveChanges();
                            LoadReviews();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы можете удалять только свои отзывы.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите отзыв для удаления.");
            }
        }

        private void RatingButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Rating());
        }

        private void AnimeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AnimeWatcherPage());
        }

        private void FavoritesButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Favourites());
        }

        private void ReviewsButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Watcher_Reviews());
        }
    }
}
