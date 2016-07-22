using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_PROC_GRPController : ApiController
    {
        BLL.Package.PACKAGE_PROC_GRP _PACKAGE_PROC_GRP = new BLL.Package.PACKAGE_PROC_GRP();
        #region 查询

        public List<PACKAGE_PROC_GRP_Entity> GetData()
        {
            return _PACKAGE_PROC_GRP.GetData();
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_GRP.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataById(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP.GetDataById(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }
        public List<PACKAGE_PROC_GRP_Entity> GetDataByProcessId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP.GetDataByProcessId(PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }

        public bool GetDataValidateId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return _PACKAGE_PROC_GRP.GetDataValidateId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_GRP_Entity entity)
        {
            return _PACKAGE_PROC_GRP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_GRP_Entity entity)
        {
            return _PACKAGE_PROC_GRP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_GRP_Entity entity)
        {
            return _PACKAGE_PROC_GRP.PostDelete(entity);
        }

        #endregion



    }
}
