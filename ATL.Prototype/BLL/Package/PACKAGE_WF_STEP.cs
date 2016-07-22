using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.SS.Formula.Functions;

namespace BLL.Package
{
    public class PACKAGE_WF_STEP
    {
        readonly DAL.Package.PACKAGE_WF_STEP _PACKAGE_WF_STEP = new DAL.Package.PACKAGE_WF_STEP();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_WF_STEP_Entity> GetData()
        {
            return _PACKAGE_WF_STEP.GetData();
        }

        public List<PACKAGE_WF_STEP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_WF_STEP.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataById(decimal PACKAGE_WF_STEP_ID, string queryStr)
        {
            return _PACKAGE_WF_STEP.GetDataById(PACKAGE_WF_STEP_ID, queryStr);
        }

        public List<PACKAGE_WF_STEP_Entity> GetDataByPkgId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgWfStep= _PACKAGE_WF_STEP.GetDataByPkgId(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            var lst = from x in pkgWfStep
                      orderby x.UPDATE_DATE descending
                      select x;
            return lst.ToList();
        }
         
        public bool GetDataValidateId(decimal PACKAGE_WF_STEP_ID)
        {
            return _PACKAGE_WF_STEP.GetDataValidateId(PACKAGE_WF_STEP_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_WF_STEP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_WF_STEP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_WF_STEP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
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
