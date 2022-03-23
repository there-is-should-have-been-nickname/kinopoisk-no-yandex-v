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

namespace KPNoYandexV.ViewModel
{
    public class GenresPageVM : BaseVM
    {
        private List<Genre> genres;

        public List<Genre> Genres { get { return genres; } set { genres = value; OnPropertyChanged(); } }

        public GenresPageVM()
        {
        }

        public void GetAllFilms()
        {
            using (var db = new KPNoYandexVContext())
            {
                Genres = db.Genres.ToList();
            }
        }

        public BaseCommand GenreOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);
                    var FilmPage = new GenreWindow(Id);
                    FilmPage.Show();
                });
            }
        }
    }
}
