using System;
using System.Linq;
using System.Windows;
using KP_Soloviev.Models;

namespace KP_Soloviev.add_windows
{
    public partial class Add_Rating : Window
    {
        private readonly Anime_CollectionContext _context;

        public Add_Rating()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
        }

        private void AddRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ratingID = int.Parse(RatingIDTextBox.Text);
                string ratingType = TypeTextBox.Text;
                string nameOfList = NameOfListTextBox.Text; // Предполагается, что это строка
                string criteria = CriteriaTextBox.Text;
                DateTime updateDate = DateTime.Parse(UpdateDateTextBox.Text); // Используем DateTime для даты

                // Преобразуем DateTime в DateOnly
                DateOnly updateDateOnly = DateOnly.FromDateTime(updateDate);

                Rating newRating = new Rating
                {
                    RatingID = ratingID,
                    RatingType = ratingType,
                    NameOfList = nameOfList,
                    Criteria = criteria,
                    UpdateDate = updateDateOnly,
                };

                _context.Ratings.Add(newRating);
                _context.SaveChanges();

                MessageBox.Show("Рейтинг добавлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления рейтинга в базу данных: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
