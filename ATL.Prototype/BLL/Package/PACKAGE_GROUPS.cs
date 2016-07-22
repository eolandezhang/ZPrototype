using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_GROUPS
    {
        readonly DAL.Package.PACKAGE_GROUPS _PACKAGE_GROUPS = new DAL.Package.PACKAGE_GROUPS();
        readonly DAL.Package.PACKAGE_BASE_INFO _PACKAGE_BASE_INFO = new DAL.Package.PACKAGE_BASE_INFO();
        readonly DAL.Package.PACKAGE_DESIGN_INFO _PACKAGE_DESIGN_INFO = new DAL.Package.PACKAGE_DESIGN_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_GROUPS_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_GROUPS.GetData(factoryId, packageNo, versionNo, queryStr);
        }
        public List<PACKAGE_GROUPS_Entity> GetGroupsNotInDesignInfo(string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_GROUPS.GetGroupsNotInDesignInfo(factoryId, packageNo, versionNo);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_GROUPS_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_GROUPS.PostAdd(entity);
            if (result == 0) return result;
            UpdateGroupInfo(entity);
            //增加设计信息
            _PACKAGE_DESIGN_INFO.PostAdd(new PACKAGE_DESIGN_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO,
                VALID_FLAG = "1",
                UPDATE_DATE = entity.UPDATE_DATE,
                UPDATE_USER = entity.UPDATE_USER
            });
            //计算分组总数
            var groups = GetData(entity.FACTORY_ID, entity.PACKAGE_NO, entity.VERSION_NO, "");
            decimal total = 0;
            foreach (var g in groups)
            {
                total += g.GROUP_QTY;
            }
            _PACKAGE_BASE_INFO.UpdateBatteryQty(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID, total);
            return result;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_GROUPS_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_GROUPS.PostEdit(entity);
            UpdateGroupInfo(entity);
            //计算分组总数
            var groups = GetData(entity.FACTORY_ID, entity.PACKAGE_NO, entity.VERSION_NO, "");
            decimal total = 0;
            foreach (var g in groups)
            {
                total += g.GROUP_QTY;
            }
            _PACKAGE_BASE_INFO.UpdateBatteryQty(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID, total);
            return result;
        }

        public int UpdateGroupInfo(PACKAGE_GROUPS_Entity entity)
        {
            var groupsList = GetData(entity.FACTORY_ID, entity.PACKAGE_NO, entity.VERSION_NO, "");
            var groups = groupsList.Count();
            var groupNoList = "";
            var groupQtyList = "";
            if (groups > 0)
            {
                foreach (var item in groupsList)
                {
                    groupNoList += item.GROUP_NO + ",";
                    groupQtyList += item.GROUP_QTY + ",";
                }
            }
            groupNoList = groupNoList.TrimEnd(',');
            groupQtyList = groupQtyList.TrimEnd(',');
            return _PACKAGE_BASE_INFO.UpdateGroupInfo(groups, groupNoList, groupQtyList, entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO);
        }
        #endregion

        #region 删除

        public int PostDelete(PACKAGE_GROUPS_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            #region 工序明细

            //参数
            new DAL.Package.PACKAGE_PARAM_SPEC_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_PARAM_SPEC_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //参数设定信息
            new DAL.Package.PACKAGE_PARAM_SETTING().DeleteByPackageIdAndGroupNo(new PACKAGE_PARAM_SETTING_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //物料信息
            new DAL.Package.PACKAGE_PROC_MATERIAL_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_PROC_MATERIAL_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            new DAL.Package.PACKAGE_PROC_PN_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_PROC_PN_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //设备信息
            new DAL.Package.PACKAGE_PROC_EQUIP_CLASS_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_PROC_EQUIP_CLASS_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            new DAL.Package.PACKAGE_PROC_EQUIP_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_PROC_EQUIP_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //附图信息            

            new DAL.Package.PACKAGE_ILLUSTRATION_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_ILLUSTRATION_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //BOM信息
            new DAL.Package.PACKAGE_BOM_SPEC_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_BOM_SPEC_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            #endregion

            //设计信息
            new DAL.Package.PACKAGE_DESIGN_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_DESIGN_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });

            //工序信息
            new DAL.Package.PACKAGE_FLOW_INFO().DeleteByPackageIdAndGroupNo(new PACKAGE_FLOW_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                GROUP_NO = entity.GROUP_NO
            });
            //某个分组
            var result = _PACKAGE_GROUPS.PostDelete(entity);
            UpdateGroupInfo(entity);
            return result;
        }

        #endregion



    }
}
