using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class PROCESS_LISTController : ApiController
    {
        BLL.BaseInfo.PROCESS_LIST _PROCESS_LIST = new BLL.BaseInfo.PROCESS_LIST();
        #region 查询

        public List<PROCESS_LIST_Entity> GetDataByFactoryIdAndTypeId(string factoryId, string productTypeId, string produceProcTypeId)
        {
            return _PROCESS_LIST.GetDataByFactoryIdAndTypeId(factoryId, productTypeId, produceProcTypeId);
        }

        public List<PROCESS_LIST_Entity> GetData(string factoryId, string productTypeId, string produceProcTypeId, string processGroupId, string queryStr)
        {
            return _PROCESS_LIST.GetData(factoryId, productTypeId, produceProcTypeId, processGroupId, queryStr);
        }

        public List<PROCESS_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_LIST.GetData(pageSize, pageNumber);
        }        

        #endregion

        #region 新增

        public int PostAdd(PROCESS_LIST_Entity entity)
        {
            return _PROCESS_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_LIST_Entity entity)
        {
            return _PROCESS_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_LIST_Entity entity)
        {
            return _PROCESS_LIST.PostDelete(entity);
        }

        #endregion



    }
}
