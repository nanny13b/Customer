using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Customer.Models
{
    #region 客戶分類   
    [Flags]
    public enum 客戶分類 : int
    {
        [Description("全部")]
        All = 0,
        [Description("程式設計")]
        Programming = 1,
        [Description("電信業")]
        Telecom = 2,
        [Description("餐飲業")]
        Restaurant = 3
    }
    #endregion

    public static class 客戶分類EnumListHelper
    {
        //enum在執行階段是不會變動的，故使用Cache避免反覆執行        
        static Dictionary<Type, Dictionary<int, string>> _cache
            = new Dictionary<Type, Dictionary<int, string>>();

        //將enum的值及[Description("..")]轉成SortedList<int, string>
        public static Dictionary<int, string> GetEnumDescDictionary(Type enumType)
        {
            if (_cache.ContainsKey(enumType))
                return _cache[enumType];

            Dictionary<int, string> dict = new Dictionary<int, string>();
            //dict.Add(0, "請選擇");
            foreach (string name in Enum.GetNames(enumType))
            {
                FieldInfo fi = enumType.GetField(name);
                DescriptionAttribute[] attrs =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute), false);

                dict.Add((int)Enum.Parse(enumType, name), attrs.Length > 0 ? attrs[0].Description : name);
            }
            if (!_cache.ContainsKey(enumType))
            {
                _cache.Add(enumType, dict);
            }            

            return dict;
        }
        public static string GetDescription(客戶分類 value)
        {
            object[] attrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length == 0)
            {
                return value.ToString();
            }
            return ((DescriptionAttribute)attrs[0]).Description;
        }
    }

}