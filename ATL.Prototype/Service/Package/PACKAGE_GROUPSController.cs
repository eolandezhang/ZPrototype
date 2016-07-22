using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_GROUPSController : ApiController
    {
        BLL.Package.PACKAGE_GROUPS _PACKAGE_GROUPS = new BLL.Package.PACKAGE_GROUPS();
        #region 查询

        public List<PACKAGE_GROUPS_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_GROUPS.GetData(factoryId, packageNo, versionNo, queryStr);
        }
        public List<PACKAGE_GROUPS_Entity> GetGroupsNotInDesignInfo(string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_GROUPS.GetGroupsNotInDesignInfo(factoryId, packageNo, versionNo);
        }
           
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_GROUPS_Entity entity)
        {
            return _PACKAGE_GROUPS.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_GROUPS_Entity entity)
        {
            return _PACKAGE_GROUPS.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_GROUPS_Entity entity)
        {
            return _PACKAGE_GROUPS.PostDelete(entity);
        }

        #endregion



    }
}
