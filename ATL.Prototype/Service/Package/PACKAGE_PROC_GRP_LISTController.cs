using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_PROC_GRP_LISTController : ApiController
    {
       
        BLL.Package.PACKAGE_PROC_GRP_LIST _PACKAGE_PROC_GRP_LIST = new BLL.Package.PACKAGE_PROC_GRP_LIST();
        #region 查询

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetData()
        {
            return _PACKAGE_PROC_GRP_LIST.GetData();
        }

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_GRP_LIST.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetDataById(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string GROUP_NO, string queryStr)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataById(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, GROUP_NO, queryStr);
        }
        public List<PACKAGE_PROC_GRP_LIST_Entity> GetDataByGrpId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataByGrpId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }

        public bool GetDataValidateId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string GROUP_NO)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataValidateId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, GROUP_NO);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            return _PACKAGE_PROC_GRP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            return _PACKAGE_PROC_GRP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            return _PACKAGE_PROC_GRP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
