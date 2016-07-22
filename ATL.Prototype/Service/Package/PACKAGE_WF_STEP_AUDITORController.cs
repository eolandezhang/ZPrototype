using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_WF_STEP_AUDITORController : ApiController
    {
        BLL.Package.PACKAGE_WF_STEP_AUDITOR _PACKAGE_WF_STEP_AUDITOR = new BLL.Package.PACKAGE_WF_STEP_AUDITOR();
        #region 查询

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetData()
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetData();
        }

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetDataById(decimal AUDITOR_ID, string queryStr)
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetDataById(AUDITOR_ID, queryStr);
        }
        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetDataByPkgStepId(decimal PACKAGE_WF_STEP_ID)
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(PACKAGE_WF_STEP_ID);
        }

        public bool GetDataValidateId(decimal AUDITOR_ID)
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetDataValidateId(AUDITOR_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            return _PACKAGE_WF_STEP_AUDITOR.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            return _PACKAGE_WF_STEP_AUDITOR.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            return _PACKAGE_WF_STEP_AUDITOR.PostDelete(entity);
        }

        #endregion



    }
}
