using Model.BaseInfo;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_DESIGN_INFO
    {
        readonly DAL.Package.PACKAGE_DESIGN_INFO _PACKAGE_DESIGN_INFO = new DAL.Package.PACKAGE_DESIGN_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_DESIGN_INFO_Entity> GetData()
        {
            return _PACKAGE_DESIGN_INFO.GetData();
        }

        public List<PACKAGE_DESIGN_INFO_Entity> GetData(decimal pageSize, decimal pageNumber, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_DESIGN_INFO.GetData(pageSize, pageNumber, factoryId, packageNo, versionNo, queryStr);
        }
        public List<PACKAGE_DESIGN_INFO_Entity> GetDataById(string groupNo, string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_DESIGN_INFO.GetDataById(groupNo, factoryId, packageNo, versionNo);
        }
        public List<PACKAGE_DESIGN_INFO_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_DESIGN_INFO.GetData(factoryId, packageNo, versionNo, queryStr);
        }

        public List<PACKAGE_DESIGN_INFO_Entity> PostDataQuery(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return _PACKAGE_DESIGN_INFO.PostDataQuery(entity);
        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_DESIGN_INFO_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_DESIGN_INFO.PostAdd(entity);
        }
        public int PostBatchAdd(PACKAGE_DESIGN_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            var total = 0;
            if (string.IsNullOrEmpty(entity.GROUP_NO))
            {
                return 0;
            }
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var groupNo = entity.GROUP_NO.Split(',');
            foreach (var t in groupNo)
            {
                entity.GROUP_NO = t;
                total += PostAdd(entity);
            }
            return total;
        }
        #endregion

        #region 修改

        public int PostEdit(PACKAGE_DESIGN_INFO_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //Init_btn_ANODE_STUFF_ID('ANODE_STUFF_ID', 'A-001', '阳极材料');
            //Init_btn_ANODE_STUFF_ID('CATHODE_STUFF_ID', 'C-001', '阴极材料');
            //Init_btn_ANODE_STUFF_ID('ANODE_FOIL_ID', 'A-002', '阳极集流体材料');
            //Init_btn_ANODE_STUFF_ID('CATHODE_FOIL_ID', 'C-002', '阴极集流体材料');
            //Init_btn_ANODE_STUFF_ID('SEPARATOR_ID', 'S-001', '隔离膜材料');
            //Init_btn_ANODE_STUFF_ID('ELECTROLYTE_ID', 'E-001', '电解液配方');
            //Init_btn_ANODE_FORMULA_ID('ANODE_FORMULA_ID', 'AMIX', '阳极配方');
            //Init_btn_ANODE_FORMULA_ID('CATHODE_FORMULA_ID', 'CMIX', '阴极配方');
            var MATERIAL_PN_LIST = new DAL.BaseInfo.MATERIAL_PN_LIST();
            var _RECIPE_LIST = new DAL.BaseInfo.RECIPE_LIST();

            //阳极材料
            if (!string.IsNullOrEmpty(entity.ANODE_STUFF_ID))
            {
                var ANODE_STUFF_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("A-001", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.ANODE_STUFF_ID).Count();
                if (ANODE_STUFF_ID==0)
                {
                    return -2;
                }
            }
            
            //阴极材料
            if (!string.IsNullOrEmpty(entity.CATHODE_STUFF_ID))
            {
                var CATHODE_STUFF_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("C-001", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.CATHODE_STUFF_ID).Count();
                if (CATHODE_STUFF_ID == 0)
                {
                    return -2;
                }
            }
            //阳极集流体材料
            if (!string.IsNullOrEmpty(entity.ANODE_FOIL_ID))
            {
                var ANODE_FOIL_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("A-002", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.ANODE_FOIL_ID).Count();
                if (ANODE_FOIL_ID == 0)
                {
                    return -2;
                }
            }
            //阴极集流体材料
            if (!string.IsNullOrEmpty(entity.CATHODE_FOIL_ID))
            {
                var CATHODE_FOIL_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("C-002", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.CATHODE_FOIL_ID).Count();
                if (CATHODE_FOIL_ID == 0)
                {
                    return -2;
                }
            }
            //隔离膜材料
            if (!string.IsNullOrEmpty(entity.SEPARATOR_ID))
            {
                var SEPARATOR_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("S-001", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.SEPARATOR_ID).Count();
                if (SEPARATOR_ID == 0)
                {
                    return -2;
                }
            }
            //电解液配方
            if (!string.IsNullOrEmpty(entity.ELECTROLYTE_ID))
            {
                var ELECTROLYTE_ID = MATERIAL_PN_LIST.GetDataByGrpAndID("E-001", entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.ELECTROLYTE_ID).Count();
                if (ELECTROLYTE_ID == 0)
                {
                    return -2;
                }
            }
            //阳极配方
            if (!string.IsNullOrEmpty(entity.ANODE_FORMULA_ID))
            {
                var ANODE_FORMULA_ID = _RECIPE_LIST.GetDataByTypeAndId(entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, "AMIX", entity.ANODE_FORMULA_ID).Count();
                if (ANODE_FORMULA_ID == 0)
                {
                    return -2;
                }
            }
            //阴极配方
            if (!string.IsNullOrEmpty(entity.CATHODE_FORMULA_ID))
            {
                var CATHODE_FORMULA_ID = _RECIPE_LIST.GetDataByTypeAndId(entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, "CMIX", entity.CATHODE_FORMULA_ID).Count();
                if (CATHODE_FORMULA_ID == 0)
                {
                    return -2;
                }
            }
            //if (ANODE_STUFF_ID == 0 ||
            //    CATHODE_STUFF_ID == 0 ||
            //    ANODE_FOIL_ID == 0 ||
            //    CATHODE_FOIL_ID == 0 ||
            //    SEPARATOR_ID == 0 ||
            //    ELECTROLYTE_ID == 0 ||
            //    ANODE_FORMULA_ID == 0 ||
            //    CATHODE_FORMULA_ID == 0
            //    )
            //{
            //    return 0;
            //}
            return _PACKAGE_DESIGN_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_DESIGN_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            return _PACKAGE_DESIGN_INFO.PostDelete(entity);
        }

        #endregion



    }
}
