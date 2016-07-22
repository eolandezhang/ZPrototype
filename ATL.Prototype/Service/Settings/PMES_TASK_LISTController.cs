using Model.Settings;
using BLL.Settings;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Settings
{
    public class PMES_TASK_LISTController : ApiController
    {
        BLL.Settings.PMES_TASK_LIST _PMES_TASK_LIST = new BLL.Settings.PMES_TASK_LIST();
        #region 查询

        public List<PMES_TASK_LIST_Entity> GetData()
        {
            return _PMES_TASK_LIST.GetData();
        }

        public List<PMES_TASK_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
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
            return _PMES_TASK_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PMES_TASK_LIST_Entity entity)
        {
            return _PMES_TASK_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PMES_TASK_LIST_Entity entity)
        {
            return _PMES_TASK_LIST.PostDelete(entity);
        }

        #endregion



    }
}
