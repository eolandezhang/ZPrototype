using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PARAMETER_LISTController : ApiController
    {
        BLL.BaseInfo.PARAMETER_LIST _PARAMETER_LIST = new BLL.BaseInfo.PARAMETER_LIST();
        #region 查询

        public List<PARAMETER_LIST_Entity> GetData()
        {
            return _PARAMETER_LIST.GetData();
        }

        public List<PARAMETER_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PARAMETER_LIST.GetData(pageSize, pageNumber);
        }

        public List<PARAMETER_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PARAMETER_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public List<PARAMETER_LIST_Entity> GetDataByPType(string PARAM_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _PARAMETER_LIST.GetDataByPType(PARAM_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<PARAMETER_LIST_Entity> GetDataById(string PARAMETER_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PARAMETER_LIST.GetDataById(PARAMETER_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PARAMETER_LIST_Entity entity)
        {
            return _PARAMETER_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PARAMETER_LIST_Entity entity)
        {
            return _PARAMETER_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PARAMETER_LIST_Entity entity)
        {
            return _PARAMETER_LIST.PostDelete(entity);
        }

        #endregion



    }
}
