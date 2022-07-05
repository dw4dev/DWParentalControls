using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DWParentalControls.Security;

namespace DWParentalControls.Helpers
{
    public static class SecurityHelp
    {
        static TextCryptography _Text;
        static SecurityHelp()
        {
            _Text = new TextCryptography();
        }

        /// <summary>
        /// 文字加密解密
        /// </summary>
        public static TextCryptography Text
        {
            get { return _Text; }
        }
    }
}
