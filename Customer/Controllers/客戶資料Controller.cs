using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
using PagedList;
using Customer.App_Code;

namespace Customer.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        // GET: 客戶資料
        //public ActionResult Index()
        //{
        //    ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類)), "Key", "Value");
        //    return View(CustRepo.All());
        //}

        [HttpPost]
        public ActionResult 依客戶名稱或類別搜尋(string 關鍵字, string 客戶類別)
        {
            ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value");
            if (string.IsNullOrEmpty(關鍵字) && string.IsNullOrEmpty(客戶類別))
            {
                return View("Index", new List<客戶資料>());
            }
            else
            {
                var data = CustRepo.All(關鍵字, 客戶類別);
                //Cindy: 這邊要注意要寫清楚丟到哪個View，例如"Index"，否則會跳出錯誤，導到錯誤的網址，也就是找不到網址
                return View("Index", data.ToList());
            }
        }

        #region 關鍵字
        #region MyOldCode
        //[HttpPost]
        //public ActionResult 依客戶名稱搜尋(string 關鍵字)
        //{
        //    if (!string.IsNullOrEmpty(關鍵字.Trim()))
        //    {
        //        var data = CustRepo.All().Where(c => c.客戶名稱.Contains(關鍵字));
        //        //return View("Index", result);
        //        //Cindy: 這邊要注意要寫清楚丟到哪個View，例如"Index"，否則會跳出錯誤，導到錯誤的網址，也就是找不到網址
        //        return View("Index", data.ToList());
        //    }
        //    else
        //    {
        //        return View("Index", new List<客戶資料>());
        //    }
        //} 
        #endregion

        #region 老師的SampleCode
        // GET: 客戶資料
        //public ActionResult Index(string keyword)
        //{
        //    var data = db.客戶資料.Where(p => false == p.是否已刪除).AsQueryable();

        //    if (!String.IsNullOrEmpty(keyword))
        //    {
        //        data = data.Where(p => p.客戶名稱.Contains(keyword));
        //    }

        //    return View(data.ToList());
        //} 
        #endregion

        #endregion

        //可顯示單筆資料
        //有空來改一下，把關鍵字搜尋 一併放入
        //可以考慮不要放參數 用ModelBinding的方式來抓
        public ActionResult Index(int? id, string type, int pageno = 1)
        {
            ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value");
            if (id.HasValue)
            {
                ViewBag.SelectedID = id;
                ViewBag.type = type;
            }
            var data = CustRepo.All().OrderBy(c => c.Id).ToPagedList(pageno, 10);

            return View(data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = CustRepo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value");
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                CustRepo.Add(客戶資料);
                CustRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 cust = CustRepo.Find(id);
            cust.密碼 = CodeClass.DecryptDES(cust.密碼);

            if (cust == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value", cust.客戶分類);
            return View(cust);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        public ActionResult Edit(int id, 客戶資料 form)
        {
            客戶資料 cust = CustRepo.Find(id);
            try
            {
                if (TryUpdateModel(cust, new string[] { "Id,客戶名稱,統一編號,電話,傳真,地址,Email, 客戶分類,帳號,密碼" }))
                {                    
                    cust.密碼 = CodeClass.EncryptDES(form.密碼);

                    CustRepo.UnitOfWork.Commit();
                    TempData["EditMessage"] = "客戶資料更新成功";
                    RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(客戶資料).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.客戶類別 = new SelectList(客戶分類EnumListHelper.GetEnumDescDictionary(typeof(客戶分類Enum)), "Key", "Value", cust.客戶分類);
            return View(cust);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = CustRepo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = CustRepo.Find(id);
            //CustRepo.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
            CustRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (客戶資料Entities)CustRepo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
