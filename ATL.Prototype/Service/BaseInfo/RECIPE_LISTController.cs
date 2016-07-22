using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class RECIPE_LISTController : ApiController
    {
        BLL.BaseInfo.RECIPE_LIST _RECIPE_LIST = new BLL.BaseInfo.RECIPE_LIST();
        #region 查询

        public List<RECIPE_LIST_Entity> GetData()
        {
            return _RECIPE_LIST.GetData();
        }

        public List<RECIPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _RECIPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<RECIPE_LIST_Entity> GetDataById(string RECIPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _RECIPE_LIST.GetDataById(RECIPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<RECIPE_LIST_Entity> GetDataByTypeId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string RECIPE_TYPE_ID, string queryStr)
        {
            return _RECIPE_LIST.GetDataByTypeId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, RECIPE_TYPE_ID, queryStr);
        }

        public List<RECIPE_LIST_Entity> GetDataQuery(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string RECIPE_TYPE_ID, string RECIPE_ID, string RECIPE_NAME, string RECIPE_DESC, string queryStr)
        {
            return _RECIPE_LIST.GetDataQuery(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, RECIPE_TYPE_ID, RECIPE_ID, RECIPE_NAME, RECIPE_DESC, queryStr);
        }

        public bool GetDataValidateId(string RECIPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _RECIPE_LIST.GetDataValidateId(RECIPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(RECIPE_LIST_Entity entity)
        {
            return _RECIPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(RECIPE_LIST_Entity entity)
        {
            return _RECIPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(RECIPE_LIST_Entity entity)
        {
            return _RECIPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
