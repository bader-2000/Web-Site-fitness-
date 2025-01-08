
using Fitness.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fitness.Models;

public partial class Profile
{
    public decimal Profileid { get; set; }

    public string? Name { get; set; }

    public string Username { get; set; } = null!;

    public string Userpassword { get; set; } = null!;

    public string? Photo { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Lname { get; set; }

    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

    public virtual ICollection<Infofitness> Infofitnesses { get; set; } = new List<Infofitness>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<Typeperson> Typepeople { get; set; } = new List<Typeperson>();
}
