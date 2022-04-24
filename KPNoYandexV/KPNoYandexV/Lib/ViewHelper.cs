using KPNoYandexV.Model;
using KPNoYandexV.ViewModel.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KPNoYandexV.Lib
{
    public static class ViewHelper
    {
        public static void AddFilmName(Film CurrentFilm, List<Button> FilmNames, BaseCommand CurrentCommand)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            btn.Content = CurrentFilm.Name;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = CurrentCommand;
            btn.CommandParameter = CurrentFilm.Id;

            FilmNames.Add(btn);
        }

        public static void AddGenreName(Genre CurrentGenre, List<Button> GenreNames, BaseCommand CurrentCommand)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            btn.Content = CurrentGenre.Name;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = CurrentCommand;
            btn.CommandParameter = CurrentGenre.Id;

            GenreNames.Add(btn);
        }

        public static void AddActorName(Actor CurrentActor, List<Button> ActorNames, BaseCommand CurrentCommand)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            btn.Content = CurrentActor.LastName;
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = CurrentCommand;
            btn.CommandParameter = CurrentActor.Id;

            ActorNames.Add(btn);
        }

        public static void AddButtons<T>(T Elem, List<Button> CurrentList, BaseCommand CurrentCommand)
        {
            var btn = new Button();
            btn.Width = 90;
            btn.Height = 30;
            btn.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            btn.FontSize = 10;
            if (Elem is Actor)
            {
                btn.Content = $"{(Elem as Actor).FirstName} {(Elem as Actor).LastName}";
            }
            else if (Elem is Film)
            {
                btn.Content = (Elem as Film).Name;
            }
            else
            {
                btn.Content = (Elem as Genre).Name;
            }
            btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btn.Command = CurrentCommand;
            if (Elem is Actor)
            {
                btn.CommandParameter = (Elem as Actor).Id;
            }
            else if (Elem is Film)
            {
                btn.CommandParameter = (Elem as Film).Id;
            }
            else
            {
                btn.CommandParameter = (Elem as Genre).Id;
            }

            CurrentList.Add(btn);
        }

        public static void WindowInteract<T, K>(T WindowClose, K WindowShow)
            where T : Window
            where K : Window
        {
            (WindowShow as Window).Show();
            (WindowClose as Window).Close();
        }

        public static void WindowInteract<T>(T WindowClose)
        {
            (WindowClose as Window).Close();
        }
    }
}
