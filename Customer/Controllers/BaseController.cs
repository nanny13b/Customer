using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class BaseController : Controller
    {
        public 客戶資料Repository CustRepo = RepositoryHelper.Get客戶資料Repository();
        public 客戶聯絡人Repository ContactRepo = RepositoryHelper.Get客戶聯絡人Repository();        
        public 客戶銀行資訊Repository BankRepo = RepositoryHelper.Get客戶銀行資訊Repository();

        // GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}