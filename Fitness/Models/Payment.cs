using System;
using System.Collections.Generic;

namespace Fitness.Models;

public partial class Payment
{
    public decimal Paymentid { get; set; }

    public decimal Amount { get; set; }

    public DateTime Paymentdate { get; set; }

    public long Cardnumber { get; set; }

    public string Cardholdername { get; set; } = null!;

    public DateTime Expirydate { get; set; }

    public decimal Profileid { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
