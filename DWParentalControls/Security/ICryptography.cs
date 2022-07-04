using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWParentalControls.Security
{
    public interface ICryptography
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns>密文二進制</returns>
        byte[] Encrypt(object str);

        /// <summary>
        /// 加密 並將密文轉換為可見的文字(HEX字串)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>密文可視文字</returns>
        string EncryptToString(object str);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns>明文</returns>
        string Decrypt(byte[] enc);

        /// <summary>
        /// 解密 將可視密文(HEX字串)轉換回明文
        /// </summary>
        /// <param name="str"></param>
        /// <returns>明文</returns>
        string DecryptFromString(string encstr);
    }
}
