using System;
using System.Linq;
using System.Collections.Generic;

namespace Customer.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(c => c.是否已刪除 == false);
        }

        public IQueryable<客戶聯絡人> All(string keyword)
        {
            var data = this.All();
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.Where(c => c.職稱.Contains(keyword));
                }
            }
            return data;
        }

        public IQueryable<客戶聯絡人> All(bool isAll)
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

        public 客戶聯絡人 Find(int? id)
        {
            return this.All().FirstOrDefault(c => c.Id == id && c.是否已刪除 == false);
        }

        public IEnumerable<IGrouping<string, 客戶聯絡人>> 取得職稱清單() {
            //string[] emaillist = (from x in mb select x.email).ToArray() ;
            //IEnumerable<IGrouping<int, string>> query2 = pets.GroupBy(pet => pet.Age, pet => pet.Name);
            IEnumerable<IGrouping<string, 客戶聯絡人>> data = this.All().GroupBy(c => c.職稱).AsEnumerable();

            return data;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}