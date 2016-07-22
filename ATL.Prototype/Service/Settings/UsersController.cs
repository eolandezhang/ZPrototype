using BLL.Settings;
using Model.Settings;
using System.Collections.Generic;
using System.Web.Http;

namespace Service.Settings
{
    public class UsersController : ApiController
    {
        Users _users = new Users();
                
        public List<UsersEntity> GetData()
        {
            return _users.GetData();
        }
        [HttpGet]
        public UsersEntity GetCurrentUser() {
            return _users.GetCurrentUser();
        } 

        public List<UsersEntity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _users.GetData(pageSize, pageNumber);
        }
        public List<UsersEntity> GetDataByUserNum(string DESCRIPTION, string queryStr)
        {
            return _users.GetDataByUserNum(DESCRIPTION, queryStr);
        }
        [HttpPost]
        public int Add(UsersEntity entity)
        {            
            return _users.Add(entity);
        }
        [HttpPost]
        public int Edit(UsersEntity entity)
        {
            return _users.Edit(entity);
        }
        [HttpPost]
        public int Edit_factory_id(UsersEntity entity)
        {            
            return _users.Edit_factory_id(entity);
        }
        [HttpGet]
        public int Delete(string userName)
        {
            return _users.Delete(userName);
        }

        [HttpGet]
        public List<UsersEntity> Search(string userName)
        {
            return _users.Search(userName);
        }

        #region 获取用户信息

        [HttpGet]
        public UsersEntity GetADUserInfo(string userName)
        {
            return _users.GetADUserInfo(userName);
        }
        

        #endregion

    }
}
