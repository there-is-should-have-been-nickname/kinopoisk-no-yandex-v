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
    public class GenresPageVM : INotifyPropertyChanged
    {
        private List<Genre> genres;

        public List<Genre> Genres { get { return genres; } set { genres = value; OnPropertyChanged(); } }

        public GenresPageVM()
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
                Genres = db.Genres.ToList();
            }
        }
    }
}
