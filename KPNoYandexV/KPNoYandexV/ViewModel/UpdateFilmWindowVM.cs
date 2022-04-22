using KPNoYandexV.Data;
using KPNoYandexV.Lib;
using KPNoYandexV.Model;
using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KPNoYandexV.ViewModel
{
    public class UpdateFilmWindowVM: BaseVM
    {
        private List<Button> genres;
        private string chosenGenresNames;
        private List<Genre> chosenGenres;
        private string chosenActorsNames;
        private List<Button> actors;
        private List<Actor> chosenActors;
        private UpdateFilmWindow currentWindow;

        private Film currentFilm;
        private string filmName;
        private string filmYear;
        private string filmDesc;
        private string filmCountry;
        private string filmRating;
        private string filmNumberReviews;
        private string filmPath;
        public List<Button> Genres { get { return genres; } set { genres = value; OnPropertyChanged(); } }
        public List<Genre> ChosenGenres { get { return chosenGenres; } set { chosenGenres = value; OnPropertyChanged(); } }
        public string ChosenGenresNames { get { return chosenGenresNames; } set { chosenGenresNames = value; OnPropertyChanged(); } }
        public List<Button> Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }
        public List<Actor> ChosenActors { get { return chosenActors; } set { chosenActors = value; OnPropertyChanged(); } }
        public string ChosenActorsNames { get { return chosenActorsNames; } set { chosenActorsNames = value; OnPropertyChanged(); } }

        public Film CurrentFilm { get { return currentFilm; } set { currentFilm = value; OnPropertyChanged(); } }
        public string FilmName { get { return filmName; } set { filmName = value; OnPropertyChanged(); } }
        public string FilmYear { get { return filmYear; } set { filmYear = value; OnPropertyChanged(); } }
        public string FilmDesc { get { return filmDesc; } set { filmDesc = value; OnPropertyChanged(); } }
        public string FilmCountry { get { return filmCountry; } set { filmCountry = value; OnPropertyChanged(); } }
        public string FilmRating { get { return filmRating; } set { filmRating = value; OnPropertyChanged(); } }
        public string FilmNumberReviews { get { return filmNumberReviews; } set { filmNumberReviews = value; OnPropertyChanged(); } }
        public string FilmPath { get { return filmPath; } set { filmPath = value; OnPropertyChanged(); } }

        public UpdateFilmWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }

        public UpdateFilmWindowVM(int Id, UpdateFilmWindow window)
        {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                CurrentFilm = db.Films.SingleOrDefault(F => F.Id == Id);
                ChosenActors = new List<Actor>();
                ChosenGenres = new List<Genre>();

                Genres = new List<Button>();
                Actors = new List<Button>();
                var DbGenres = db.Genres.ToList();
                foreach (var Gen in DbGenres)
                {
                    ViewHelper.AddButtons<Genre>(Gen, Genres, ChooseGenre);
                }
                var DbActors = db.Actors.ToList();
                foreach (var Act in DbActors)
                {
                    ViewHelper.AddButtons<Actor>(Act, Actors, ChooseActor);
                }

                List<FilmsGenre> FilmGenres = db.FilmsGenres.Where(FG => FG.FilmId == CurrentFilm.Id).ToList();
                foreach (var FilmGen in FilmGenres)
                {
                    Genre Gen = db.Genres.SingleOrDefault(G => G.Id == FilmGen.GenreId);
                    ChosenGenres.Add(Gen);
                }

                List<FilmsActor> FilmActors = db.FilmsActors.Where(FA => FA.FilmId == CurrentFilm.Id).ToList();
                foreach (var FilmAct in FilmActors)
                {
                    Actor Act = db.Actors.SingleOrDefault(A => A.Id == FilmAct.ActorId);
                    ChosenActors.Add(Act);
                }
            }
            FilmName = CurrentFilm.Name;
            FilmDesc = CurrentFilm.Description;
            FilmCountry = CurrentFilm.Country;
            FilmNumberReviews = CurrentFilm.ReviewsNumber.ToString();
            FilmPath = CurrentFilm.PosterPath;
            FilmRating = CurrentFilm.Rating.ToString();
            FilmYear = CurrentFilm.Year.Value.Year.ToString();

            ChosenGenresNames = "Жанры: ";
            foreach (var Gen in ChosenGenres)
            {
                ChosenGenresNames += Gen.Name + ", ";
            }

            ChosenActorsNames = "Актеры: ";
            foreach (var Act in ChosenActors)
            {
                ChosenActorsNames += Act.FirstName + " " + Act.LastName + ", ";
            }

        }
        
        public BaseCommand ChooseGenre
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurGenre = db.Genres.SingleOrDefault(G => G.Id == Id);
                        var GenreInChosen = ChosenGenres.SingleOrDefault(ChoseG => ChoseG.Id == CurGenre.Id);
                        if (GenreInChosen != null)
                        {
                            ChosenGenres = ChosenGenres.Where(ChoseG => ChoseG.Id != GenreInChosen.Id).ToList();
                        }
                        else
                        {
                            ChosenGenres.Add(CurGenre);
                        }
                        ChosenGenresNames = "Жанры: ";
                        foreach (var Gen in ChosenGenres)
                        {
                            ChosenGenresNames += Gen.Name + ", ";
                        }

                    }
                });
            }
        }

        public BaseCommand ChooseActor
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    using (var db = new KPNoYandexVContext())
                    {
                        var CurActor = db.Actors.SingleOrDefault(A => A.Id == Id);
                        var ActorInChosen = ChosenActors.SingleOrDefault(ChoseA => ChoseA.Id == CurActor.Id);
                        if (ActorInChosen != null)
                        {
                            ChosenActors = ChosenActors.Where(ChoseA => ChoseA.Id != ActorInChosen.Id).ToList();
                        }
                        else
                        {
                            ChosenActors.Add(CurActor);
                        }
                        ChosenActorsNames = "Актеры: ";
                        foreach (var Act in ChosenActors)
                        {
                            ChosenActorsNames += Act.FirstName + " " + Act.LastName + ", ";
                        }

                    }
                });
            }
        }
        public BaseCommand ChooseFile
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        FilmPath = openFileDialog.FileName;
                    }
                });
            }
        }

        public BaseCommand UpdateFilmClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                var ErrorMessage = ErrorHandler.GetFilmErrorMessage(FilmName, FilmYear, FilmCountry, FilmRating);
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    ErrorHandler.ShowError(ErrorMessage);
                }
                else
                {
                    CurrentFilm.Name = FilmName;
                    CurrentFilm.Description = FilmDesc;
                    CurrentFilm.Year = DateTime.Parse($"Jan 1, {FilmYear}");
                    CurrentFilm.Country = FilmCountry;
                    CurrentFilm.Rating = Convert.ToDouble(FilmRating);
                    CurrentFilm.ReviewsNumber = Convert.ToInt32(FilmNumberReviews);
                    string DbFilePath = FilmPath.Split("\\")[^1];
                    if (DbFilePath != FilmPath)
                    {
                        File.Copy(FilmPath, $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Posters\\{DbFilePath}", true);

                        CurrentFilm.PosterPath = DbFilePath;
                    }

                       

                    using (var db = new KPNoYandexVContext())
                    {

                        db.Films.Update(CurrentFilm);
                        db.SaveChanges();

                        var ExistedGenres = db.FilmsGenres.Where(FG => FG.FilmId == CurrentFilm.Id);
                        foreach (var ExistedGenre in ExistedGenres)
                        {
                            db.FilmsGenres.Remove(ExistedGenre);
                        }
                        db.SaveChanges();

                        var ExistedActors = db.FilmsActors.Where(FA => FA.FilmId == CurrentFilm.Id);
                        foreach (var ExistedActor in ExistedActors)
                        {
                            db.FilmsActors.Remove(ExistedActor);
                        }
                        db.SaveChanges();

                        foreach (var ChosenGenre in ChosenGenres)
                        {
                            FilmsGenre NewFilmGenre = new FilmsGenre();
                            NewFilmGenre.FilmId = CurrentFilm.Id;
                            NewFilmGenre.GenreId = ChosenGenre.Id;
                            db.FilmsGenres.Add(NewFilmGenre);
                        }
                        foreach (var ChosenActor in ChosenActors)
                        {
                            FilmsActor NewFilmActor = new FilmsActor();
                            NewFilmActor.FilmId = CurrentFilm.Id;
                            NewFilmActor.ActorId = ChosenActor.Id;
                            db.FilmsActors.Add(NewFilmActor);
                        }
                        db.SaveChanges();
                        MessageBox.Show("Изменение успешно");
                        ViewHelper.WindowInteract<UpdateFilmWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                    }
                    }
                });
            }
        }
    }
}
