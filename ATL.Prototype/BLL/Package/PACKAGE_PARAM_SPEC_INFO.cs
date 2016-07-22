using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PARAM_SPEC_INFO
    {
        readonly DAL.Package.PACKAGE_PARAM_SPEC_INFO _PACKAGE_PARAM_SPEC_INFO = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData()
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData();
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetData(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID, string SPEC_TYPE)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetData(PACKAGE_NO, GROUP_NO, VERSION_NO, PARAMETER_ID, FACTORY_ID, SPEC_TYPE);
        }
        
        public List<PACKAGE_PARAM_SPEC_INFO_Entity> GetDataByParamId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PARAMETER_ID, string FACTORY_ID)
        {
            return _PACKAGE_PARAM_SPEC_INFO.GetDataByParamId(PACKAGE_NO, GROUP_NO, VERSION_NO, PARAMETER_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_PARAM_SPEC_INFO.PostAdd(entity);
            if (result <= 0) return result;
            if (entity.SPEC_TYPE == "FAI")//首件
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_FAI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_FIRST_CHECK_PARAM = "1"
                });
            }
            if (entity.SPEC_TYPE == "PMI")//过程
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_PMI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_PROC_MON_PARAM = "1"
                });
            }
            if (entity.SPEC_TYPE == "OI")//出货
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_OI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_OUTPUT_PARAM = "1"
                });
            }
            return result;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            _PACKAGE_PARAM_SPEC_INFO.PostEdit(entity);
            if (string.IsNullOrEmpty(entity.GROUPS)) return 1;
            foreach (var g in entity.GROUPS.Split(',').Where(x => x != entity.GROUP_NO))
            {
                entity.GROUP_NO = g;
                _PACKAGE_PARAM_SPEC_INFO.PostEdit(entity);
            }
            return 1;
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PARAM_SPEC_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            var result = _PACKAGE_PARAM_SPEC_INFO.PostDelete(entity);
            if (result <= 0) return result;
            if (entity.SPEC_TYPE == "FAI")//首件
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_FAI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_FIRST_CHECK_PARAM = "0"
                });
            }

            if (entity.SPEC_TYPE == "PMI")//过程
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_PMI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_PROC_MON_PARAM = "0"
                });
            }

            if (entity.SPEC_TYPE == "OI")//出货
            {
                new DAL.Package.PACKAGE_PARAM_SETTING().PostEdit_OI(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    IS_OUTPUT_PARAM = "0"
                });
            }
            return result;
        }

        #endregion



    }
}
