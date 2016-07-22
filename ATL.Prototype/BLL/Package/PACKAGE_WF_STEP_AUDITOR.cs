using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_WF_STEP_AUDITOR
    {
        DAL.Package.PACKAGE_WF_STEP_AUDITOR _PACKAGE_WF_STEP_AUDITOR = new DAL.Package.PACKAGE_WF_STEP_AUDITOR();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetData()
        {
            return _PACKAGE_WF_STEP_AUDITOR.GetData();
        }

        public List<PACKAGE_WF_STEP_AUDITOR_Entity> GetData(decimal pageSize, decimal pageNumber)
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
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_WF_STEP_AUDITOR.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_AUDITOR_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
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
