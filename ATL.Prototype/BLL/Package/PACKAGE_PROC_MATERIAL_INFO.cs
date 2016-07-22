using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PROC_MATERIAL_INFO
    {
        readonly DAL.Package.PACKAGE_PROC_MATERIAL_INFO _PACKAGE_PROC_MATERIAL_INFO = new DAL.Package.PACKAGE_PROC_MATERIAL_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetData()
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetData();
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, PROCESS_ID, FACTORY_ID);
        }

        public List<PACKAGE_PROC_MATERIAL_INFO_Entity> GetDataByCategoryId(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string PROCESS_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string queryStr)
        {
            return _PACKAGE_PROC_MATERIAL_INFO.GetDataByCategoryId(PACKAGE_NO, GROUP_NO, VERSION_NO, PROCESS_ID, FACTORY_ID, MATERIAL_CATEGORY_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            if (!BLL.Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var packageList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO, "");
            if (!packageList.Any())
            {
                return 0;
            }
            var package = packageList.First();
            if (!string.IsNullOrEmpty(entity.GROUPS))
            {
                foreach (var g in entity.GROUPS.Split(','))
                {
                    entity.GROUP_NO = g;
                    var result = _PACKAGE_PROC_MATERIAL_INFO.PostAdd(entity);
                    if (result <= 0) continue;

                    #region 批量增加参数
                    //物料类型参数
                    var paramList = new DAL.BaseInfo.MATERIAL_PARA_INFO().GetDataByTypeId(entity.MATERIAL_TYPE_ID, entity.FACTORY_ID, package.PRODUCT_TYPE_ID, package.PRODUCT_PROC_TYPE_ID, "");
                    if (!paramList.Any()) continue;
                    foreach (var p in paramList)
                    {
                        new PACKAGE_PARAM_SETTING().PostAdd(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = entity.PACKAGE_NO,
                            GROUP_NO = entity.GROUP_NO,
                            FACTORY_ID = entity.FACTORY_ID,
                            VERSION_NO = entity.VERSION_NO,
                            PRODUCT_TYPE_ID = package.PRODUCT_TYPE_ID,
                            PRODUCT_PROC_TYPE_ID = package.PRODUCT_PROC_TYPE_ID,
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
                            IS_SC_PARAM = p.IS_SPEC_PARAM
                        });
                    }

                    #endregion
                }
            }
            return 1;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_PROC_MATERIAL_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PROC_MATERIAL_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            #region 删除对应的参数
            var temp = new List<PACKAGE_PARAM_SETTING_Entity>();
            //找到【物料类型】对应的所有参数
            var PACKAGE_PARAM_SETTING_MATERIAL_INFO = new PACKAGE_PARAM_SETTING().GetDataByMaterialTypeId(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID, entity.GROUP_NO, entity.MATERIAL_TYPE_ID, "");
            //找到此 【物料类型】的【物料编号】
            var PACKAGE_PROC_PN_INFO = new DAL.Package.PACKAGE_PROC_PN_INFO().GetDataByProcessIdAndGroupNo(entity.PACKAGE_NO, entity.GROUP_NO, entity.VERSION_NO, entity.PROCESS_ID, entity.FACTORY_ID).Where(x => x.MATERIAL_TYPE_ID == entity.MATERIAL_TYPE_ID);
            //遍历物料编号，取得对应的参数
            foreach (var pn in PACKAGE_PROC_PN_INFO)
            {
                var param = new DAL.Package.PACKAGE_PARAM_SETTING().GetDataByMaterialPN(pn.PACKAGE_NO, pn.VERSION_NO, pn.FACTORY_ID, pn.GROUP_NO, pn.MATERIAL_PN_ID, "");
                temp.AddRange(param);
            }
            temp = temp.Distinct(new comparer()).ToList();
            var paramSetting = PACKAGE_PARAM_SETTING_MATERIAL_INFO.Except(temp, new comparer()).ToList();
            var BLL_PACKAGE_PARAM_SETTING = new PACKAGE_PARAM_SETTING();

            foreach (var p in paramSetting)
            {
                BLL_PACKAGE_PARAM_SETTING.PostDelete(p);
            }

            #endregion

            return _PACKAGE_PROC_MATERIAL_INFO.PostDelete(entity);
        }

        #endregion




        class comparer : IEqualityComparer<PACKAGE_PARAM_SETTING_Entity>
        {
            public bool Equals(PACKAGE_PARAM_SETTING_Entity x, PACKAGE_PARAM_SETTING_Entity y)
            {

                //Check whether the compared objects reference the same data. 
                if (ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null. 
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                    return false;

                //Check whether the products' properties are equal. 
                return x.PARAMETER_ID == y.PARAMETER_ID;
            }


            public int GetHashCode(PACKAGE_PARAM_SETTING_Entity param)
            {
                //Check whether the object is null 
                if (ReferenceEquals(param, null)) return 0;

                //Get hash code for the Name field if it is not null. 
                var hash = param.PARAMETER_ID == null ? 0 : param.PARAMETER_ID.GetHashCode();

                //Calculate the hash code for the param. 
                return hash;
            }
        }
    }
}
