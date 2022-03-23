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
    class GenreWindowVM : INotifyPropertyChanged
    {
        private Genre genre = new Genre();

        public Genre CurrentGenre { get { return genre; } set { genre = value; OnPropertyChanged(); } }

        public GenreWindowVM(int Id)
        {
            using (KPNoYandexVContext context = new KPNoYandexVContext())
            {
                CurrentGenre = context.Genres.SingleOrDefault(gen => gen.Id == Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
