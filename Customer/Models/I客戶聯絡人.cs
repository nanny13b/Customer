using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public interface I客戶聯絡人
    {
        int Id { get; set; }
        int 客戶Id { get; set; }
        string 職稱 { get; set; }
        string 電話 { get; set; }
        string 手機 { get; set; }

        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}