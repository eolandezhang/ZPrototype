using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_ILLUSTRATION_INFOController : ApiController
    {
        BLL.Package.PACKAGE_ILLUSTRATION_INFO _PACKAGE_ILLUSTRATION_INFO = new BLL.Package.PACKAGE_ILLUSTRATION_INFO();
        #region 查询

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetData()
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetData();
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return _PACKAGE_ILLUSTRATION_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return _PACKAGE_ILLUSTRATION_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return _PACKAGE_ILLUSTRATION_INFO.PostDelete(entity);
        }

        #endregion



    }
}
