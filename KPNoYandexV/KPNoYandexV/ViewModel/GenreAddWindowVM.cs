using KPNoYandexV.Data;
using KPNoYandexV.Lib;
using KPNoYandexV.Model;
using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KPNoYandexV.ViewModel
{
    public class GenreAddWindowVM : BaseVM
    {
        private List<Button> films;
        private string chosenFilmsNames;
        private List<Film> chosenFilms;
        private GenreAddWindow currentWindow;

        private string genreName;

        public List<Button> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Film> ChosenFilms { get { return chosenFilms; } set { chosenFilms = value; OnPropertyChanged(); } }
        public string ChosenFilmsNames { get { return chosenFilmsNames; } set { chosenFilmsNames = value; OnPropertyChanged(); } }
        public string GenreName { get { return genreName; } set { genreName = value; OnPropertyChanged(); } }
        public GenreAddWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        public GenreAddWindowVM(GenreAddWindow window)
        {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                Films = new List<Button>();
                var DbFilms = db.Films.ToList();
                foreach (var Fil in DbFilms)
                {
                    ViewHelper.AddButtons<Film>(Fil, Films, ChooseFilm);
                }
                ChosenFilms = new List<Film>();
                ChosenFilmsNames = "Фильмы: ";
            }
        }
        public BaseCommand ChooseFilm
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurFilm = db.Films.SingleOrDefault(F => F.Id == Id);
                        var FilmInChosen = ChosenFilms.SingleOrDefault(ChoseF => ChoseF.Id == CurFilm.Id);
                        if (FilmInChosen != null)
                        {
                            ChosenFilms = ChosenFilms.Where(ChoseF => ChoseF.Id != FilmInChosen.Id).ToList();
                        }
                        else
                        {
                            ChosenFilms.Add(CurFilm);
                        }
                        ChosenFilmsNames = "Фильмы: ";
                        foreach (var Fil in ChosenFilms)
                        {
                            ChosenFilmsNames += Fil.Name + ", ";
                        }

                    }
                });
            }
        }

        public BaseCommand BackClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<GenreAddWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                });
            }
        }

        public BaseCommand AddGenreClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    var ErrorMessage = ErrorHandler.GetGenreErrorMessage(GenreName);
                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        ErrorHandler.ShowError(ErrorMessage);
                    }
                    else
                    {
                        Genre NewGenre = new Genre();
                        NewGenre.Name = GenreName;

                        using (var db = new KPNoYandexVContext())
                        {

                            db.Genres.Add(NewGenre);
                            db.SaveChanges();

                            foreach (var ChosenFilm in ChosenFilms)
                            {
                                FilmsGenre NewFilmGenre = new FilmsGenre();
                                NewFilmGenre.GenreId = NewGenre.Id;
                                NewFilmGenre.FilmId = ChosenFilm.Id;
                                db.FilmsGenres.Add(NewFilmGenre);
                            }
                            db.SaveChanges();
                            MessageBox.Show("Добавление успешно");
                            ViewHelper.WindowInteract<GenreAddWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                        }
                    }
                });
            }
        }
    }
}
