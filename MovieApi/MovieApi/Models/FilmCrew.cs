using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class FilmCrew
{
    public int FcrId { get; set; }

    public int FcrFilm { get; set; }

    public int FcrPerson { get; set; }

    public int FcrRole { get; set; }

    public virtual Film FcrFilmNavigation { get; set; } = null!;

    public virtual Person FcrPersonNavigation { get; set; } = null!;

    public virtual CrewRole FcrRoleNavigation { get; set; } = null!;
}
