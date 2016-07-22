using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_BASE_INFOController : ApiController
    {
        BLL.Package.PACKAGE_BASE_INFO _PACKAGE_BASE_INFO = new BLL.Package.PACKAGE_BASE_INFO();
        #region 查询

        public List<PACKAGE_BASE_INFO_Entity> GetData()
        {
            return _PACKAGE_BASE_INFO.GetData();
        }
        public PACKAGE_BASE_INFO_Entity GetDataById(string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_BASE_INFO.GetData(factoryId, packageNo, versionNo);
        }
        public List<PACKAGE_BASE_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber, string queryStr)
        {
            return _PACKAGE_BASE_INFO.GetData(pageSize, pageNumber, queryStr);
        }
        public List<PACKAGE_BASE_INFO_Entity> GetDataByFactoryId(string factoryId)
        {
            return _PACKAGE_BASE_INFO.GetDataByFactoryId(factoryId);
        }
        public List<PACKAGE_BASE_INFO_Entity> GetDataByPackageNo(string factoryId, string packageNo)
        {
            return _PACKAGE_BASE_INFO.GetDataByPackageNo(factoryId, packageNo);
        }
        #endregion

        #region 新增
        [HttpPost]
        public int PostAdd(PACKAGE_BASE_INFO_Entity entity)
        {
            return _PACKAGE_BASE_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BASE_INFO_Entity entity)
        {
            return _PACKAGE_BASE_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_BASE_INFO_Entity entity)
        {
            return _PACKAGE_BASE_INFO.PostDelete(entity);
        }

        #endregion

        #region 复制
        public int PostCopy(PACKAGE_BASE_INFO_Entity entity)
        {
            return _PACKAGE_BASE_INFO.PostCopy(entity);
        }
        #endregion


        #region 版本号
        [HttpGet]
        public string GenerateVersion(string factoryId, string packageNo)
        {
            return _PACKAGE_BASE_INFO.GenerateVersion(factoryId, packageNo);
        }
        #endregion
    }
}
