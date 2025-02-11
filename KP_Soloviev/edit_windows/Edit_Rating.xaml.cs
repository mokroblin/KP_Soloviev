using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using KP_Soloviev.Models;

namespace KP_Soloviev.edit_windows
{
    public partial class Edit_Rating : Window
    {
        private readonly Anime_CollectionContext _context;
        private readonly int _ratingId;
        private readonly ObservableCollection<Rating> _ratings;

        public Edit_Rating(int ratingId, ObservableCollection<Rating> ratings)
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            _ratingId = ratingId;
            _ratings = ratings;
            LoadRatingData();
        }

        private void LoadRatingData()
        {
            try
            {
                // Извлечение рейтинга из базы данных с помощью его ID
                var rating = _context.Ratings.FirstOrDefault(p => p.RatingID == _ratingId);
                if (rating != null)
                {
                    RatingIDTextBox.Text = rating.RatingID.ToString();
                    RatingTypeTextBox.Text = rating.RatingType;
                    NameOfListTextBox.Text = rating.NameOfList;
                    CriteriaTextBox.Text = rating.Criteria;
                    UpdateDateTextBox.Text = rating.UpdateDate.ToString();
                }
                else
                {
                    MessageBox.Show("Рейтинг не найден.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о рейтинге: {ex.Message}");
                this.Close();
            }
        }

        private void EditRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rating = _context.Ratings.FirstOrDefault(p => p.RatingID == _ratingId);
                if (rating != null)
                {
                    rating.RatingType = RatingTypeTextBox.Text;
                    rating.NameOfList = NameOfListTextBox.Text;
                    rating.Criteria = CriteriaTextBox.Text;
                    rating.UpdateDate = DateOnly.FromDateTime(DateTime.Parse(UpdateDateTextBox.Text));

                    _context.SaveChanges();

                    var ratingInCollection = _ratings.FirstOrDefault(p => p.RatingID == _ratingId);
                    if (ratingInCollection != null)
                    {
                        ratingInCollection.RatingType = rating.RatingType;
                        ratingInCollection.NameOfList = rating.NameOfList;
                        ratingInCollection.Criteria = rating.Criteria;
                        ratingInCollection.UpdateDate = rating.UpdateDate;
                    }

                    MessageBox.Show("Данные рейтинга обновлены.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Рейтинг не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования данных рейтинга: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
