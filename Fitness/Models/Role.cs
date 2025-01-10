using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Role
{
    public decimal Roleid { get; set; }

    public string Rname { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
