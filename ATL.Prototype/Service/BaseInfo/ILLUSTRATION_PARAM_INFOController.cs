using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class ILLUSTRATION_PARAM_INFOController : ApiController
    {
        BLL.BaseInfo.ILLUSTRATION_PARAM_INFO _ILLUSTRATION_PARAM_INFO = new BLL.BaseInfo.ILLUSTRATION_PARAM_INFO();
        #region 查询

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetData()
        {
            return _ILLUSTRATION_PARAM_INFO.GetData();
        }

        public List<ILLUSTRATION_PARAM_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _ILLUSTRATION_PARAM_INFO.GetData(pageSize, pageNumber);
        }
        public List<ILLUSTRATION_PARAM_INFO_Entity> GetDataByImgId(string ILLUSTRATION_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _ILLUSTRATION_PARAM_INFO.GetDataByImgId(ILLUSTRATION_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }
        #endregion

        #region 新增

        public int PostAdd(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            return _ILLUSTRATION_PARAM_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            return _ILLUSTRATION_PARAM_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(ILLUSTRATION_PARAM_INFO_Entity entity)
        {
            return _ILLUSTRATION_PARAM_INFO.PostDelete(entity);
        }

        #endregion



    }
}
