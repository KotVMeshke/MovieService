using System;
using System.Collections.Generic;

namespace MovieApi;

public partial class MediaPoster
{
    public int MpPoster { get; set; }

    public int MpMedia { get; set; }

    public virtual Medium MpMediaNavigation { get; set; } = null!;
}
