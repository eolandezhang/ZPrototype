using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class APPROVE_FLOW_LISTController : ApiController
    {
        BLL.BaseInfo.APPROVE_FLOW_LIST _APPROVE_FLOW_LIST = new BLL.BaseInfo.APPROVE_FLOW_LIST();
        #region 查询

        public List<APPROVE_FLOW_LIST_Entity> GetData()
        {
            return _APPROVE_FLOW_LIST.GetData();
        }

        public List<APPROVE_FLOW_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _APPROVE_FLOW_LIST.GetData(pageSize, pageNumber);
        }

        public List<APPROVE_FLOW_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _APPROVE_FLOW_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(APPROVE_FLOW_LIST_Entity entity)
        {
            return _APPROVE_FLOW_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(APPROVE_FLOW_LIST_Entity entity)
        {
            return _APPROVE_FLOW_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(APPROVE_FLOW_LIST_Entity entity)
        {
            return _APPROVE_FLOW_LIST.PostDelete(entity);
        }

        #endregion



    }
}
