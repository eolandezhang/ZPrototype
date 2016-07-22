using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Settings
{
    public class PMES_USER_GRP_TASK_INFO
    {
        DAL.Settings.PMES_USER_GRP_TASK_INFO _PMES_USER_GRP_TASK_INFO = new DAL.Settings.PMES_USER_GRP_TASK_INFO();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PMES_USER_GRP_TASK_INFO_Entity> GetData()
        {
            return _PMES_USER_GRP_TASK_INFO.GetData();
        }

        public List<PMES_USER_GRP_TASK_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PMES_USER_GRP_TASK_INFO.GetData(pageSize, pageNumber);
        }

        public List<PMES_USER_GRP_TASK_INFO_Entity> GetDataById(string PMES_USER_GROUP_ID, string PMES_TASK_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_GRP_TASK_INFO.GetDataById(PMES_USER_GROUP_ID, PMES_TASK_ID, FACTORY_ID, queryStr);
        }
        public List<PMES_USER_GRP_TASK_INFO_Entity> GetDataByGroupId(string PMES_USER_GROUP_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_GRP_TASK_INFO.GetDataByGroupId(PMES_USER_GROUP_ID, FACTORY_ID, queryStr);
        }
        public bool GetDataValidateId(string PMES_USER_GROUP_ID, string PMES_TASK_ID, string FACTORY_ID)
        {
            return _PMES_USER_GRP_TASK_INFO.GetDataValidateId(PMES_USER_GROUP_ID, PMES_TASK_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_USER_GRP_TASK_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_USER_GRP_TASK_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_USER_GRP_TASK_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_USER_GRP_TASK_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_USER_GRP_TASK_INFO_Entity entity)
        {
            return _PMES_USER_GRP_TASK_INFO.PostDelete(entity);
        }

        #endregion



    }
}
