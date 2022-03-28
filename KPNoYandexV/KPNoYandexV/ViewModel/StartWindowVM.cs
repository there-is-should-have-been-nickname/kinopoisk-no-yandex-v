using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KPNoYandexV.ViewModel
{
    public class StartWindowVM : BaseVM
    {
        private StartWindow startWind;

        public StartWindow StartWind { get { return startWind; } set { startWind = value; OnPropertyChanged(); } }
        public StartWindowVM(StartWindow window) {
            StartWind = window;
        }

        public BaseCommand UserOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    Window CurrentWindow = obj as Window;
                    var UserWnd = new UserWindow();
                    UserWnd.Show();
                    StartWind.Close();
                });
            }
        }

        public BaseCommand AdminOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    Window CurrentWindow = obj as Window;
                    var AdminWnd = new AdminWindow();
                    AdminWnd.Show();
                    StartWind.Close();
                });
            }
        }
    }
}
