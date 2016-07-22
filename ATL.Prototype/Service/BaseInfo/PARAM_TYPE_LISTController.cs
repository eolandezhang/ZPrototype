using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PARAM_TYPE_LISTController : ApiController
    {
        BLL.BaseInfo.PARAM_TYPE_LIST _PARAM_TYPE_LIST = new BLL.BaseInfo.PARAM_TYPE_LIST();
        #region 查询

        public List<PARAM_TYPE_LIST_Entity> GetData()
        {
            return _PARAM_TYPE_LIST.GetData();
        }

        public List<PARAM_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PARAM_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PARAM_TYPE_LIST_Entity> GetDataType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _PARAM_TYPE_LIST.GetDataType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PARAM_TYPE_LIST_Entity entity)
        {
            return _PARAM_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PARAM_TYPE_LIST_Entity entity)
        {
            return _PARAM_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PARAM_TYPE_LIST_Entity entity)
        {
            return _PARAM_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
