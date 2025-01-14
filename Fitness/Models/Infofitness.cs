using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models;

public partial class Infofitness
{
    public decimal Idif { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Location { get; set; }

    public string? Aboutus { get; set; }

    public string? Facebook { get; set; }

    public string? Linkedin { get; set; }

    public string? Photoaboutus { get; set; }

    [NotMapped]
    public virtual  IFormFile ImageFileaboutus { get; set; }

   
    public decimal? Inprofileid { get; set; }

    public virtual Profile Inprofile { get; set; }
}
