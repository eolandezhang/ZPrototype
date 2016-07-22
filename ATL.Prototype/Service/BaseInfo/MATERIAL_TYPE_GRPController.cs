using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class MATERIAL_TYPE_GRPController : ApiController
    {
        BLL.BaseInfo.MATERIAL_TYPE_GRP _MATERIAL_TYPE_GRP = new BLL.BaseInfo.MATERIAL_TYPE_GRP();
        #region 查询

        public List<MATERIAL_TYPE_GRP_Entity> GetData()
        {
            return _MATERIAL_TYPE_GRP.GetData();
        }

        public List<MATERIAL_TYPE_GRP_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_TYPE_GRP.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_TYPE_GRP_Entity> GetDataById(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_TYPE_GRP.GetDataById(MATERIAL_TYPE_GRP_NUM, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public bool GetDataValidateId(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _MATERIAL_TYPE_GRP.GetDataValidateId(MATERIAL_TYPE_GRP_NUM, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_TYPE_GRP_Entity entity)
        {
            return _MATERIAL_TYPE_GRP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_TYPE_GRP_Entity entity)
        {
            return _MATERIAL_TYPE_GRP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_TYPE_GRP_Entity entity)
        {
            return _MATERIAL_TYPE_GRP.PostDelete(entity);
        }

        #endregion



    }
}
