using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class CUSTOMER_CODE_LISTController : ApiController
    {
        BLL.BaseInfo.CUSTOMER_CODE_LIST _CUSTOMER_CODE_LIST = new BLL.BaseInfo.CUSTOMER_CODE_LIST();
        #region 查询

        public List<CUSTOMER_CODE_LIST_Entity> GetData()
        {
            return _CUSTOMER_CODE_LIST.GetData();
        }

        public List<CUSTOMER_CODE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _CUSTOMER_CODE_LIST.GetData(pageSize, pageNumber);
        }

        public List<CUSTOMER_CODE_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _CUSTOMER_CODE_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public bool GetDataValidateId(string CUSTOMER_CODE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _CUSTOMER_CODE_LIST.GetDataValidateId(CUSTOMER_CODE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(CUSTOMER_CODE_LIST_Entity entity)
        {
            return _CUSTOMER_CODE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(CUSTOMER_CODE_LIST_Entity entity)
        {
            return _CUSTOMER_CODE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(CUSTOMER_CODE_LIST_Entity entity)
        {
            return _CUSTOMER_CODE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
