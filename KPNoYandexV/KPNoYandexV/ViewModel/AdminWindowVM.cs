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
    public class AdminWindowVM : BaseVM
    {
        private List<Film> films;
        private List<Button> filmNamesUp;
        private string currentFilmNameUp;
        private List<Button> filmNamesDel;
        private string currentFilmNameDel;

        private List<Genre> genres;
        private List<Button> genreNamesUp;
        private string currentGenreNameUp;
        private List<Button> genreNamesDel;
        private string currentGenreNameDel;

        private AdminWindow currentWindow;

        public List<Film> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Button> FilmNamesUp { get { return filmNamesUp; } set { filmNamesUp = value; OnPropertyChanged(); } }
        public string CurrentFilmNameUp { get { return currentFilmNameUp; } set { currentFilmNameUp = value; OnPropertyChanged(); } }
        public List<Button> FilmNamesDel { get { return filmNamesDel; } set { filmNamesDel = value; OnPropertyChanged(); } }
        public string CurrentFilmNameDel { get { return currentFilmNameDel; } set { currentFilmNameDel = value; OnPropertyChanged(); } }

        public List<Genre> Genres { get { return genres; } set { genres = value; OnPropertyChanged(); } }
        public List<Button> GenreNamesUp { get { return genreNamesUp; } set { genreNamesUp = value; OnPropertyChanged(); } }
        public string CurrentGenreNameUp { get { return currentGenreNameUp; } set { currentGenreNameUp = value; OnPropertyChanged(); } }
        public List<Button> GenreNamesDel { get { return genreNamesDel; } set { genreNamesDel = value; OnPropertyChanged(); } }
        public string CurrentGenreNameDel { get { return currentGenreNameDel; } set { currentGenreNameDel = value; OnPropertyChanged(); } }
        public AdminWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        public AdminWindowVM(AdminWindow window) {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
                Genres = db.Genres.ToList();
            }
            FilmNamesUp = new List<Button>();
            FilmNamesDel = new List<Button>();
            GenreNamesUp = new List<Button>();
            GenreNamesDel = new List<Button>();

            foreach (var CurrentFilm in Films)
            {
                ViewHelper.AddFilmName(CurrentFilm, FilmNamesUp, ApplyFilmNameUp);
                ViewHelper.AddFilmName(CurrentFilm, FilmNamesDel, ApplyFilmNameDel);
            }

            foreach (var CurrentGenre in Genres)
            {
                ViewHelper.AddGenreName(CurrentGenre, GenreNamesUp, ApplyGenreNameUp);
                ViewHelper.AddGenreName(CurrentGenre, GenreNamesDel, ApplyGenreNameDel);
            }


            CurrentFilmNameUp = "";
            CurrentFilmNameDel = "";
            CurrentGenreNameUp = "";
            CurrentGenreNameDel = "";
        }

        public BaseCommand OpenAddFilmWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<AdminWindow, FilmAddWindow>(CurrentWindow, new FilmAddWindow());
                });
            }
        }
        public BaseCommand OpenUpdateFilmWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentFilmNameUp))
                    {
                        int Id = 0;
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurFilm = db.Films.SingleOrDefault(F => F.Name == CurrentFilmNameUp);
                            Id = CurFilm.Id;
                        }
                        ViewHelper.WindowInteract<AdminWindow, UpdateFilmWindow>(CurrentWindow, new UpdateFilmWindow(Id));
                    }
                    
                });
            }
        }

        public BaseCommand RemoveFilm
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentFilmNameDel))
                    {
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurFilm = db.Films.SingleOrDefault(F => F.Name == CurrentFilmNameDel);

                            var ExistedGenres = db.FilmsGenres.Where(FG => FG.FilmId == CurFilm.Id);
                            foreach (var FilmGenre in ExistedGenres)
                            {
                                db.FilmsGenres.Remove(FilmGenre);
                            }
                            var ExistedActors = db.FilmsActors.Where(FA => FA.FilmId == CurFilm.Id);
                            foreach (var FilmActor in ExistedActors)
                            {
                                db.FilmsActors.Remove(FilmActor);
                            }
                            db.SaveChanges();
                            db.Films.Remove(CurFilm);
                            db.SaveChanges();
                            MessageBox.Show("Удаление успешно");
                        }
                    }
                    
                });
            }
        }

        public BaseCommand ApplyFilmNameUp
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurFilm = db.Films.SingleOrDefault(F => F.Id == Id);
                        CurrentFilmNameUp = CurFilm.Name;
                    }
                });
            }
        }

        public BaseCommand ApplyFilmNameDel
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurFilm = db.Films.SingleOrDefault(F => F.Id == Id);
                        CurrentFilmNameDel = CurFilm.Name;
                    }
                });
            }
        }

        public BaseCommand OpenAddGenreWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<AdminWindow, GenreAddWindow>(CurrentWindow, new GenreAddWindow());
                });
            }
        }
        public BaseCommand OpenUpdateGenreWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentGenreNameUp))
                    {
                        int Id = 0;
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurGenre = db.Genres.SingleOrDefault(G => G.Name == CurrentGenreNameUp);
                            Id = CurGenre.Id;
                        }
                        ViewHelper.WindowInteract<AdminWindow, UpdateGenreWindow>(CurrentWindow, new UpdateGenreWindow(Id));
                    }

                });
            }
        }

        public BaseCommand RemoveGenre
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentGenreNameDel))
                    {
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurGenre = db.Genres.SingleOrDefault(G => G.Name == CurrentGenreNameDel);

                            var ExistedFilms = db.FilmsGenres.Where(FG => FG.GenreId == CurGenre.Id);
                            foreach (var FilmGenre in ExistedFilms)
                            {
                                db.FilmsGenres.Remove(FilmGenre);
                            }
                            db.SaveChanges();
                            db.Genres.Remove(CurGenre);
                            db.SaveChanges();
                            MessageBox.Show("Удаление успешно");

                            var TempGenreNamesDel = new List<Button>();
                            foreach (var GenreName in GenreNamesDel)
                            {
                                if (GenreName.Content as string != CurrentGenreNameDel)
                                {
                                    TempGenreNamesDel.Add(GenreName);
                                }
                                
                            }
                            GenreNamesDel = new List<Button>();
                            foreach (var TGenre in TempGenreNamesDel)
                            {
                                GenreNamesDel.Add(TGenre);
                            }
                            CurrentGenreNameDel = "";


                        }
                    }

                });
            }
        }

        public BaseCommand ApplyGenreNameUp
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurGenre = db.Genres.SingleOrDefault(G => G.Id == Id);
                        CurrentGenreNameUp = CurGenre.Name;
                    }
                });
            }
        }

        public BaseCommand ApplyGenreNameDel
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurGenre = db.Genres.SingleOrDefault(G => G.Id == Id);
                        CurrentGenreNameDel = CurGenre.Name;
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
                    ViewHelper.WindowInteract<AdminWindow, StartWindow>(CurrentWindow, new StartWindow());
                });
            }
        }
    }
}
