﻿using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Country
{
    public int CtrId { get; set; }

    public string CtrName { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
