using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Feedback
{
    public int FbkUser { get; set; }

    public int FbkFilm { get; set; }

    public int FbkMark { get; set; }

    public string FbkText { get; set; } = null!;

    public virtual Film FbkFilmNavigation { get; set; } = null!;

    public virtual User FbkUserNavigation { get; set; } = null!;
}
