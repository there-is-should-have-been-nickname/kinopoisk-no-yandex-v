using System;
using System.Collections.Generic;

#nullable disable

namespace KPNoYandexV.Model
{
    public partial class Genre
    {
        public Genre()
        {
            FilmsGenres = new HashSet<FilmsGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FilmsGenre> FilmsGenres { get; set; }
    }
}
