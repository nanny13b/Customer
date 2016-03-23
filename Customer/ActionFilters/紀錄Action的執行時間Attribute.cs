using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Customer.Controllers
{
    public class 紀錄Action的執行時間Attribute : ActionFilterAttribute
    {
        private const string Key = "執行區間";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 紀錄開始時間
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            filterContext.Controller.ViewBag.執行開始 = stopWatch;
            Debug.WriteLine("執行開始 : " + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // 紀錄結束時間            
            var stopWatch = (Stopwatch)filterContext.Controller.ViewBag.執行開始;
            stopWatch.Stop();

            // 計算執行時間
            TimeSpan ts = new TimeSpan(stopWatch.ElapsedTicks);
            filterContext.Controller.ViewBag.執行時間 = ts.TotalSeconds;

            Debug.WriteLine("執行結束 : " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") +
                "。共花費: " + ts.TotalSeconds + "秒");
            Debug.WriteLine(ts);

            base.OnActionExecuted(filterContext);
        }
    }
}