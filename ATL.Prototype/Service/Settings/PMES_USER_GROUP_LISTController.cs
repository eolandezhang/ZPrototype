using Model.Settings;
using BLL.Settings;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Settings
{
    public class PMES_USER_GROUP_LISTController : ApiController
    {
        BLL.Settings.PMES_USER_GROUP_LIST _PMES_USER_GROUP_LIST = new BLL.Settings.PMES_USER_GROUP_LIST();
        #region 查询

        public List<PMES_USER_GROUP_LIST_Entity> GetData()
        {
            return _PMES_USER_GROUP_LIST.GetData();
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PMES_USER_GROUP_LIST.GetData(pageSize, pageNumber);
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetDataById(string PMES_USER_GROUP_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_GROUP_LIST.GetDataById(PMES_USER_GROUP_ID, FACTORY_ID, queryStr);
        }
        public List<PMES_USER_GROUP_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_GROUP_LIST.GetDataByFactoryId(FACTORY_ID, queryStr);
        }

        public bool GetDataValidateId(string PMES_USER_GROUP_ID, string FACTORY_ID)
        {
            return _PMES_USER_GROUP_LIST.GetDataValidateId(PMES_USER_GROUP_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_USER_GROUP_LIST_Entity entity)
        {
            return _PMES_USER_GROUP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_USER_GROUP_LIST_Entity entity)
        {
            return _PMES_USER_GROUP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_USER_GROUP_LIST_Entity entity)
        {
            return _PMES_USER_GROUP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
