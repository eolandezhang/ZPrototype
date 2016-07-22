using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_TYPE_LISTController : ApiController
    {
        BLL.Package.PACKAGE_TYPE_LIST _PACKAGE_TYPE_LIST = new BLL.Package.PACKAGE_TYPE_LIST();
        #region 查询

        public List<PACKAGE_TYPE_LIST_Entity> GetData()
        {
            return _PACKAGE_TYPE_LIST.GetData();
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _PACKAGE_TYPE_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_TYPE_LIST_Entity entity)
        {
            return _PACKAGE_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_TYPE_LIST_Entity entity)
        {
            return _PACKAGE_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_TYPE_LIST_Entity entity)
        {
            return _PACKAGE_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
