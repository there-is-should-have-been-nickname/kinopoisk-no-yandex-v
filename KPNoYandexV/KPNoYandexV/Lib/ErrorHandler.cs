using KPNoYandexV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KPNoYandexV.Lib
{
    public static class ErrorHandler
    {
        public static string GetFilmErrorMessage(string FilmName, string FilmYear, 
            string FilmCountry, string FilmRating)
        {
            if (string.IsNullOrWhiteSpace(FilmName))
            {
                return "Не задано название фильма";
            }
            else if (string.IsNullOrWhiteSpace(FilmYear))
            {
                return "Не задан год фильма";
            }
            else if (string.IsNullOrWhiteSpace(FilmCountry))
            {
                return "Не задана страна фильма";
            }
            else if (string.IsNullOrWhiteSpace(FilmRating))
            {
                return "Не задан рейтинг фильма";
            }
            return "";
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message); 
        }
    }
}
