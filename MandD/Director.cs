using System;
using System.Collections.Generic;

namespace MandD;

public partial class Director
{
    public int DirectorId { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
