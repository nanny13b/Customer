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
    public partial class 客戶聯絡人
    {
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
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public bool 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }

        public static bool HasRepeatEmail(string email, int 客戶Id)
        {
            客戶資料Entities db = new 客戶資料Entities();
            var data = db.客戶聯絡人.ToList()
                    .FindAll(人 => 人.Email == email && 人.客戶Id == 客戶Id);

            if (data.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


