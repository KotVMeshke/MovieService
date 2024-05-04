using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Genre
{
    public int GnrId { get; set; }

    public string GnrName { get; set; } = null!;

    public virtual ICollection<Film> FgFilms { get; set; } = new List<Film>();
}
