using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class ILLUSTRATION_LISTController : ApiController
    {
        BLL.BaseInfo.ILLUSTRATION_LIST _ILLUSTRATION_LIST = new BLL.BaseInfo.ILLUSTRATION_LIST();
        #region 查询

        public List<ILLUSTRATION_LIST_Entity> GetData()
        {
            return _ILLUSTRATION_LIST.GetData();
        }

        public List<ILLUSTRATION_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _ILLUSTRATION_LIST.GetData(pageSize, pageNumber);
        }

        public List<ILLUSTRATION_LIST_Entity> GetDataByFactoryIdAndTypeAndProcessId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID,string PROCESS_ID)
        {
            return _ILLUSTRATION_LIST.GetDataByFactoryIdAndTypeAndProcessId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID,PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(ILLUSTRATION_LIST_Entity entity)
        {
            return _ILLUSTRATION_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(ILLUSTRATION_LIST_Entity entity)
        {
            return _ILLUSTRATION_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(ILLUSTRATION_LIST_Entity entity)
        {
            return _ILLUSTRATION_LIST.PostDelete(entity);
        }

        #endregion



    }
}
