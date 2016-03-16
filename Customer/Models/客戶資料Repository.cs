using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Customer.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => c.是否已刪除 == false);
        }

        public IQueryable<客戶資料> All(string keyword, string CustType)
        {
            var data = this.All();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(c => c.客戶名稱.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(CustType) && CustType != "0")
            {
                data = data.Where(c => c.客戶分類 == CustType);
            }
            return data;
        }

        public IQueryable<客戶資料> All(bool isAll)
        {
            if (isAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public 客戶資料 Find(int? id)
        {
            return this.All().FirstOrDefault(c => c.Id == id && c.是否已刪除 == false);
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}