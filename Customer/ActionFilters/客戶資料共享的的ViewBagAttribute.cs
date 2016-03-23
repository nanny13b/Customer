using Customer.Models;
using System.Web.Mvc;


namespace Customer.Controllers
{
    public class 客戶資料共享的的ViewBagAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.Message = "Your application description page.";
            //ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value");
            filterContext.Controller.ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value");

            //if (!filterContext.HttpContext.Request.IsLocal)
            //{
            //    filterContext.Result = new RedirectResult("/");
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}