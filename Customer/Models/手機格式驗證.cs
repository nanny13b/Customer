using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Customer.Models
{
    public class 手機格式驗證Attribute : DataTypeAttribute
    {
        private static Regex _Regex = new Regex(@"\d{4}-\d{6}");
        public 手機格式驗證Attribute() : base(DataType.Text)
        {
            ErrorMessage = "手機格式不符，請重新輸入";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                string valueStr = value as string;
                return valueStr != null && _Regex.Match(valueStr).Length > 0;
            }
        }
    }
}