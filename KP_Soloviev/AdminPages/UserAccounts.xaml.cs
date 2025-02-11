using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KP_Soloviev.Models;
using KP_Soloviev.Pages;
using KP_Soloviev.Views;

namespace KP_Soloviev.AdminPages
{
    public partial class UserAccounts : Page
    {
        private readonly Anime_CollectionContext _context;

        public ObservableCollection<User> Users { get; set; }

        public UserAccounts()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            LoadUserAccounts();
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

        private void LoadUserAccounts()
        {
            Users = new ObservableCollection<User>(_context.Users.ToList());
            UserAccountsDataGrid.ItemsSource = Users;
        }

        private void RefreshUserAccounts()
        {
            var useraccountsList = _context.Users.ToList();
            Users.Clear();
            foreach (var user in useraccountsList)
            {
                Users.Add(user);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            RefreshUserAccounts();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredUserAccounts = _context.Users
                    .Where(c => c.UserName.ToLower().Contains(searchText))
                    .ToList();

                Users.Clear();
                foreach (var user in filteredUserAccounts)
                {
                    Users.Add(user);
                }
            }
            else
            {
                LoadUserAccounts();
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
