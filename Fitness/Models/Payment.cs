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

//public partial class Payment
//{
//    public decimal PaymentId { get; set; }  // يمكن أن يكون رقمًا عشريًا في C# ولكن يفضل أن يكون نوعه INT إذا كان ID فقط

//    public decimal Amount { get; set; }  // المبلغ المدفوع (DECIMAL(18, 2))

//    [DataType(DataType.Date)]  // تحديد تنسيق التاريخ
//    public DateTime PaymentDate { get; set; }  // تاريخ الدفع (DATE)

//    public long CardNumber { get; set; }  // رقم البطاقة (NUMBER(16))

//    public string CardHolderName { get; set; } = null!;  // اسم صاحب البطاقة

//    [DataType(DataType.Date)]  // تحديد تنسيق التاريخ
//    public DateTime ExpiryDate { get; set; }  // تاريخ انتهاء البطاقة (DATE)

//    public decimal ProfileId { get; set; }  // ProfileId هو من نوع DECIMAL في C# لأنك لا ترغب في تحويله إلى INT

//    public virtual Profile Profile { get; set; } = null!;  // الربط مع Profile
//}
