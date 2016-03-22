using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Customer.App_Code;

namespace Customer.Controllers
{
    public class MemberController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }        

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(客戶帳號ViewModel login)
        {
            LoginViewModel sample = new LoginViewModel();

            if (CheckLogin(login.帳號, login.密碼))
            {
                FormsAuthentication.RedirectFromLoginPage(login.帳號, false);

                return RedirectToAction("Index", "Home");
            }

            //沒有顯示
            ModelState.AddModelError("Password", "您輸入的帳號或密碼錯誤");

            return View();
        }

        private bool CheckLogin(string username, string password)
        {            
            var m = CustRepo.All().FirstOrDefault(c => c.帳號 == username);
            string pwd = CodeClass.DecryptDES(m.密碼);
            return (pwd == password);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}