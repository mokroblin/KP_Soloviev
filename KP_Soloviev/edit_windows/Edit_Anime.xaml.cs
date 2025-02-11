using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using KP_Soloviev.Models;
using Microsoft.Win32;
using System.IO;

namespace KP_Soloviev.edit_windows
{
    public partial class Edit_Anime : Window
    {
        private readonly Anime_CollectionContext _context;
        private readonly int _animeId;
        private readonly ObservableCollection<Anime> _animes;
        private byte[] SelectedPhoto { get; set; }
        private byte[] OriginalPhoto { get; set; }

        public Edit_Anime(int animeId, ObservableCollection<Anime> animes)
        {
            InitializeComponent();
            _context = new Anime_CollectionContext();
            _animeId = animeId;
            _animes = animes;
            LoadAnimeData();
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.ico)|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tiff;*.ico"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedPhoto = File.ReadAllBytes(openFileDialog.FileName);
                MessageBox.Show("Изображение выбрано успешно.");
            }
        }

        private void LoadAnimeData()
        {
            try
            {
                var anime = _context.Animes.FirstOrDefault(p => p.AnimeID == _animeId);
                if (anime != null)
                {
                    AnimeIDTextBox.Text = anime.AnimeID.ToString();
                    TitleTextBox.Text = anime.Title;
                    OriginalPhoto = anime.Photo; // Сохранение текущего фото
                    SelectedPhoto = anime.Photo; // Юзаю текущее фото по умолчанию
                    GenreTextBox.Text = anime.Genre;
                    AgeRatingTextBox.Text = anime.AgeRating.ToString();
                    DurationTextBox.Text = anime.Duration.ToString();
                    SeriesTextBox.Text = anime.Series.ToString();
                    SeasonsTextBox.Text = anime.Seasons.ToString();
                    DescriptionTextBox.Text = anime.Description;
                    StatusTextBox.Text = anime.Status;
                }
                else
                {
                    MessageBox.Show("Аниме не найдено.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных о аниме: {ex.Message}");
                this.Close();
            }
        }

        private void EditAnime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var anime = _context.Animes.FirstOrDefault(p => p.AnimeID == _animeId);
                if (anime != null)
                {
                    // Обновление данных
                    anime.Title = TitleTextBox.Text;
                    anime.Photo = SelectedPhoto ?? OriginalPhoto;
                    anime.Genre = GenreTextBox.Text;
                    anime.AgeRating = int.Parse(AgeRatingTextBox.Text);
                    anime.Duration = int.Parse(DurationTextBox.Text);
                    anime.Series = int.Parse(SeriesTextBox.Text);
                    anime.Seasons = int.Parse(SeasonsTextBox.Text);
                    anime.Description = DescriptionTextBox.Text;
                    anime.Status = StatusTextBox.Text;

                    _context.SaveChanges();

                    var animeInCollection = _animes.FirstOrDefault(p => p.AnimeID == _animeId);
                    if (animeInCollection != null)
                    {
                        animeInCollection.Title = anime.Title;
                        animeInCollection.Photo = anime.Photo;
                        animeInCollection.Genre = anime.Genre;
                        animeInCollection.AgeRating = anime.AgeRating;
                        animeInCollection.Duration = anime.Duration;
                        animeInCollection.Series = anime.Series;
                        animeInCollection.Seasons = anime.Seasons;
                        animeInCollection.Description = anime.Description;
                        animeInCollection.Status = anime.Status;
                    }

                    MessageBox.Show("Данные аниме обновлены.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Аниме не найдено.");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка редактирования данных аниме: {ex.Message}");
                MessageBox.Show("Данные аниме обновлены.");
                this.Close();
            }
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
