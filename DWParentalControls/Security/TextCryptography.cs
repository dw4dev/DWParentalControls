using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using DWParentalControls.Helpers;

namespace DWParentalControls.Security
{
    public class TextCryptography : ICryptography
    {
        AesCryptoServiceProvider aes;

        public TextCryptography()
        {
            aes = new AesCryptoServiceProvider();
        }

        public string Decrypt(byte[] enc)
        {
            string plaintext = "";

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(enc, 0, enc.Length);
                cs.FlushFinalBlock();
                plaintext = Encoding.UTF8.GetString(ms.ToArray());
            }

            return plaintext;
        }

        /// <summary>
        /// 從 Hex 字串解密
        /// </summary>
        /// <param name="encstr"></param>
        /// <returns></returns>
        public string DecryptFromString(string encstr)
        {
            byte[] enc = ConvertHelp.HexStringToBytes(encstr);
            return Decrypt(enc);
        }

        public byte[] Encrypt(object str)
        {
            byte[] plaintext = ConvertHelp.GetBytes(str.ToString());
            byte[] encrypt;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plaintext, 0, plaintext.Length);
                cs.FlushFinalBlock();
                encrypt = ms.ToArray();
            }

            return encrypt;
        }

        /// <summary>
        /// 回傳密文的HEX字串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncryptToString(object str)
        {
            byte[] enc = Encrypt(str.ToString());
            return ConvertHelp.BytesToHexString(enc);
        }

        /// <summary>
        /// 回傳密文的 Base64 字串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncryptToBase64String(string str)
        {
            byte[] enc = Encrypt(str);
            return ConvertHelp.BytesToBase64(enc);
        }

        /// <summary>
        /// 從 Base64 字串解密
        /// </summary>
        /// <param name="encstr"></param>
        /// <returns></returns>
        public string DecryptFromBase64String(string encstr)
        {
            byte[] enc = ConvertHelp.Base64ToBytes(encstr);
            return Decrypt(enc);
        }
    }
}
