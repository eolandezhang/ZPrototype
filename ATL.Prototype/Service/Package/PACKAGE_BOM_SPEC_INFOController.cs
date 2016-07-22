using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_BOM_SPEC_INFOController : ApiController
    {
        BLL.Package.PACKAGE_BOM_SPEC_INFO _PACKAGE_BOM_SPEC_INFO = new BLL.Package.PACKAGE_BOM_SPEC_INFO();
        #region 查询

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetData()
        {
            return _PACKAGE_BOM_SPEC_INFO.GetData();
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_BOM_SPEC_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string PROCESS_ID, string VERSION_NO)
        {
            return _PACKAGE_BOM_SPEC_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, FACTORY_ID, PROCESS_ID, VERSION_NO);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_BOM_SPEC_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_BOM_SPEC_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_BOM_SPEC_INFO.PostDelete(entity);
        }

        #endregion



    }
}
