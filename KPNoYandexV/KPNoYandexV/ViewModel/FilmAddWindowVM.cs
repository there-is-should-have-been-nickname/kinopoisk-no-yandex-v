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
    public class FilmAddWindowVM : BaseVM
    {
        private List<Button> genres;
        private string chosenGenresNames;
        private List<Genre> chosenGenres;
        private string chosenActorsNames;
        private List<Button> actors;
        private List<Actor> chosenActors;
        private FilmAddWindow currentWindow;

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
        public string FilmName { get { return filmName; } set { filmName = value; OnPropertyChanged(); } }
        public string FilmYear { get { return filmYear; } set { filmYear = value; OnPropertyChanged(); } }
        public string FilmDesc { get { return filmDesc; } set { filmDesc = value; OnPropertyChanged(); } }
        public string FilmCountry { get { return filmCountry; } set { filmCountry = value; OnPropertyChanged(); } }
        public string FilmRating { get { return filmRating; } set { filmRating = value; OnPropertyChanged(); } }
        public string FilmNumberReviews { get { return filmNumberReviews; } set { filmNumberReviews = value; OnPropertyChanged(); } }
        public string FilmPath { get { return filmPath; } set { filmPath = value; OnPropertyChanged(); } }
        public FilmAddWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }
        

        public FilmAddWindowVM(FilmAddWindow window)
        {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
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
                ChosenActors = new List<Actor>();
                ChosenGenres = new List<Genre>();
                ChosenGenresNames = "Жанры: ";
                ChosenActorsNames = "Актеры: ";
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
                        } else
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

        public BaseCommand BackClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<FilmAddWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                });
            }
        }

        public BaseCommand AddFilmClick
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
                        Film NewFilm = new Film();
                        NewFilm.Name = FilmName;
                        NewFilm.Description = FilmDesc;
                        NewFilm.Year = DateTime.Parse($"Jan 1, {FilmYear}");
                        NewFilm.Country = FilmCountry;
                        NewFilm.Rating = Convert.ToDouble(FilmRating);
                        NewFilm.ReviewsNumber = Convert.ToInt32(FilmNumberReviews);

                        if (!string.IsNullOrWhiteSpace(FilmPath))
                        {
                            string DbFilePath = FilmPath.Split("\\")[^1];

                            File.Copy(FilmPath, $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Posters\\{DbFilePath}", true);

                            NewFilm.PosterPath = DbFilePath;
                        }

                        using (var db = new KPNoYandexVContext())
                        {

                            db.Films.Add(NewFilm);
                            db.SaveChanges();

                            foreach (var ChosenGenre in ChosenGenres)
                            {
                                FilmsGenre NewFilmGenre = new FilmsGenre();
                                NewFilmGenre.FilmId = NewFilm.Id;
                                NewFilmGenre.GenreId = ChosenGenre.Id;
                                db.FilmsGenres.Add(NewFilmGenre);
                            }
                            foreach (var ChosenActor in ChosenActors)
                            {
                                FilmsActor NewFilmActor = new FilmsActor();
                                NewFilmActor.FilmId = NewFilm.Id;
                                NewFilmActor.ActorId = ChosenActor.Id;
                                db.FilmsActors.Add(NewFilmActor);
                            }
                            db.SaveChanges();
                            MessageBox.Show("Добавление успешно");
                            ViewHelper.WindowInteract<FilmAddWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                        }
                    }
                });
            }
        }
    }
}
