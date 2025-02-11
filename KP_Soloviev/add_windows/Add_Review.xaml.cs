using System;
using System.Linq;
using System.Windows;
using KP_Soloviev.Models;

namespace KP_Soloviev.add_windows
{
    public partial class Add_Review : Window
    {
        private readonly Anime_CollectionContext _context;
        private readonly int _currentUserId;

        public Add_Review()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            _currentUserId = _context.Users.FirstOrDefault(u => u.UserName == CurrentUser.UserName)?.UserID ?? 0;
        }

        private void AddReview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string animeTitle = AnimeTitleTextBox.Text;
                string title = TitleTextBox.Text;
                string text = TextTextBox.Text;

                var anime = _context.Animes.FirstOrDefault(a => a.Title == animeTitle);
                if (anime == null)
                {
                    MessageBox.Show("Аниме не найдено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Review newReview = new Review
                {
                    Title = title,
                    Text = text,
                    AnimeID = anime.AnimeID,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Author = CurrentUser.UserName,
                    UserID = _currentUserId
                };

                _context.Reviews.Add(newReview);
                _context.SaveChanges();

                MessageBox.Show("Отзыв добавлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления отзыва в базу данных: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
