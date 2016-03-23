using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Customer.Models;

namespace Customer.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {        
        //有空來改一下，把關鍵字搜尋 一併放入
        //可以考慮不要放參數 用ModelBinding的方式來抓
        // GET: 客戶聯絡人
        public ActionResult 依職稱顯示客戶聯絡人(string 職稱清單)
        {
            ViewBag.職稱清單 = new SelectList(ContactRepo.取得職稱清單(), "Key", "Key");
            var data = ContactRepo.All(職稱清單).Include(客 => 客.客戶資料);
            return View("Index", data);
        }

        //POST vs Get vs Binding
        //[ChildActionOnly]
        public ActionResult 根據客戶代號顯示聯絡人(int 客戶id, string type)
        {
            ViewBag.顯示方式 = type;
            ViewBag.職稱清單 = new SelectList(ContactRepo.取得職稱清單(), "Key", "Key");

            var data = ContactRepo.All().Where(c => c.客戶Id == 客戶id);

            return View("Index", data);
        }

        public ActionResult Index()
        {
            ViewBag.職稱清單 = new SelectList(ContactRepo.取得職稱清單(), "Key", "Key");
            var data = ContactRepo.All().Include(客 => 客.客戶資料);
            return View(data);            
        }

        [HttpPost]
        //客戶聯絡人批次更新ViewModel
        //批次更新最好另外拉出來成為一個Action，呼叫Action轉回View
        //可能要了解Action跟View之間的關係
        public ActionResult Index( IList<客戶聯絡人批次更新ViewModel> data)
        {
            ViewBag.職稱清單 = new SelectList(ContactRepo.取得職稱清單(), "Key", "Key");
            var test = ModelState.Values.ToArray();
            var CustID = 0;

            if (ModelState.IsValid)
            {
                try
                {                   
                    foreach (var item in data)
                    {
                        var c = ContactRepo.Find(item.Id);
                        //職稱、手機、電話
                        c.職稱 = item.職稱;
                        c.手機 = item.手機;
                        c.電話 = item.電話;
                        CustID = item.客戶Id;
                    }
                    ContactRepo.UnitOfWork.Commit();
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction("根據客戶代號顯示聯絡人", "客戶聯絡人", new { 客戶id = CustID, type = "ByCustID" });
                //return RedirectToAction("Index");
            }
            //ViewBag.顯示方式 = "ByCustID";

            //更新後，為什麼無法保留在原頁面 失去Partial的效果
            return View("Index", data);
        }


        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //注意錯誤該怎麼return
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(CustRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            //Cindy: 如何在前端就跳出訊息: "有重複之信箱"，只能用Jquery嗎?
            //if (ModelState.IsValid && !客戶聯絡人MetaData.HasRepeatEmail(客戶聯絡人.Email, 客戶聯絡人.客戶Id))錯誤版，應該用Validation
            if (ModelState.IsValid)
            {
                ContactRepo.Add(客戶聯絡人);
                ContactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(CustRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(CustRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        public ActionResult Edit(int id, FormCollection form)
        {
            客戶聯絡人 c = ContactRepo.Find(id);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(客戶聯絡人).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            if (TryUpdateModel<客戶聯絡人>(c, new string[] { "Id,客戶Id,職稱,姓名,Email,手機,電話" }))
            {
                //延遲驗證：Action開始後，這邊才開始做model binding ，可以額外加參數 ex: Include Property。可控性比較高
            }
            ViewBag.客戶Id = new SelectList(CustRepo.All(), "Id", "客戶名稱", c.客戶Id);
            return RedirectToAction("Index");
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id);
            //ContactRepo.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;
            ContactRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (客戶資料Entities)ContactRepo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
