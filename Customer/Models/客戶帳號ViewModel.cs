using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Customer.Models
{
    public class 客戶帳號ViewModel
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("登入帳號")]
        [Required(ErrorMessage = "請輸入登入帳號")]
        [StringLength(20, ErrorMessage = "登入帳號最多20個字")]
        public string 帳號 { get; set; }

        [DisplayName("登入密碼")]
        [Required(ErrorMessage = "請輸入登入密碼")]
        [DataType(DataType.Password)]
        public string 密碼 { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }
    }
}
