using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Settings
{
    public class PMES_USER_GROUP_LIST
    {
        readonly DAL.Settings.PMES_USER_GROUP_LIST _PMES_USER_GROUP_LIST = new DAL.Settings.PMES_USER_GROUP_LIST();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PMES_USER_GROUP_LIST_Entity> GetData()
        {
            return _PMES_USER_GROUP_LIST.GetData();
        }

        public List<PMES_USER_GROUP_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
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
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_USER_GROUP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_USER_GROUP_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_USER_GROUP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_USER_GROUP_LIST_Entity entity)
        {
            #region 组功能
            new DAL.Settings.PMES_USER_GRP_TASK_INFO().DeleteByGrpId(entity.PMES_USER_GROUP_ID, entity.FACTORY_ID);
            #endregion

            #region 组用户
            new DAL.Settings.PMES_USER_GROUP_INFO().DeleteByGrpId(entity.PMES_USER_GROUP_ID, entity.FACTORY_ID);
            #endregion

            return _PMES_USER_GROUP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
