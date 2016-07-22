using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PROCESS_PARAM_INFOController : ApiController
    {
        BLL.BaseInfo.PROCESS_PARAM_INFO _PROCESS_PARAM_INFO = new BLL.BaseInfo.PROCESS_PARAM_INFO();
        #region 查询

        public List<PROCESS_PARAM_INFO_Entity> GetData()
        {
            return _PROCESS_PARAM_INFO.GetData();
        }

        public List<PROCESS_PARAM_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_PARAM_INFO.GetData(pageSize, pageNumber);
        }

        public List<PROCESS_PARAM_INFO_Entity> GetData(string PROCESS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return _PROCESS_PARAM_INFO.GetData(PROCESS_ID, PARAMETER_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID);
        }

        public List<PROCESS_PARAM_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return _PROCESS_PARAM_INFO.GetDataByProcessId(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID);
        }
        public List<PROCESS_PARAM_INFO_Entity> GetDataByProcessIdQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _PROCESS_PARAM_INFO.GetDataByProcessIdQuery(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }

        public bool GetDataValidateId(string PROCESS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return _PROCESS_PARAM_INFO.GetDataValidateId(PROCESS_ID, PARAMETER_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_PARAM_INFO_Entity entity)
        {
            return _PROCESS_PARAM_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_PARAM_INFO_Entity entity)
        {
            return _PROCESS_PARAM_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_PARAM_INFO_Entity entity)
        {
            return _PROCESS_PARAM_INFO.PostDelete(entity);
        }

        #endregion



    }
}
