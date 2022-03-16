using KPNoYandexV.Data;
using KPNoYandexV.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KPNoYandexV.ViewModel
{
    public class FilmsPageVM : INotifyPropertyChanged
    {
        private List<Film> films;

        public List<Film> Films { get { return films; } set { films = value; OnPropertyChanged(); } }

        public FilmsPageVM()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void GetAllFilms()
        {
            using (var db = new KPNoYandexVContext())
            {
                Films = db.Films.ToList();
            }
        }


        
    }
}
