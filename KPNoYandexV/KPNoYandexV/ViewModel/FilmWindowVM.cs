using KPNoYandexV.Data;
using KPNoYandexV.Lib;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPNoYandexV.ViewModel
{
    class FilmWindowVM : BaseVM
    {
        private Film film = new Film();
        private string yearAndCounry;
        private string ratingAndRevNumber;
        private string path;
        private List<Button> genreNames;
        private List<Actor> actors = new List<Actor>();

        private FilmWindow currentWindow;
        private FilmsPageVM parentWindowVM;

        public Film CurrentFilm { get { return film; } set { film = value; OnPropertyChanged(); } }
        public string YearAndCounry { get { return yearAndCounry; } set { yearAndCounry = value; OnPropertyChanged(); } }
        public string RatingAndRevNumber { get { return ratingAndRevNumber; } set { ratingAndRevNumber = value; OnPropertyChanged(); } }
        public string Path { get { return path; } set { path = value; OnPropertyChanged(); } }
        public List<Button> GenreNames { get { return genreNames; } set { genreNames = value; OnPropertyChanged(); } }
        public List<Actor> CurrentActors { get { return actors; } set { actors = value; OnPropertyChanged(); } }

        public FilmWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        public FilmsPageVM ParentWindowVM { get { return parentWindowVM; } set { parentWindowVM = value; OnPropertyChanged(); } }

        
        public FilmWindowVM(int Id, FilmWindow window, FilmsPageVM ?parWindow)
        {
            CurrentWindow = window;
            ParentWindowVM = parWindow;
            GenreNames = new List<Button>();
            using (KPNoYandexVContext context = new KPNoYandexVContext())
            {
                CurrentFilm = context.Films.SingleOrDefault(film => film.Id == Id);
                List<FilmsActor> FilmActors = context.FilmsActors.Where(filmact => filmact.FilmId == CurrentFilm.Id).ToList();
                foreach (var FilmActor in FilmActors)
                {
                    Actor Act = context.Actors.SingleOrDefault(act => act.Id == FilmActor.ActorId);
                    CurrentActors.Add(Act);
                }

                List<FilmsGenre> FilmGenres = context.FilmsGenres.Where(filmact => filmact.FilmId == CurrentFilm.Id).ToList();
                foreach (var FilmGenre in FilmGenres)
                {
                    Genre Gen = context.Genres.SingleOrDefault(gen => gen.Id == FilmGenre.GenreId);
                    var Btn = new Button();
                    Btn.Width = 70;
                    Btn.Height = 25;
                    Btn.Content = Gen.Name;
                    Btn.Margin = new Thickness(0, 0, 10, 0);
                    Btn.Cursor = Cursors.Hand;
                    Btn.Command = GenreClick;
                    Btn.CommandParameter = Gen.Id;
                    
                    GenreNames.Add(Btn);
                }

                CurrentWindow.RenderGenres(GenreNames);

            }
            DateTime Date = Convert.ToDateTime(CurrentFilm.Year.ToString());
            YearAndCounry = Date.Year.ToString() + ", " + CurrentFilm.Country;
            RatingAndRevNumber = CurrentFilm.Rating + "; отзывов:" + CurrentFilm.ReviewsNumber;
            Path = $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Posters\\{CurrentFilm.PosterPath}";
        }




        public BaseCommand GenreClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<FilmWindow>(CurrentWindow);
                    ParentWindowVM.ApplyGenreFilter.Execute(obj);

                });
            }
        }

        public BaseCommand ActorOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    List<object> Parameters = obj as List<object>;
                    int Id = Convert.ToInt32(Parameters[0]);
                    var ActorPage = new ActorWindow(Id);
                    ActorPage.Show();
                    Window CurrentWindow = Parameters[1] as Window;
                    CurrentWindow.Close();
                    
                });
            }
        }
    }
}
