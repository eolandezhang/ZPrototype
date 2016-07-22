using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PRODUCT_PROC_TYPE_LISTController : ApiController
    {
        BLL.BaseInfo.PRODUCT_PROC_TYPE_LIST _PRODUCT_PROC_TYPE_LIST = new BLL.BaseInfo.PRODUCT_PROC_TYPE_LIST();
        #region 查询

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetData()
        {
            return _PRODUCT_PROC_TYPE_LIST.GetData();
        }

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PRODUCT_PROC_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PRODUCT_PROC_TYPE_LIST_Entity> GetDataByProductTypeId(string PRODUCT_TYPE_ID, string FACTORY_ID)
        {
            return _PRODUCT_PROC_TYPE_LIST.GetDataByProductTypeId(PRODUCT_TYPE_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_PROC_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_PROC_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PRODUCT_PROC_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_PROC_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
