using System;
using System.Collections.Generic;

namespace MandD;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public decimal Budget { get; set; }

    public string? Genre { get; set; }

    public decimal? Collections { get; set; }

    public int? DirectorId { get; set; }

    public virtual Director? Director { get; set; }
}
