using KPNoYandexV.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPNoYandexV.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private Page films;
        private Page genres;
        private Page actors;
        private Page currentPage;

        public Page Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public Page Genres { get { return genres; } set { genres = value; OnPropertyChanged(); } }
        public Page Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }
        public Page CurrentPage { get { return currentPage; } set { currentPage = value; OnPropertyChanged(); } }

        public MainWindowVM() {

            Films = new Films();
            Genres = new Genres();
            Actors = new Actors();

            CurrentPage = Films;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public FilmsSelectCommand FilmsClick
        {
            get
            {
                return new FilmsSelectCommand((obj) =>
                {
                    CurrentPage = Films;
                });
            }
        }

        public GenresSelectCommand GenresClick
        {
            get
            {
                return new GenresSelectCommand((obj) =>
                {
                    CurrentPage = Genres;
                });
            }
        }

        public ActorsSelectCommand ActorsClick
        {
            get
            {
                return new ActorsSelectCommand((obj) =>
                {
                    CurrentPage = Actors;
                });
            }
        }
    }
}
