using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_BOM_SPEC_INFO
    {
        readonly DAL.Package.PACKAGE_BOM_SPEC_INFO _PACKAGE_BOM_SPEC_INFO = new DAL.Package.PACKAGE_BOM_SPEC_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetData()
        {
            return _PACKAGE_BOM_SPEC_INFO.GetData();
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_BOM_SPEC_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_BOM_SPEC_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string PROCESS_ID, string VERSION_NO)
        {
            return _PACKAGE_BOM_SPEC_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, FACTORY_ID, PROCESS_ID, VERSION_NO);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            if (string.IsNullOrEmpty(entity.GROUPS)) return 1;
            foreach (var g in entity.GROUPS.Split(','))
            {
                entity.GROUP_NO = g;
                _PACKAGE_BOM_SPEC_INFO.PostAdd(entity);
            }
            return 1;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            if (string.IsNullOrEmpty(entity.GROUPS))//如果不是批量修改
            {
                return _PACKAGE_BOM_SPEC_INFO.PostEdit(entity);
            }
            else
            {
                _PACKAGE_BOM_SPEC_INFO.PostEdit(entity);
                foreach (var g in entity.GROUPS.Split(','))
                {
                    entity.GROUP_NO = g;
                    _PACKAGE_BOM_SPEC_INFO.PostEdit(entity);                    
                }
            }
            return 1;
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_BOM_SPEC_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            return _PACKAGE_BOM_SPEC_INFO.PostDelete(entity);
        }

        #endregion



    }
}
