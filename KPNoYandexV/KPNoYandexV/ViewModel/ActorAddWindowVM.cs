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
using System.Windows;
using System.Windows.Controls;

namespace KPNoYandexV.ViewModel
{
    public class ActorAddWindowVM : BaseVM
    {
        private List<Button> films;
        private string chosenFilmsNames;
        private List<Film> chosenFilms;
        private ActorAddWindow currentWindow;

        private string actorFirstName;
        private string actorLastName;
        private string actorCountry;
        private string actorDateBirth;
        private string actorPath;

        public List<Button> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Film> ChosenFilms { get { return chosenFilms; } set { chosenFilms = value; OnPropertyChanged(); } }
        public string ChosenFilmsNames { get { return chosenFilmsNames; } set { chosenFilmsNames = value; OnPropertyChanged(); } }
        public string ActorFirstName { get { return actorFirstName; } set { actorFirstName = value; OnPropertyChanged(); } }
        public string ActorLastName { get { return actorLastName; } set { actorLastName = value; OnPropertyChanged(); } }
        public string ActorCountry { get { return actorCountry; } set { actorCountry = value; OnPropertyChanged(); } }
        public string ActorDateBirth { get { return actorDateBirth; } set { actorDateBirth = value; OnPropertyChanged(); } }
        public string ActorPath { get { return actorPath; } set { actorPath = value; OnPropertyChanged(); } }
        public ActorAddWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }

        public ActorAddWindowVM(ActorAddWindow window)
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
                        ActorPath = openFileDialog.FileName;
                    }
                });
            }
        }

        public BaseCommand AddActorClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    var ErrorMessage = ErrorHandler.GetActorErrorMessage(ActorFirstName, ActorLastName, ActorCountry);
                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        ErrorHandler.ShowError(ErrorMessage);
                    }
                    else
                    {
                        Actor NewActor = new Actor();
                        NewActor.FirstName = ActorFirstName;
                        NewActor.LastName = ActorLastName;
                        NewActor.DateBirth = DateTime.Parse(ActorDateBirth);
                        NewActor.Country = ActorCountry;
                        string DbFilePath = ActorPath.Split("\\")[^1];

                        File.Copy(ActorPath, $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Faces\\{DbFilePath}", true);

                        NewActor.FacePath = DbFilePath;

                        using (var db = new KPNoYandexVContext())
                        {

                            db.Actors.Add(NewActor);
                            db.SaveChanges();

                            foreach (var ChosenFilm in ChosenFilms)
                            {
                                FilmsActor NewFilmActor = new FilmsActor();
                                NewFilmActor.ActorId = NewActor.Id;
                                NewFilmActor.FilmId = ChosenFilm.Id;
                                db.FilmsActors.Add(NewFilmActor);
                            }
                            db.SaveChanges();
                            MessageBox.Show("Добавление успешно");
                            ViewHelper.WindowInteract<ActorAddWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                        }
                    }
                });
            }
        }
    }
}
