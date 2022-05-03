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
    public class UpdateActorWindowVM : BaseVM
    {
        private List<Button> films;
        private string chosenFilmsNames;
        private List<Film> chosenFilms;
        private UpdateActorWindow currentWindow;

        private string actorFirstName;
        private string actorLastName;
        private string actorCountry;
        private string actorDateBirth;
        private string actorPath;

        private Actor currentActor;

        public List<Button> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Film> ChosenFilms { get { return chosenFilms; } set { chosenFilms = value; OnPropertyChanged(); } }
        public string ChosenFilmsNames { get { return chosenFilmsNames; } set { chosenFilmsNames = value; OnPropertyChanged(); } }
        public string ActorFirstName { get { return actorFirstName; } set { actorFirstName = value; OnPropertyChanged(); } }
        public string ActorLastName { get { return actorLastName; } set { actorLastName = value; OnPropertyChanged(); } }
        public string ActorCountry { get { return actorCountry; } set { actorCountry = value; OnPropertyChanged(); } }
        public string ActorDateBirth { get { return actorDateBirth; } set { actorDateBirth = value; OnPropertyChanged(); } }
        public string ActorPath { get { return actorPath; } set { actorPath = value; OnPropertyChanged(); } }

        public Actor CurrentActor { get { return currentActor; } set { currentActor = value; OnPropertyChanged(); } }
        public UpdateActorWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }

        public UpdateActorWindowVM(int Id, UpdateActorWindow window)
        {
            CurrentWindow = window;
            using (var db = new KPNoYandexVContext())
            {
                CurrentActor = db.Actors.SingleOrDefault(A => A.Id == Id);
                ChosenFilms = new List<Film>();

                Films = new List<Button>();

                var DbFilms = db.Films.ToList();
                foreach (var Fil in DbFilms)
                {
                    ViewHelper.AddButtons<Film>(Fil, Films, ChooseFilm);
                }

                List<FilmsActor> FilmActors = db.FilmsActors.Where(FG => FG.ActorId == CurrentActor.Id).ToList();
                foreach (var FilmAct in FilmActors)
                {
                    Film Fil = db.Films.SingleOrDefault(F => F.Id == FilmAct.FilmId);
                    ChosenFilms.Add(Fil);
                }
            }
            ActorFirstName = CurrentActor.FirstName;
            ActorLastName = CurrentActor.LastName;
            ActorCountry = CurrentActor.Country;
            ActorDateBirth = CurrentActor.DateBirth.ToString();
            ActorPath = CurrentActor.FacePath;

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

        public BaseCommand BackClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    ViewHelper.WindowInteract<UpdateActorWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                });
            }
        }

        public BaseCommand UpdateActorClick
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
                        CurrentActor.FirstName = ActorFirstName;
                        CurrentActor.LastName = ActorLastName;
                        CurrentActor.DateBirth = DateTime.Parse(ActorDateBirth);
                        CurrentActor.Country = ActorCountry;

                        if (!string.IsNullOrWhiteSpace(ActorPath))
                        {
                            string DbFilePath = ActorPath.Split("\\")[^1];
                            if (DbFilePath != ActorPath)
                            {
                                File.Copy(ActorPath, $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Faces\\{DbFilePath}", true);

                                CurrentActor.FacePath = DbFilePath;
                            }
                        }


                        using (var db = new KPNoYandexVContext())
                        {

                            db.Actors.Update(CurrentActor);
                            db.SaveChanges();

                            var ExistedFilms = db.FilmsActors.Where(FG => FG.ActorId == CurrentActor.Id);
                            foreach (var ExistedFilm in ExistedFilms)
                            {
                                db.FilmsActors.Remove(ExistedFilm);
                            }
                            db.SaveChanges();


                            foreach (var ChosenFilm in ChosenFilms)
                            {
                                FilmsActor NewFilmActor = new FilmsActor();
                                NewFilmActor.ActorId = CurrentActor.Id;
                                NewFilmActor.FilmId = ChosenFilm.Id;
                                db.FilmsActors.Add(NewFilmActor);
                            }
                            db.SaveChanges();
                            MessageBox.Show("Изменение успешно");
                            ViewHelper.WindowInteract<UpdateActorWindow, AdminWindow>(CurrentWindow, new AdminWindow());
                        }
                    }
                });
            }
        }
    }
}
