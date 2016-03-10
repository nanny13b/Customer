using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class 客戶檢視表Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶檢視表
        public ActionResult Index()
        {
            //方法1
            #region View 的語法
            //SELECT 客戶資料.客戶名稱,  帳戶數 = ISNull(帳戶數, 0), 聯絡人數量 = ISNull(聯絡人數量, 0)
            //FROM 客戶資料
            //LEFT JOIN(SELECT 客戶Id, COUNT(Id) as 帳戶數 FROM 客戶銀行資訊
            //        WHERE 是否已刪除 = 0 GROUP BY 客戶Id) 客戶銀行資訊 ON 客戶資料.Id = 客戶銀行資訊.客戶Id
            //LEFT JOIN(SELECT 客戶Id, COUNT(Id) as 聯絡人數量 FROM 客戶聯絡人
            //        WHERE 是否已刪除 = 0 GROUP BY 客戶Id) 客戶聯絡人 ON 客戶資料.Id = 客戶聯絡人.客戶Id
            //WHERE 客戶資料.是否已刪除 = 0 
            #endregion
            return View(db.VW客戶檢視表.ToList());



        }
    }
}