using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_PROC_MATERIAL_INFOController : ApiController
    {
        BLL.Package.PACKAGE_PROC_MATERIAL_INFO _PACKAGE_PROC_MATERIAL_INFO = new BLL.Package.PACKAGE_PROC_MATERIAL_INFO();
        #region 查询

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetData()
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetData();
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, PROCESS_ID,FACTORY_ID);
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetDataByCategoryId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string queryStr)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetDataByCategoryId(PACKAGE_NO, GROUP_NO, VERSION_NO, PROCESS_ID, FACTORY_ID, MATERIAL_CATEGORY_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.PostDelete(entity);
        }

        #endregion



    }
}
