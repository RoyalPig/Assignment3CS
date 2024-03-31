using System;
using System.Collections.Generic;

namespace PROG2500_A3_LINQ.Models;

public partial class DiscontinuedArtist
{
    public int DiscArtistId { get; set; }

    public int OriginalArtistId { get; set; }

    public string? ArtistName { get; set; }

    public DateTime DiscontinuedDate { get; set; }
}
