namespace Customer.App_Code
{
    public class CodeClass
    {
        /// <summary> 
        /// DES 加密字串 
        /// </summary> 
        /// <span  name="original" class="mceItemParam"></span>原始字串</param> 
        /// <returns>string</returns>
        public static string EncryptDES(string original)
        {
            return EncyptDesDll.CodeClass.EncryptDES(original);
        }

        /// <summary> 
        /// DES 解密字串 
        /// </summary> 
        /// <span  name="hexString" class="mceItemParam"></span>加密後 Hex String</param> 
        /// <returns>string</returns>
        public static string DecryptDES(string hexString)
        {
            return EncyptDesDll.CodeClass.DecryptDES(hexString);
        }

        /// <summary> 
        /// 取得 MD5 編碼後的 Hex 字串   都在Class裡做
        /// 加密後為 32 Bytes Hex String (16 Byte) 
        /// </summary> 
        /// <span  name="toEncode">要編碼的字串。</param> 
        /// <returns>string</returns>
        public static string EncodeStringMD5(string toEncode)
        {
            return EncyptDesDll.CodeClass.EncodeStringMD5(toEncode);
        }
    }
}