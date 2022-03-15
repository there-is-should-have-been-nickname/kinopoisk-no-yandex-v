using KPNoYandexV.Model;
using System;
using System.Collections.Generic;

#nullable disable

namespace KPNoYandexV.Model
{
    public partial class FilmsActor
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Film Film { get; set; }
    }
}
