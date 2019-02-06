using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aes
{
    class Program
    {
        static void Main(string[] args)
        {
            string javaAESStr = @"k2xD9YF/nTz4M373+0lSWeMFNjLcV79jNTHugVCDPiBSwJkWGVaY8YXpTAm25UqLJAtPljeBdcrW
e3O1tyCXhilkP6mKqG5u3ANRktbpRFAaxhyd8hjXfvANmfM7HE / PkU4zdGfWJ + OrX8pzcNlCHFYc
+/ E9JIaSWG8p + ivRm4MWffSup7FZsgYPo7kQ9lJ / XkeXHVMauSLCrZ + bK9oRvV02hYES5uhp3cRY
01y3MKbDkp7mF8og3mFqNytTQengb + ecVdLfaPdCfInlAmr + P / 3XBBh5U6wHUqwDer / 671ie//Gu
        MBLAa7G//Al7MwrF";

            string str = Decrypt_AES(javaAESStr);

        }

        private static readonly String strAesKey = "P@ssw0rd2019";//加密所需32位密匙

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">要加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static String Encrypt_AES(String str)
        {
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(strAesKey);
            Byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }



        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">要解密字符串</param>
        /// <returns>返回解密后字符串</returns>
        public static String Decrypt_AES(String str)
        {
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(strAesKey);
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return System.Text.UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
