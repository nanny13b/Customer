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
        //private 客戶資料Entities db = new 客戶資料Entities();
        

        // GET: 客戶聯絡人
        public ActionResult Index(string 職稱清單)
        {
            //ViewBag.職稱清單 = ContactRepo.取得職稱清單();
            ViewBag.職稱清單 = new SelectList(ContactRepo.取得職稱清單(), "Key", "Key");

            return View(ContactRepo.All(職稱清單).Include(客 => 客.客戶資料));
        }

        public ActionResult Index(int? 客戶id)
        {
            //ViewBag.職稱清單 = ContactRepo.取得職稱清單();          
            var data = 客戶id.HasValue ? ContactRepo.All().Where(c => c.客戶Id == 客戶id.Value): null;

            return View(data);
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
