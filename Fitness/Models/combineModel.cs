using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models;

public class CombinedViewModel
{
    public List<Profile> Profile { get; set; }
    public Testimonial Testimonial { get; set; }
    public Subscription Subscription { get; set; }
    public Workoutplan WorkoutPlan { get; set; }
    public Payment Payment { get; set; }
    public Typeperson Typeperson { get; set; }
	public Infofitness Infofitness { get; set; }
}
