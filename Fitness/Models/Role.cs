using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Role
{
    public decimal Roleid { get; set; }

    public string Rname { get; set; } = null!;

    public decimal? Rprofileid { get; set; }

    public virtual Profile? Rprofile { get; set; }
}
