using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PRODUCT_TYPE_LISTController : ApiController
    {
        BLL.BaseInfo.PRODUCT_TYPE_LIST _PRODUCT_TYPE_LIST = new BLL.BaseInfo.PRODUCT_TYPE_LIST();
        #region 查询

        public List<PRODUCT_TYPE_LIST_Entity> GetData()
        {
            return _PRODUCT_TYPE_LIST.GetData();
        }

        public List<PRODUCT_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PRODUCT_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PRODUCT_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _PRODUCT_TYPE_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PRODUCT_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PRODUCT_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PRODUCT_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
