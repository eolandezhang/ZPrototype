using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PROCESS_GROUP_LISTController : ApiController
    {
        BLL.BaseInfo.PROCESS_GROUP_LIST _PROCESS_GROUP_LIST = new BLL.BaseInfo.PROCESS_GROUP_LIST();
        #region 查询

        public List<PROCESS_GROUP_LIST_Entity> GetData(string factoryId, string productTypeId, string produceProcTypeId, string queryStr)
        {
            return _PROCESS_GROUP_LIST.GetData(factoryId, productTypeId, produceProcTypeId, queryStr);
        }

        public List<PROCESS_GROUP_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_GROUP_LIST.GetData(pageSize, pageNumber);
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_GROUP_LIST_Entity entity)
        {
            return _PROCESS_GROUP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_GROUP_LIST_Entity entity)
        {
            return _PROCESS_GROUP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_GROUP_LIST_Entity entity)
        {
            return _PROCESS_GROUP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
