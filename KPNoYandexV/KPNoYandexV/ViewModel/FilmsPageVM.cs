using KPNoYandexV.Data;
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
using System.Windows.Controls;

namespace KPNoYandexV.ViewModel
{
    public class FilmsPageVM : BaseVM
    {
        private List<Film> films;
        private List<Button> genresButtons;
        private Films filmsPage;
        private string currentGenre;

        public List<Film> Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public List<Button> GenresButtons { get { return genresButtons; } set { genresButtons = value; OnPropertyChanged(); } }
        public Films FilmsPage { get { return filmsPage; } set { filmsPage = value; OnPropertyChanged(); } }
        public string CurrentGenre { get { return currentGenre; } set { currentGenre = value; OnPropertyChanged(); } }

        public FilmsPageVM(Films page)
        {
            FilmsPage = page;
            CurrentGenre = "Все";

            List<Genre> Genres = new List<Genre>();
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
                Genres = db.Genres.ToList();
            }

            GenresButtons = new List<Button>();
            AddGenreButton("Все");

            foreach (var CurrentGenre in Genres)
            {
                AddGenreButton(CurrentGenre);
            }

        }

        private void AddGenreButton(Genre CurrentGenre)
        {
            var btn = new Button();
            btn.Width = 120;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 14;
            btn.Content = CurrentGenre.Name;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = ApplyGenreFilter;
            btn.CommandParameter = CurrentGenre.Id;

            GenresButtons.Add(btn);
        }

        private void AddGenreButton(string content)
        {
            var btn = new Button();
            btn.Width = 120;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 14;
            btn.Content = content;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = ApplyGenreFilter;
            btn.CommandParameter = -1;

            GenresButtons.Add(btn);
        }

        public BaseCommand FilmOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);
                    var FilmPage = new FilmWindow(Id);
                    FilmPage.Show();
                });
            }
        }

        public BaseCommand ApplyGenreFilter
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);

                    Films = new List<Film>();

                    using (var db = new KPNoYandexVContext())
                    {
                        if (Id == -1)
                        {
                            Films = db.Films.ToList();
                            CurrentGenre = "Все";
                        } else
                        {
                            var FilmsGenres = db.FilmsGenres.Where(FG => FG.GenreId == Id).ToList();
                            var CurGenre = db.Genres.SingleOrDefault(G => G.Id == Id);
                            CurrentGenre = CurGenre.Name;

                            foreach (var FilmGenre in FilmsGenres)
                            {
                                Film Fil = db.Films.SingleOrDefault(F => F.Id == FilmGenre.FilmId);
                                Films.Add(Fil);
                            }
                        }                       
                    }


                    FilmsPage.DrawFilms(this);
                });
            }
        }


    }
}
