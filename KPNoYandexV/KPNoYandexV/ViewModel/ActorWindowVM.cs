using KPNoYandexV.Data;
using KPNoYandexV.Model;
using KPNoYandexV.View;
using KPNoYandexV.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPNoYandexV.ViewModel
{
    public class ActorWindowVM : BaseVM
    {
        private Actor actor = new Actor();
        private string fullName;
        private string dateBirth;
        private string path;
        private List<Film> films = new List<Film>();

        public Actor CurrentActor { get { return actor; } set { actor = value; OnPropertyChanged(); } }
        public string FullName { get { return fullName; } set { fullName = value; OnPropertyChanged(); } }
        public string DateBirth { get { return dateBirth; } set { dateBirth = value; OnPropertyChanged(); } }
        public string Path { get { return path; } set { path = value; OnPropertyChanged(); } }
        public List<Film> CurrentFilms { get { return films; } set { films = value; OnPropertyChanged(); } }

        public ActorWindowVM(int Id)
        {
            using (KPNoYandexVContext context = new KPNoYandexVContext())
            {
                CurrentActor = context.Actors.SingleOrDefault(act => act.Id == Id);
                List<FilmsActor> FilmActors = context.FilmsActors.Where(filmact => filmact.ActorId == CurrentActor.Id).ToList();
                foreach (var FilmActor in FilmActors)
                {
                    Film Fil = context.Films.SingleOrDefault(fil => fil.Id == FilmActor.FilmId);
                    CurrentFilms.Add(Fil);
                }

            }
            DateTime Date = Convert.ToDateTime(CurrentActor.DateBirth.ToString());
            DateBirth = "День рождение: " +  Date.Day.ToString() + "." + Date.Month.ToString() + "." + Date.Year.ToString();
            FullName = CurrentActor.FirstName + " " + CurrentActor.LastName;
            Path = $"C:\\Users\\ACER\\Desktop\\Projects\\kinopoisk-no-yandex-v\\KPNoYandexV\\KPNoYandexV\\Images\\Faces\\{CurrentActor.FacePath}";
        }

        public BaseCommand FilmOpenClick
        {
            get
            {
                return new BaseCommand((obj) =>
                {
                    int Id = Convert.ToInt32(obj);
                    var FilmPage = new FilmWindow(Id);
                    FilmPage.Show();
                });
            }
        }
    }
}
