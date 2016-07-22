using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PROC_EQUIP_INFO
    {
        readonly DAL.Package.PACKAGE_PROC_EQUIP_INFO _PACKAGE_PROC_EQUIP_INFO = new DAL.Package.PACKAGE_PROC_EQUIP_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetData()
        {
            return _PACKAGE_PROC_EQUIP_INFO.GetData();
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_EQUIP_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID)
        {
            return _PACKAGE_PROC_EQUIP_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, PROCESS_ID, FACTORY_ID);
        }

        public List<PACKAGE_PROC_EQUIP_INFO_Entity> GetDataByProcessIdAndGroupNoAndTypeId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return _PACKAGE_PROC_EQUIP_INFO.GetDataByProcessIdAndGroupNoAndTypeId(PACKAGE_NO, GROUP_NO, VERSION_NO, EQUIPMENT_TYPE_ID, PROCESS_ID, FACTORY_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var packageList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO, "");
            var package = packageList.First();
            if (!string.IsNullOrEmpty(entity.GROUPS))
            {
                foreach (var g in entity.GROUPS.Split(','))
                {
                    entity.GROUP_NO = g;
                    var result = _PACKAGE_PROC_EQUIP_INFO.PostAdd(entity);
                    if (result <= 0) continue;
                    var EQUIPMENT_PARAM_INFO = new DAL.BaseInfo.EQUIPMENT_PARAM_INFO().GetDataByEquipmentId(entity.EQUIPMENT_ID, package.PRODUCT_TYPE_ID, package.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID, "");
                    var PACKAGE_PARAM_SETTING = new PACKAGE_PARAM_SETTING();
                    foreach (var p in EQUIPMENT_PARAM_INFO)
                    {
                        PACKAGE_PARAM_SETTING.PostAdd(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = entity.PACKAGE_NO,
                            GROUP_NO = entity.GROUP_NO,
                            FACTORY_ID = entity.FACTORY_ID,
                            VERSION_NO = entity.VERSION_NO,
                            PARAMETER_ID = p.PARAMETER_ID,
                            PARAM_TYPE_ID = p.PARAM_TYPE_ID,
                            PROCESS_ID = entity.PROCESS_ID,
                            PARAM_IO = p.PARAM_IO,
                            IS_GROUP_PARAM = p.IS_GROUP_PARAM,
                            IS_FIRST_CHECK_PARAM = p.IS_FIRST_CHECK_PARAM,
                            IS_PROC_MON_PARAM = p.IS_PROC_MON_PARAM,
                            IS_OUTPUT_PARAM = p.IS_OUTPUT_PARAM,
                            PARAM_UNIT = p.PARAM_UNIT,
                            PARAM_DATATYPE = p.PARAM_DATATYPE,
                            TARGET = p.TARGET,
                            USL = p.USL,
                            LSL = p.LSL,
                            UPDATE_USER = entity.UPDATE_USER,
                            UPDATE_DATE = entity.UPDATE_DATE,
                            SAMPLING_FREQUENCY = p.SAMPLING_FREQUENCY,
                            CONTROL_METHOD = p.CONTROL_METHOD,
                            IS_SC_PARAM = p.IS_SC_PARAM
                        });
                    }
                }
            }
            else
            {
                return -1;
            }
            return 1;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_PROC_EQUIP_INFO.PostEdit(entity);
            if (!string.IsNullOrEmpty(entity.GROUPS))
            {
                foreach (var g in entity.GROUPS.Split(',').Where(x => x != entity.GROUP_NO))
                {
                    entity.GROUP_NO = g;
                    _PACKAGE_PROC_EQUIP_INFO.PostEdit(entity);
                }
            }
            return result;
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_EQUIP_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            #region 删除参数
            //取得所有参数
            var PACKAGE_PARAM_SETTING = new DAL.Package.PACKAGE_PARAM_SETTING().GetDataByEquipmentInfo(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID, entity.GROUP_NO, entity.EQUIPMENT_ID, "");
            //依次删除
            var bllPackageParamSetting = new PACKAGE_PARAM_SETTING();
            foreach (var p in PACKAGE_PARAM_SETTING)
            {
                bllPackageParamSetting.PostDelete(p);
            }
            #endregion
            return _PACKAGE_PROC_EQUIP_INFO.PostDelete(entity);
        }

        #endregion



    }
}
