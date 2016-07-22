using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_PARAM_SPEC_INFOController : ApiController
    {
        BLL.Package.PACKAGE_PARAM_SPEC_INFO _PACKAGE_PARAM_SPEC_INFO = new BLL.Package.PACKAGE_PARAM_SPEC_INFO();
        #region 查询

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData()
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData();
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID, string SPEC_TYPE)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData(PACKAGE_NO, GROUP_NO, VERSION_NO, PARAMETER_ID, FACTORY_ID, SPEC_TYPE);
        }

        //public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        //{
        //    return _PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        //}

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByParamId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetDataByParamId(PACKAGE_NO, GROUP_NO, VERSION_NO, PARAMETER_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_PARAM_SPEC_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_PARAM_SPEC_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            return _PACKAGE_PARAM_SPEC_INFO.PostDelete(entity);
        }

        #endregion



    }
}
