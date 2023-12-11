using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandD;

public partial class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MovieId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string? Description { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? ReleaseDate { get; set; }

    [Required]
    public decimal Budget { get; set; }

    [StringLength(100)]
    public string? Genre { get; set; }

    public decimal? Collections { get; set; }

    [ForeignKey("DirectorId")]
    [Required]
    public int? DirectorId { get; set; }


    [InverseProperty("Movies")]
    public virtual Director? Director { get; set; }
}
