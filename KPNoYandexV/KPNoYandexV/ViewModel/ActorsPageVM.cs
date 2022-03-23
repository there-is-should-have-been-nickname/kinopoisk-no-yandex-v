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
    public class ActorsPageVM : BaseVM
    {
        private List<Actor> actors;

        public List<Actor> Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }

        public ActorsPageVM()
        {
            using (var db = new KPNoYandexVContext())
            {
                Actors = db.Actors.ToList();
            }
        }

        public BaseCommand ActorOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);
                    var ActorPage = new ActorWindow(Id);
                    ActorPage.Show();
                });
            }
        }
    }
}
