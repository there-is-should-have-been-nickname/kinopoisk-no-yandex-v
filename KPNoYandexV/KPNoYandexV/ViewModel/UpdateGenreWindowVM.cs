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
    public class UpdateGenreWindowVM : BaseVM
    {
        private List<Button> films;
        private string chosenFilmsNames;
        private List<Film> chosenFilms;
        private UpdateGenreWindow currentWindow;
        private Genre currentGenre;

        private string genreName;

        public List<Button> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Film> ChosenFilms { get { return chosenFilms; } set { chosenFilms = value; OnPropertyChanged(); } }
        public string ChosenFilmsNames { get { return chosenFilmsNames; } set { chosenFilmsNames = value; OnPropertyChanged(); } }
        public string GenreName { get { return genreName; } set { genreName = value; OnPropertyChanged(); } }
        public UpdateGenreWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        public Genre CurrentGenre { get { return currentGenre; } set { currentGenre = value; OnPropertyChanged(); } }
        public UpdateGenreWindowVM(int Id, UpdateGenreWindow window)
        {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                CurrentGenre = db.Genres.SingleOrDefault(G => G.Id == Id);
                ChosenFilms = new List<Film>();

                Films = new List<Button>();

                var DbFilms = db.Films.ToList();
                foreach (var Fil in DbFilms)
                {
                    ViewHelper.AddButtons<Film>(Fil, Films, ChooseFilm);
                }

                List<FilmsGenre> FilmGenres = db.FilmsGenres.Where(FG => FG.GenreId == CurrentGenre.Id).ToList();
                foreach (var FilmGen in FilmGenres)
                {
                    Film Fil = db.Films.SingleOrDefault(F => F.Id == FilmGen.FilmId);
                    ChosenFilms.Add(Fil);
                }
            }
            GenreName = CurrentGenre.Name;

            ChosenFilmsNames = "Фильмы: ";
            foreach (var Fil in ChosenFilms)
            {
                ChosenFilmsNames += Fil.Name + ", ";
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

        public BaseCommand UpdateGenreClick
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
                        CurrentGenre.Name = GenreName;


                        using (var db = new KPNoYandexVContext())
                        {

                            db.Genres.Update(CurrentGenre);
                            db.SaveChanges();

                            var ExistedFilms = db.FilmsGenres.Where(FG => FG.GenreId == CurrentGenre.Id);
                            foreach (var ExistedFilm in ExistedFilms)
                            {
                                db.FilmsGenres.Remove(ExistedFilm);
                            }
                            db.SaveChanges();


                            foreach (var ChosenFilm in ChosenFilms)
                            {
                                FilmsGenre NewFilmGenre = new FilmsGenre();
                                NewFilmGenre.GenreId = CurrentGenre.Id;
                                NewFilmGenre.FilmId = ChosenFilm.Id;
                                db.FilmsGenres.Add(NewFilmGenre);
                            }
                            db.SaveChanges();
                            MessageBox.Show("Изменение успешно");
                            ViewHelper.WindowInteract<UpdateGenreWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                        }
                    }
                });
            }
        }
    }
}
