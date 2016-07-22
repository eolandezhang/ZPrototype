using Model.Settings;
using BLL.Settings;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Settings
{
    public class PMES_USER_TASK_INFOController : ApiController
    {
        BLL.Settings.PMES_USER_TASK_INFO _PMES_USER_TASK_INFO = new BLL.Settings.PMES_USER_TASK_INFO();
        #region 查询

        public List<PMES_USER_TASK_INFO_Entity> GetData()
        {
            return _PMES_USER_TASK_INFO.GetData();
        }

        public List<PMES_USER_TASK_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PMES_USER_TASK_INFO.GetData(pageSize, pageNumber);
        }

        public List<PMES_USER_TASK_INFO_Entity> GetDataById(string PMES_USER_ID, string PMES_TASK_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_TASK_INFO.GetDataById(PMES_USER_ID, PMES_TASK_ID, FACTORY_ID, queryStr);
        }
        public List<PMES_USER_TASK_INFO_Entity> GetDataByTaskId(string PMES_TASK_ID, string FACTORY_ID, string queryStr)
        {
            return _PMES_USER_TASK_INFO.GetDataByTaskId(PMES_TASK_ID, FACTORY_ID, queryStr);
        }
        public bool GetDataValidateId(string PMES_USER_ID, string PMES_TASK_ID, string FACTORY_ID)
        {
            return _PMES_USER_TASK_INFO.GetDataValidateId(PMES_USER_ID, PMES_TASK_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PMES_USER_TASK_INFO_Entity entity)
        {
            return _PMES_USER_TASK_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_USER_TASK_INFO_Entity entity)
        {
            return _PMES_USER_TASK_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_USER_TASK_INFO_Entity entity)
        {
            return _PMES_USER_TASK_INFO.PostDelete(entity);
        }

        #endregion



    }
}
