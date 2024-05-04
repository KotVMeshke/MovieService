using System;
using System.Collections.Generic;

namespace MovieApi.Models;

public partial class Photo
{
    public int PhId { get; set; }

    public int PhPerson { get; set; }

    public string PhPath { get; set; } = null!;

    public virtual Person PhPersonNavigation { get; set; } = null!;
}
