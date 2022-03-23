using KPNoYandexV.Data;
using KPNoYandexV.Model;
using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KPNoYandexV.ViewModel
{
    class FilmWindowVM : BaseVM
    {
        private Film film = new Film();
        private string yearAndCounry;
        private string ratingAndRevNumber;
        private string path;
        private List<Actor> actors = new List<Actor>();

        public Film CurrentFilm { get { return film; } set { film = value; OnPropertyChanged(); } }
        public string YearAndCounry { get { return yearAndCounry; } set { yearAndCounry = value; OnPropertyChanged(); } }
        public string RatingAndRevNumber { get { return ratingAndRevNumber; } set { ratingAndRevNumber = value; OnPropertyChanged(); } }
        public string Path { get { return path; } set { path = value; OnPropertyChanged(); } }
        public List<Actor> CurrentActors { get { return actors; } set { actors = value; OnPropertyChanged(); } }

        public FilmWindowVM(int Id)
        {
            using (KPNoYandexVContext context = new KPNoYandexVContext())
            {
                CurrentFilm = context.Films.SingleOrDefault(film => film.Id == Id);
                List<FilmsActor> FilmActors = context.FilmsActors.Where(filmact => filmact.FilmId == CurrentFilm.Id).ToList();
                foreach (var FilmActor in FilmActors)
                {
                    Actor Act = context.Actors.SingleOrDefault(act => act.Id == FilmActor.ActorId);
                    CurrentActors.Add(Act);
                }

            }
            DateTime Date = Convert.ToDateTime(CurrentFilm.Year.ToString());
            YearAndCounry = Date.Year.ToString() + ", " + CurrentFilm.Country;
            RatingAndRevNumber = CurrentFilm.Rating + "; отзывов:" + CurrentFilm.ReviewsNumber;
            Path = $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Posters\\{CurrentFilm.PosterPath}";
        }

        public BaseCommand ActorOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);
                    var ActorPage = new ActorWindow(Id);
                    ActorPage.Show();
                });
            }
        }
    }
}
