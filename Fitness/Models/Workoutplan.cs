using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Workoutplan
{
    public decimal Idwop { get; set; }

    public decimal? Numberofweek { get; set; }

    public string? Goals { get; set; }

    public string? Day1 { get; set; }

    public string? Day2 { get; set; }

    public string? Day3 { get; set; }

    public string? Day4 { get; set; }

    public string? Day5 { get; set; }

    public string? Day6 { get; set; }

    public string? Day7 { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
