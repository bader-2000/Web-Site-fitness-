using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Testimonial
{
    public decimal Testimoid { get; set; }

    public string? Feedback { get; set; }

    public string? Status { get; set; }

    public decimal? Tprofileid { get; set; }

    public virtual Profile? Tprofile { get; set; }
}
