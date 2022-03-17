using KPNoYandexV.Data;
using KPNoYandexV.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KPNoYandexV.ViewModel
{
    class FilmWindowVM : INotifyPropertyChanged
    {
        private Film film;
        private string yearAndCounry;
        private string ratingAndRevNumber;
        private string path;

        public Film CurrentFilm { get { return film; } set { film = value; OnPropertyChanged(); } }
        public string YearAndCounry { get { return yearAndCounry; } set { yearAndCounry = value; OnPropertyChanged(); } }
        public string RatingAndRevNumber { get { return ratingAndRevNumber; } set { ratingAndRevNumber = value; OnPropertyChanged(); } }
        public string Path { get { return path; } set { path = value; OnPropertyChanged(); } }

        public FilmWindowVM(int Id)
        {
            using (KPNoYandexVContext context = new KPNoYandexVContext())
            {
                CurrentFilm = context.Films.SingleOrDefault(film => film.Id == Id);
            }
            DateTime Date = Convert.ToDateTime(CurrentFilm.Year.ToString());
            YearAndCounry = Date.Year.ToString() + ", " + CurrentFilm.Country;
            RatingAndRevNumber = CurrentFilm.Rating + "; отзывов:" + CurrentFilm.ReviewsNumber;
            Path = $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Posters\\{CurrentFilm.PosterPath}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
