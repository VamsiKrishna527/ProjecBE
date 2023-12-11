using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandD;

public partial class Director
{
    [Key]
    public int DirectorId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public int Age { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Nationality { get; set; } = null!;

    [InverseProperty("Director")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}

