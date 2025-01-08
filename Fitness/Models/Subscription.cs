using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Subscription
{
    public decimal Subscrid { get; set; }

    public decimal? Countweeks { get; set; }

    public string? Nameplan { get; set; }

    public string? Price { get; set; }

    public decimal? Sidwop { get; set; }

    public virtual Workoutplan? SidwopNavigation { get; set; }

    public virtual ICollection<Typeperson> Typepeople { get; set; } = new List<Typeperson>();
}
