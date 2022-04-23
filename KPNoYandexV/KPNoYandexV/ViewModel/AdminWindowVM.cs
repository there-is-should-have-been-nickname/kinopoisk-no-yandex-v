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

        private List<Actor> actors;
        private List<Button> actorNamesUp;
        private string currentActorNameUp;
        private List<Button> actorNamesDel;
        private string currentActorNameDel;

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


        public List<Actor> Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }
        public List<Button> ActorNamesUp { get { return actorNamesUp; } set { actorNamesUp = value; OnPropertyChanged(); } }
        public string CurrentActorNameUp { get { return currentActorNameUp; } set { currentActorNameUp = value; OnPropertyChanged(); } }
        public List<Button> ActorNamesDel { get { return actorNamesDel; } set { actorNamesDel = value; OnPropertyChanged(); } }
        public string CurrentActorNameDel { get { return currentActorNameDel; } set { currentActorNameDel = value; OnPropertyChanged(); } }

        public AdminWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        public AdminWindowVM(AdminWindow window) {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
                Genres = db.Genres.ToList();
                Actors = db.Actors.ToList();
            }
            FilmNamesUp = new List<Button>();
            FilmNamesDel = new List<Button>();
            GenreNamesUp = new List<Button>();
            GenreNamesDel = new List<Button>();
            ActorNamesUp = new List<Button>();
            ActorNamesDel = new List<Button>();

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
            foreach (var CurrentActor in Actors)
            {
                ViewHelper.AddActorName(CurrentActor, ActorNamesUp, ApplyActorNameUp);
                ViewHelper.AddActorName(CurrentActor, ActorNamesDel, ApplyActorNameDel);
            }


            CurrentFilmNameUp = "";
            CurrentFilmNameDel = "";
            CurrentGenreNameUp = "";
            CurrentGenreNameDel = "";
            CurrentActorNameUp = "";
            CurrentActorNameDel = "";
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

                            string Copy = CurrentFilmNameDel;
                            FilmNamesDel = FilmNamesDel.FindAll(FilmName => FilmName.Content as string != CurrentFilmNameDel);
                            FilmNamesUp = FilmNamesDel;
                            if (CurrentFilmNameUp == Copy)
                            {
                                CurrentFilmNameUp = "";
                            }
                            CurrentFilmNameDel = "";
                            
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

                            string Copy = CurrentGenreNameDel;
                            GenreNamesDel = GenreNamesDel.FindAll(GenreName => GenreName.Content as string != CurrentGenreNameDel);
                            GenreNamesUp = GenreNamesDel;
                            if (CurrentGenreNameUp == Copy)
                            {
                                CurrentGenreNameUp = "";
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

        public BaseCommand OpenAddActorWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<AdminWindow, ActorAddWindow>(CurrentWindow, new ActorAddWindow());
                });
            }
        }
        public BaseCommand OpenUpdateActorWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentActorNameUp))
                    {
                        int Id = 0;
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurActor = db.Actors.SingleOrDefault(G => G.LastName == CurrentActorNameUp);
                            Id = CurActor.Id;
                        }
                        ViewHelper.WindowInteract<AdminWindow, UpdateActorWindow>(CurrentWindow, new UpdateActorWindow(Id));
                    }

                });
            }
        }

        public BaseCommand RemoveActor
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(CurrentActorNameDel))
                    {
                        using (var db = new KPNoYandexVContext())
                        {
                            var CurActor = db.Actors.SingleOrDefault(A => A.LastName == CurrentActorNameDel);

                            var ExistedFilms = db.FilmsActors.Where(FG => FG.ActorId == CurActor.Id);
                            foreach (var FilmActor in ExistedFilms)
                            {
                                db.FilmsActors.Remove(FilmActor);
                            }
                            db.SaveChanges();
                            db.Actors.Remove(CurActor);
                            db.SaveChanges();
                            MessageBox.Show("Удаление успешно");

                            string Copy = CurrentActorNameDel;
                            ActorNamesDel = ActorNamesDel.FindAll(ActorName => ActorName.Content as string != CurrentActorNameDel);
                            ActorNamesUp = ActorNamesDel;
                            if (CurrentActorNameUp == Copy)
                            {
                                CurrentActorNameUp = "";
                            }
                            CurrentActorNameDel = "";


                        }
                    }

                });
            }
        }

        public BaseCommand ApplyActorNameUp
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurActor = db.Actors.SingleOrDefault(A => A.Id == Id);
                        CurrentActorNameUp = CurActor.LastName;
                    }
                });
            }
        }

        public BaseCommand ApplyActorNameDel
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurActor = db.Actors.SingleOrDefault(G => G.Id == Id);
                        CurrentActorNameDel = CurActor.LastName;
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
