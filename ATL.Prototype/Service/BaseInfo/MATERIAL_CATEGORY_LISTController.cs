using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class MATERIAL_CATEGORY_LISTController : ApiController
    {
        BLL.BaseInfo.MATERIAL_CATEGORY_LIST _MATERIAL_CATEGORY_LIST = new BLL.BaseInfo.MATERIAL_CATEGORY_LIST();
        #region 查询

        public List<MATERIAL_CATEGORY_LIST_Entity> GetData()
        {
            return _MATERIAL_CATEGORY_LIST.GetData();
        }

        public List<MATERIAL_CATEGORY_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_CATEGORY_LIST.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_CATEGORY_LIST_Entity> GetDataByFactoryIdAndTypeId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_CATEGORY_LIST.GetDataByFactoryIdAndTypeId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            return _MATERIAL_CATEGORY_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            return _MATERIAL_CATEGORY_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            return _MATERIAL_CATEGORY_LIST.PostDelete(entity);
        }

        #endregion



    }
}
