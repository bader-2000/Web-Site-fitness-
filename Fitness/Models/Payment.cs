using Fitness.Models;
using System.ComponentModel.DataAnnotations;



namespace Fitness.Models;
public partial class Payment
{
    public decimal Paymentid { get; set; }

    public decimal Amount { get; set; }

    public DateTime Paymentdate { get; set; }

    public long Cardnumber { get; set; }

    public string Cardholdername { get; set; } 

    [DataType(DataType.Date)]
    public DateTime Expirydate { get; set; }

    public decimal Profileid { get; set; }  // تأكد من وجود هذه الخاصية

    public virtual Profile ?Profile { get; set; }  // العلاقة مع Profile
}
