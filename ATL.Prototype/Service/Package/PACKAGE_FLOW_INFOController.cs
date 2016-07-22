using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_FLOW_INFOController : ApiController
    {
        BLL.Package.PACKAGE_FLOW_INFO _PACKAGE_FLOW_INFO = new BLL.Package.PACKAGE_FLOW_INFO();
        #region 查询       

        public List<PACKAGE_FLOW_INFO_Entity> GetDataNoPage(string groupNo, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_FLOW_INFO.GetData( groupNo,factoryId, packageNo, versionNo, queryStr);
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetGroupNoByProcessId(string factoryId, string packageNo, string versionNo, string processId,string queryStr)
        {
            return _PACKAGE_FLOW_INFO.GetGroupNoByProcessId(factoryId, packageNo, versionNo, processId,queryStr);
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetDataByPackageId(string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_FLOW_INFO.GetDataByPackageId(factoryId, packageNo, versionNo);
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetDataByProcessId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID)
        {
            return _PACKAGE_FLOW_INFO.GetDataByProcessId(PACKAGE_NO, FACTORY_ID, VERSION_NO, PROCESS_ID);
        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostAdd(entity);
        }
        public int PostAddBatch(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostAddBatch(entity);
        }
        public int PostAddBatchOneProcess(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostAddBatchOneProcess(entity);
        }
        #endregion

        #region 修改

        public int PostEdit(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostDelete(entity);
        }
        public int PostDelete_Batch(PACKAGE_FLOW_INFO_Entity entity)
        {
            return _PACKAGE_FLOW_INFO.PostDelete_Batch(entity);
        }
        #endregion



    }
}
