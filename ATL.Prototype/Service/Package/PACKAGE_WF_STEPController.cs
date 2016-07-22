using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_WF_STEPController : ApiController
    {
        BLL.Package.PACKAGE_WF_STEP _PACKAGE_WF_STEP = new BLL.Package.PACKAGE_WF_STEP();
        #region 查询

        public List<PACKAGE_WF_STEP_Entity> GetData()
        {
            return _PACKAGE_WF_STEP.GetData();
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_WF_STEP.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataById(decimal PACKAGE_WF_STEP_ID, string queryStr)
        {
            return _PACKAGE_WF_STEP.GetDataById(PACKAGE_WF_STEP_ID, queryStr);
        }
        public List<PACKAGE_WF_STEP_Entity> GetDataByPkgId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _PACKAGE_WF_STEP.GetDataByPkgId(PACKAGE_NO, VERSION_NO, FACTORY_ID);
        }

        public bool GetDataValidateId(decimal PACKAGE_WF_STEP_ID)
        {
            return _PACKAGE_WF_STEP.GetDataValidateId(PACKAGE_WF_STEP_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_WF_STEP_Entity entity)
        {
            return _PACKAGE_WF_STEP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_Entity entity)
        {
            return _PACKAGE_WF_STEP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_WF_STEP_Entity entity)
        {
            return _PACKAGE_WF_STEP.PostDelete(entity);
        }

        #endregion



    }
}
