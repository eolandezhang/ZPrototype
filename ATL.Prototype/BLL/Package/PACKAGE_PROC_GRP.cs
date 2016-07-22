using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PROC_GRP
    {
        readonly DAL.Package.PACKAGE_PROC_GRP _PACKAGE_PROC_GRP = new DAL.Package.PACKAGE_PROC_GRP();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PROC_GRP_Entity> GetData()
        {
            return _PACKAGE_PROC_GRP.GetData();
        }

        public List<PACKAGE_PROC_GRP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_GRP.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataById(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP.GetDataById(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }

        public List<PACKAGE_PROC_GRP_Entity> GetDataByProcessId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PROC_GRP.GetDataByProcessId(PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID, queryStr);
        }

        public bool GetDataValidateId(string PROC_GRP_ID, string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return _PACKAGE_PROC_GRP.GetDataValidateId(PROC_GRP_ID, PACKAGE_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_GRP_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var _PACKAGE_PROC_GRP_LIST = new DAL.Package.PACKAGE_PROC_GRP_LIST();
            var groups = entity.GROUPS.Split(',').OrderBy(x => x).Aggregate("", (current, item) => current + item);
            entity.PROC_GRP_ID = groups;
            var result = _PACKAGE_PROC_GRP.PostAdd(entity);
            #region 批量添加分组
            foreach (var item in entity.GROUPS.Split(','))
            {
                _PACKAGE_PROC_GRP_LIST.PostAdd(new PACKAGE_PROC_GRP_LIST_Entity
                {
                    GROUP_NO = item,
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    PROCESS_ID = entity.PROCESS_ID,
                    PROC_GRP_ID = entity.PROC_GRP_ID,
                    UPDATE_USER = entity.UPDATE_USER,
                    UPDATE_DATE = entity.UPDATE_DATE
                });
            }
            #endregion
            return result;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_GRP_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_PROC_GRP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_GRP_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            new DAL.Package.PACKAGE_PROC_GRP_LIST().DeleteByGRP(new PACKAGE_PROC_GRP_LIST_Entity
            {
                PROC_GRP_ID = entity.PROC_GRP_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                FACTORY_ID = entity.FACTORY_ID,
                PROCESS_ID = entity.PROCESS_ID
            });
            return _PACKAGE_PROC_GRP.PostDelete(entity);
        }


        #endregion



    }
}
