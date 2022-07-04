using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DWParentalControls.Helpers
{
    public static class ConvertHelper
    {
        /// <summary>
        /// 將 Hex 字串轉換回 byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hex)
        {
            if (Regex.IsMatch(hex, @"([G-Z\.\W])+"))
            {
                throw new Exception("不正確的 Hex 字串文字！");
            }
            if (hex.Length % 2 != 0)
            {
                throw new Exception("不正確的 Hex 字串長度！");
            }

            int ba_len = hex.Length;
            byte[] bytes = new byte[ba_len / 2];
            for (int i = 0; i < ba_len; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// 將 byte[] 直接轉換為 A-F0-9 的16進制字串文字
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        /// <summary>
        /// 將 byte[] 傳換為 Base64 的字串
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string BytesToBase64(byte[] ba)
        {
            return Convert.ToBase64String(ba);
        }

        /// <summary>
        /// 將 Base64 的字串傳換回 byte[] 
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(string base64)
        {
            if ((base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
            {
                return Convert.FromBase64String(base64);
            }
            else
            {
                throw new Exception("不正確的 Base64 字串文字！");
            }
        }

        public static string StringToRtfAscii950(string str)
        {
            string rst = "";
            byte[] ba = Encoding.GetEncoding(950).GetBytes(str);
            List<byte> cht = new List<byte>();
            for (int c = 0; c < ba.Length;)
            {
                byte b = ba[c];
                if (b >= 32 && b <= 126)
                {
                    if (cht.Count > 0)
                    {
                        var cb = BitConverter.ToString(cht.ToArray()).ToLower();
                        rst += @"\'" + cb.Replace("-", @"\'");
                        cht.Clear();
                    }
                    rst += (char)b;
                }
                else
                {
                    cht.Add(ba[c]);
                    c++;
                    cht.Add(ba[c]);
                }
                c++;
            }
            if (cht.Count > 0)
            {
                var cb = BitConverter.ToString(cht.ToArray()).ToLower();
                rst += @"\'" + cb.Replace("-", @"\'");
            }

            rst = rst.Replace("{", @"\{").Replace("}", @"\}");

            return rst;
        }

        public static string RtfAscii950ToString(string str)
        {
            string rst = "";
            string pstr = str.ToLower().Replace(@"\{", "{").Replace(@"\}", "}");
            List<byte> chtba = new List<byte>();

            for (int c = 0; c < pstr.Length;)
            {
                if (pstr[c] == '\\' && pstr[c + 1] == '\'')
                {
                    chtba.Add((byte)(
                        "0123456789abcdef".IndexOf(pstr[c + 2]) * 16 +
                        "0123456789abcdef".IndexOf(pstr[c + 3])
                        ));
                    c += 4;
                    continue;
                }
                else
                {
                    if (chtba.Count > 0)
                    {
                        rst += Encoding.GetEncoding(950).GetString(chtba.ToArray());
                        chtba.Clear();
                    }
                    rst += pstr[c];
                }
                c++;
            }
            if (chtba.Count > 0)
            {
                rst += Encoding.GetEncoding(950).GetString(chtba.ToArray());
            }

            return rst;
        }

        /// <summary>
        /// 將傳入值轉為文字字串後回傳其 byte[]
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] GetBytes(object val)
        {
            return Encoding.UTF8.GetBytes(val.ToString());
        }
    }
}
