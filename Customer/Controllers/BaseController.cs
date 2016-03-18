using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public abstract class BaseController : Controller
    {
        protected 客戶資料Repository CustRepo = RepositoryHelper.Get客戶資料Repository();
        protected 客戶聯絡人Repository ContactRepo = RepositoryHelper.Get客戶聯絡人Repository();
        protected 客戶銀行資訊Repository BankRepo = RepositoryHelper.Get客戶銀行資訊Repository();

        protected override void HandleUnknownAction(string actionName)
        {
            this.Redirect("/").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
        }

        public ActionResult Debug()
        {
            return Content("DEBUG");
        }
        // GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}