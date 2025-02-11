using System;
using System.Linq;
using System.Windows;
using KP_Soloviev.Models;
using Microsoft.Win32;
using System.IO;

namespace KP_Soloviev.add_windows
{
    public partial class Add_Anime : Window
    {
        private readonly Anime_CollectionContext _context;
        private byte[] SelectedPhoto { get; set; }

        public Add_Anime()
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.ico)|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.ico"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] photo = File.ReadAllBytes(openFileDialog.FileName);
                SelectedPhoto = photo;
            }
        }

        private void AddAnime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int animeID = int.Parse(AnimeIDTextBox.Text);
                string title = TitleTextBox.Text;
                byte[] photo = SelectedPhoto; // Используйте выбранное фото
                string genre = GenreTextBox.Text;
                int ageRating = int.Parse(AgeRatingTextBox.Text);
                int duration = int.Parse(DurationTextBox.Text);
                int series = int.Parse(SeriesTextBox.Text);
                int seasons = int.Parse(SeasonsTextBox.Text);
                string description = DescriptionTextBox.Text;
                string status = StatusTextBox.Text;

                Anime newAnime = new Anime
                {
                    AnimeID = animeID,
                    Title = title,
                    Photo = photo,
                    Genre = genre,
                    AgeRating = ageRating,
                    Duration = duration,
                    Series = series,
                    Seasons = seasons,
                    Description = description,
                    Status = status
                };

                _context.Animes.Add(newAnime);
                _context.SaveChanges();

                MessageBox.Show("Аниме добавлено.");
                this.Close();
            }
            catch (Exception ex)
            {
                // Получение внутреннего исключения
                var innerException = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Ошибка добавления аниме в базу данных: {innerException}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
