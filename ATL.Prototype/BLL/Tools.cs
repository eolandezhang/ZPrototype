using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace BLL
{
    public class Tools
    {
        static readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        public static string GetADAccount()
        {
            var ntAccount = String.Empty;

            //ATL-BM\USERNAME
            var userNameArray = HttpContext.Current.User.Identity.Name.Split('\\');
            var length = userNameArray.Length;

            if (length > 0)
            {
                ntAccount = userNameArray[length - 1];
            }

            return ntAccount.ToLower();
        }

        #region 分组编号
        //将数字转换成字母
        public static string Num2Alpha(int index)
        {
            if (index < 26)
            {
                return ((char)('A' + index)).ToString(CultureInfo.InvariantCulture);
            }
            return Num2Alpha(index / 26 - 1) + Num2Alpha(index % 26);
        }
        //将字母转换为数字
        public static int Alpha2Num(string str)
        {
            var l = str.Length;
            var n = 0;
            for (var i = 0; i < l; i++)
            {
                n = n * 26 + Convert.ToChar(str[i]) - 0x40;
            }
            return n - 1;
        }
        #endregion

        public static string GetUserFactoryId()
        {
            var username = Settings.Users.GetADAccount();
            var factoryId = _Users.GetData().First(x => x.USERNAME == username).FACTORY_ID;
            return factoryId;
        }
    }
}
