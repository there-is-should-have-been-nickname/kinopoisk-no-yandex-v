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
    public class FilmsPageVM : BaseVM
    {
        private List<Film> films;

        public List<Film> Films { get { return films; } set { films = value; OnPropertyChanged(); } }

        public FilmsPageVM()
        {
        }

        public void GetAllFilms()
        {
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
            }
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


    }
}
