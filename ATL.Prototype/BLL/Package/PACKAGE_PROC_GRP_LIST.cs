using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PROC_GRP_LIST
    {
        readonly DAL.Package.PACKAGE_PROC_GRP_LIST _PACKAGE_PROC_GRP_LIST = new DAL.Package.PACKAGE_PROC_GRP_LIST();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetData()
        {
            return _PACKAGE_PROC_GRP_LIST.GetData();
        }

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_GRP_LIST.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetDataById(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string GROUP_NO, string queryStr)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataById(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, GROUP_NO, queryStr);
        }

        public List<PACKAGE_PROC_GRP_LIST_Entity> GetDataByGrpId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataByGrpId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }

        public bool GetDataValidateId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string GROUP_NO)
        {
            return _PACKAGE_PROC_GRP_LIST.GetDataValidateId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, GROUP_NO);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_PROC_GRP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_PROC_GRP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_GRP_LIST_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            return _PACKAGE_PROC_GRP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
