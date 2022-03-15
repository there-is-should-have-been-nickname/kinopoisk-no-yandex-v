using System;
using System.Collections.Generic;


namespace KPNoYandexV.Model
{
    public partial class Film
    {
        public Film()
        {
            FilmsActors = new HashSet<FilmsActor>();
            FilmsGenres = new HashSet<FilmsGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Year { get; set; }
        public string Country { get; set; }
        public double? Rating { get; set; }
        public int? ReviewsNumber { get; set; }
        public string PosterPath { get; set; }

        public virtual ICollection<FilmsActor> FilmsActors { get; set; }
        public virtual ICollection<FilmsGenre> FilmsGenres { get; set; }
    }
}
