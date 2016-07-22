using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Settings
{
    public class PMES_TASK_LIST
    {
        DAL.Settings.PMES_TASK_LIST _PMES_TASK_LIST = new DAL.Settings.PMES_TASK_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PMES_TASK_LIST_Entity> GetData()
        {
            return _PMES_TASK_LIST.GetData();
        }

        public List<PMES_TASK_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PMES_TASK_LIST.GetData(pageSize, pageNumber);
        }

        public List<PMES_TASK_LIST_Entity> GetDataById(string PMES_TASK_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_TASK_LIST.GetDataById(PMES_TASK_ID, FACTORY_ID, queryStr);
        }
        public List<PMES_TASK_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _PMES_TASK_LIST.GetDataByFactoryId(FACTORY_ID, queryStr);
        }
        public bool GetDataValidateId(string PMES_TASK_ID, string FACTORY_ID)
        {
            return _PMES_TASK_LIST.GetDataValidateId(PMES_TASK_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_TASK_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_TASK_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_TASK_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PMES_TASK_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_TASK_LIST_Entity entity)
        {
            #region 组功能
            new DAL.Settings.PMES_USER_GRP_TASK_INFO().DeleteByTaskId(entity.PMES_TASK_ID, entity.FACTORY_ID);
            #endregion
            #region 功能用户
            new DAL.Settings.PMES_USER_TASK_INFO().DeleteByTaskId(entity.PMES_TASK_ID, entity.FACTORY_ID);
            #endregion
            return _PMES_TASK_LIST.PostDelete(entity);
        }

        #endregion



    }
}
