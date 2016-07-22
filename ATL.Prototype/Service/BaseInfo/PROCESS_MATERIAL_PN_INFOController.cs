using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PROCESS_MATERIAL_PN_INFOController : ApiController
    {
        BLL.BaseInfo.PROCESS_MATERIAL_PN_INFO _PROCESS_MATERIAL_PN_INFO = new BLL.BaseInfo.PROCESS_MATERIAL_PN_INFO();
        #region 查询

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetData()
        {
            return _PROCESS_MATERIAL_PN_INFO.GetData();
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_MATERIAL_PN_INFO.GetData(pageSize, pageNumber);
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _PROCESS_MATERIAL_PN_INFO.GetDataByProcessId(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string MATERIAL_TYPE_ID, string MATERIAL_PN_ID, string MATERIAL_PN_NAME, string MATERIAL_PN_DESC, string queryStr)
        {
            return _PROCESS_MATERIAL_PN_INFO.GetDataQuery(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, MATERIAL_CATEGORY_ID, MATERIAL_TYPE_ID, MATERIAL_PN_ID, MATERIAL_PN_NAME, MATERIAL_PN_DESC, queryStr);
        }

        public List<PROCESS_MATERIAL_PN_INFO_Entity> GetDataSearchById(string PROCESS_ID, string MATERIAL_PN_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _PROCESS_MATERIAL_PN_INFO.GetDataSearchById(PROCESS_ID, MATERIAL_PN_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            return _PROCESS_MATERIAL_PN_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            return _PROCESS_MATERIAL_PN_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_MATERIAL_PN_INFO_Entity entity)
        {
            return _PROCESS_MATERIAL_PN_INFO.PostDelete(entity);
        }

        #endregion



    }
}
