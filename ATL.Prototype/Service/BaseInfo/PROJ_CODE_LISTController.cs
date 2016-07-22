using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PROJ_CODE_LISTController : ApiController
    {
        BLL.BaseInfo.PROJ_CODE_LIST _PROJ_CODE_LIST = new BLL.BaseInfo.PROJ_CODE_LIST();
        #region 查询

        public List<PROJ_CODE_LIST_Entity> GetData()
        {
            return _PROJ_CODE_LIST.GetData();
        }

        public List<PROJ_CODE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PROJ_CODE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PROJ_CODE_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PROJ_CODE_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public bool GetDataValidateId(string PROJ_CODE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PROJ_CODE_LIST.GetDataValidateId(PROJ_CODE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PROJ_CODE_LIST_Entity entity)
        {
            return _PROJ_CODE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROJ_CODE_LIST_Entity entity)
        {
            return _PROJ_CODE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROJ_CODE_LIST_Entity entity)
        {
            return _PROJ_CODE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
