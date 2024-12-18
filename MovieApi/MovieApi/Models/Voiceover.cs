﻿using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Voiceover
{
    public int VoiId { get; set; }

    public string VoiFilepath { get; set; } = null!;

    public int VoiFilm { get; set; }

    public virtual Film VoiFilmNavigation { get; set; } = null!;
}
