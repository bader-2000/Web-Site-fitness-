using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Typeperson
{
    public decimal? Tprofileid { get; set; }

    public decimal? Tsubscrid { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public string? Status { get; set; }

    public decimal Id { get; set; }

    public virtual Profile? Tprofile { get; set; }

    public virtual Subscription? Tsubscr { get; set; }
}
