using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_DESIGN_INFOController : ApiController
    {
        BLL.Package.PACKAGE_DESIGN_INFO _PACKAGE_DESIGN_INFO = new BLL.Package.PACKAGE_DESIGN_INFO();
        #region 查询


        public List<PACKAGE_DESIGN_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_DESIGN_INFO.GetData(pageSize, pageNumber, factoryId, packageNo, versionNo, queryStr);
        }
        public List<PACKAGE_DESIGN_INFO_Entity> GetDataById(string groupNo, string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_DESIGN_INFO.GetDataById(groupNo, factoryId, packageNo, versionNo);
        }
        public List<PACKAGE_DESIGN_INFO_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_DESIGN_INFO.GetData(factoryId, packageNo, versionNo, queryStr);
        }

        public List<PACKAGE_DESIGN_INFO_Entity> PostDataQuery(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostDataQuery(entity);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostAdd(entity);
        }
        public int PostBatchAdd(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostBatchAdd(entity);
        }
        #endregion

        #region 修改

        public int PostEdit(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostDelete(entity);
        }

        #endregion



    }
}
