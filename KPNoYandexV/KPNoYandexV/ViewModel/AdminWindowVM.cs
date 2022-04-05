﻿using KPNoYandexV.Data;
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

        public List<Film> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Button> FilmNamesUp { get { return filmNamesUp; } set { filmNamesUp = value; OnPropertyChanged(); } }
        public string CurrentFilmNameUp { get { return currentFilmNameUp; } set { currentFilmNameUp = value; OnPropertyChanged(); } }
        public List<Button> FilmNamesDel { get { return filmNamesDel; } set { filmNamesDel = value; OnPropertyChanged(); } }
        public string CurrentFilmNameDel { get { return currentFilmNameDel; } set { currentFilmNameDel = value; OnPropertyChanged(); } }

        public AdminWindowVM() {
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
            }
            FilmNamesUp = new List<Button>();
            FilmNamesDel = new List<Button>();

            foreach (var CurrentFilm in Films)
            {
                AddFilmNameUpGenre(CurrentFilm);
                AddFilmNameDelGenre(CurrentFilm);
            }
            currentFilmNameUp = "";
            currentFilmNameDel = "";
        }

        private void AddFilmNameUpGenre(Film CurrentFilm)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            btn.Content = CurrentFilm.Name;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = ApplyFilmNameUp;
            btn.CommandParameter = CurrentFilm.Id;

            FilmNamesUp.Add(btn);
        }
        private void AddFilmNameDelGenre(Film CurrentFilm)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            btn.Content = CurrentFilm.Name;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = ApplyFilmNameDel;
            btn.CommandParameter = CurrentFilm.Id;

            FilmNamesDel.Add(btn);
        }

        public BaseCommand OpenAddFilmWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    var wind = new FilmAddWindow();
                    wind.Show();
                });
            }
        }
        public BaseCommand OpenUpdateFilmWindow
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = 0;
                    using (var db = new KPNoYandexVContext())
                    {
                        var CurFilm = db.Films.SingleOrDefault(F => F.Name == CurrentFilmNameUp);
                        Id = CurFilm.Id;
                    }
                    var wind = new UpdateFilmWindow(Id);
                    wind.Show();
                });
            }
        }

        public BaseCommand RemoveFilm
        {
            get
            {
                return new BaseCommand((obj) =>
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
    }
}
