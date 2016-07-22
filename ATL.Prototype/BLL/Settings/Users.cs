using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BLL.Settings
{
    public class Users
    {
        DAL.Settings.Users _users = new DAL.Settings.Users();

        public List<UsersEntity> GetData()
        {
            return _users.GetData();
        }

        public List<UsersEntity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _users.GetData(pageSize, pageNumber);
        }

        public UsersEntity GetData(string userName)
        {
            var user = GetData().Where(x => x.USERNAME.ToUpper() == userName.ToUpper()).ToList();
            if (user.Any())
            {
                return user.First();
            }
            return null;
        }
        public List<UsersEntity> GetDataByUserNum(string DESCRIPTION, string queryStr)
        {
            return _users.GetDataByUserNum(DESCRIPTION, queryStr);
        }
        public UsersEntity GetCurrentUser()
        {
            return GetData(GetADAccount());
        }

        public int Add(UsersEntity entity)
        {
            entity.CREATEDBY = GetADAccount();
            entity.FACTORY_ID="SSL-P";
            return _users.Add(entity);
        }
        public int Edit(UsersEntity entity)
        {
            entity.MODIFIEDBY = GetADAccount();
            return _users.Edit(entity);
        }
        public int Edit_factory_id(UsersEntity entity)
        {
            entity.USERNAME = GetADAccount();
            return _users.Edit_factory_id(entity);
        }
        public int Delete(string userName)
        {
            return _users.Delete(userName);
        }

        public List<UsersEntity> Search(string userName)
        {
            var users = _users.GetData();
            if (string.IsNullOrEmpty(userName))
            {
                return users;
            }
            var user = (from u in users where (u.USERNAME).ToUpper().Contains(userName.ToUpper()) select u).ToList();
            return user;
        }






        #region AD用户信息

        public static string GetADAccount()
        {
            string ntAccount = String.Empty;
            //ATL-BM\USERNAME
            string[] userNameArray = HttpContext.Current.User.Identity.Name.Split('\\');
            int length = userNameArray.Length;
            if (length > 0)
            {
                ntAccount = userNameArray[length - 1];
            }
            return ntAccount.ToLower();
        }

        public UsersEntity GetADUserInfo(string userName)
        {
            UsersEntity user = new UsersEntity();
            QRSGetADUserInfo.Service GetUserInfo = new QRSGetADUserInfo.Service();
            try
            {
                var userInfo = GetUserInfo.GetUserInfoByConditon(userName, "ByAccount");
                user.CNNAME = string.IsNullOrEmpty(userInfo.CnName) ? "" : userInfo.CnName;
                user.USERNAME = userName;
                user.DEPARTMENT = string.IsNullOrEmpty(userInfo.Dept) ? "" : userInfo.Dept;
                user.TITLE = string.IsNullOrEmpty(userInfo.Title) ? "" : userInfo.Title;
                user.DESCRIPTION = string.IsNullOrEmpty(userInfo.StaffNo) ? "" : userInfo.StaffNo;
                user.MAIL = string.IsNullOrEmpty(userInfo.Mail) ? "" : userInfo.Mail;
                user.FACTORY_ID = "SSL-P";
                return user;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
