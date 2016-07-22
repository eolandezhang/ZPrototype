using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class RECIPE_TYPE_LISTController : ApiController
    {
        BLL.BaseInfo.RECIPE_TYPE_LIST _RECIPE_TYPE_LIST = new BLL.BaseInfo.RECIPE_TYPE_LIST();
        #region 查询

        public List<RECIPE_TYPE_LIST_Entity> GetData()
        {
            return _RECIPE_TYPE_LIST.GetData();
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _RECIPE_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataById(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _RECIPE_TYPE_LIST.GetDataById(RECIPE_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _RECIPE_TYPE_LIST.GetDataByFactoryId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public bool GetDataValidateId(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _RECIPE_TYPE_LIST.GetDataValidateId(RECIPE_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(RECIPE_TYPE_LIST_Entity entity)
        {
            return _RECIPE_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(RECIPE_TYPE_LIST_Entity entity)
        {
            return _RECIPE_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(RECIPE_TYPE_LIST_Entity entity)
        {
            return _RECIPE_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
