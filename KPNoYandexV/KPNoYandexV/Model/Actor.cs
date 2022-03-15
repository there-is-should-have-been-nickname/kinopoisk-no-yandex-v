using System;
using System.Collections.Generic;

#nullable disable

namespace KPNoYandexV.Model
{
    public partial class Actor
    {
        public Actor()
        {
            FilmsActors = new HashSet<FilmsActor>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirth { get; set; }
        public string Country { get; set; }
        public string FacePath { get; set; }

        public virtual ICollection<FilmsActor> FilmsActors { get; set; }
    }
}
