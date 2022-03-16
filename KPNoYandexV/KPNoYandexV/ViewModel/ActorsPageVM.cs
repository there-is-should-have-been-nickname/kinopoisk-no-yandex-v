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
    public class ActorsPageVM : INotifyPropertyChanged
    {
        private List<Actor> actors;

        public List<Actor> Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }

        public ActorsPageVM()
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
                Actors = db.Actors.ToList();
            }
        }
    }
}
