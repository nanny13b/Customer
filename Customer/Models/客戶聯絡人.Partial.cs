namespace Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Customer.Models;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶聯絡人Repository contact = RepositoryHelper.Get客戶聯絡人Repository();

            if (this.Id == 0)
            {
                if (contact.Where(c => c.Email == this.Email && c.客戶Id == this.客戶Id).Any())
                {
                    yield return new ValidationResult("Email已存在", new string[] { "Email" });
                }
            }
            else
            {
                if (contact.Where(c => c.Id != this.Id && c.客戶Id != this.客戶Id && c.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email已存在", new string[] { "Email" });
                }
            }

            yield return ValidationResult.Success;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [手機格式驗證]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "手機格式必須為09xx-xxxxxx")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public bool 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }

        //public partial class EMailConfirm : IValidatableObject
        //{
        //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //    {

        //        //return !db.客戶聯絡人.Where(人 => 人.Email == email && 人.客戶Id == 客戶Id).Any();
        //    }
        //}

        /*
        public static bool HasRepeatEmail(string email, int 客戶Id)
        {
            客戶資料Entities db = new 客戶資料Entities();
            //Cindy: ToList 是把所有資料先撈回來，在做判斷，所以如果資料量很大 會全抓 //效能問題
            var data = db.客戶聯絡人.ToList()
                    .FindAll(人 => 人.Email == email && 人.客戶Id == 客戶Id);
            //沒有判斷到是否刪除

            return !db.客戶聯絡人.Where(人 => 人.Email == email && 人.客戶Id == 客戶Id).Any();


            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
    }
}


