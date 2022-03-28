using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPNoYandexV.ViewModel
{
    public class UserWindowVM : BaseVM
    {
        private Page films;
        private Page actors;
        private Page currentPage;
        private UserWindow currentWindow;

        public Page Films { get { return films; } set { films = value; OnPropertyChanged(); } }
        public Page Actors { get { return actors; } set { actors = value; OnPropertyChanged(); } }
        public Page CurrentPage { get { return currentPage; } set { currentPage = value; OnPropertyChanged(); } }
        public UserWindow CurrentWindow { get { return currentWindow; } set { currentWindow = value; OnPropertyChanged(); } }

        public UserWindowVM(UserWindow window) {
            CurrentWindow = window;

            Films = new Films();
            Actors = new Actors();

            CurrentPage = Films;
        }


        public BaseCommand FilmsClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    CurrentPage = Films;
                });
            }
        }

        public BaseCommand ActorsClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    CurrentPage = Actors;
                });
            }
        }

        public BaseCommand BackClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    var StartWind = new StartWindow();
                    StartWind.Show();

                    CurrentWindow.Close();
                });
            }
        }
    }
}
