using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_FLOW_INFO
    {
        readonly DAL.Package.PACKAGE_FLOW_INFO _PACKAGE_FLOW_INFO = new DAL.Package.PACKAGE_FLOW_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_FLOW_INFO_Entity> GetData(string groupNo, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return _PACKAGE_FLOW_INFO.GetData(groupNo, factoryId, packageNo, versionNo, queryStr);
        }

        public List<PACKAGE_FLOW_INFO_Entity> GetGroupNoByProcessId(string factoryId, string packageNo, string versionNo, string processId, string queryStr)
        {
            return _PACKAGE_FLOW_INFO.GetGroupNoByProcessId(factoryId, packageNo, versionNo, processId, queryStr);
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetDataByPackageId(string factoryId, string packageNo, string versionNo)
        {
            return _PACKAGE_FLOW_INFO.GetDataByPackageId(factoryId, packageNo, versionNo);
        }
        public List<PACKAGE_FLOW_INFO_Entity> GetDataByProcessId(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID)
        {
            return _PACKAGE_FLOW_INFO.GetDataByProcessId(PACKAGE_NO, FACTORY_ID, VERSION_NO, PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_FLOW_INFO.PostAdd(entity);
            if (result <= 0) return result;
            //取得工艺产品参数，非图片参数
            var param = new DAL.BaseInfo.PROCESS_PARAM_INFO().GetDataByProcessIdQuery(entity.PROCESS_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID, " AND  IS_ILLUSTRATION_PARAM='0'" + "AND (PARAM.PARAM_TYPE_ID='PRODUCT' OR PARAM.PARAM_TYPE_ID='PROCESS') AND PARAM.VALID_FLAG='1'");
            foreach (var p in param)
            {
                new PACKAGE_PARAM_SETTING().PostAdd(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    GROUP_NO = entity.GROUP_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    VERSION_NO = entity.VERSION_NO,
                    PRODUCT_TYPE_ID = entity.PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID = entity.PRODUCT_PROC_TYPE_ID,
                    PARAMETER_ID = p.PARAMETER_ID,
                    PROCESS_ID = entity.PROCESS_ID,
                    PARAM_TYPE_ID = p.PARAM_TYPE_ID,
                    UPDATE_USER = entity.UPDATE_USER,
                    UPDATE_DATE = entity.UPDATE_DATE,
                    PARAM_IO = p.PARAM_IO,
                    IS_GROUP_PARAM = p.IS_GROUP_PARAM,
                    IS_FIRST_CHECK_PARAM = p.IS_FIRST_CHECK_PARAM,
                    IS_PROC_MON_PARAM = p.IS_PROC_MON_PARAM,
                    IS_OUTPUT_PARAM = p.IS_OUTPUT_PARAM,
                    TARGET = p.TARGET,
                    PARAM_UNIT = p.PARAM_UNIT,
                    PARAM_DATATYPE = p.PARAM_DATATYPE,
                    USL = p.USL,
                    LSL = p.LSL,
                    SAMPLING_FREQUENCY = p.SAMPLING_FREQUENCY,
                    CONTROL_METHOD = p.CONTROL_METHOD,
                    IS_SC_PARAM = p.IS_SC_PARAM,
                    DISP_ORDER_IN_SC = p.DISP_ORDER_IN_SC
                });
            }
            return result;
        }

        public int PostAddBatch(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var groupNos = entity.GROUP_NOS.Split(',');
            var processIds = entity.PROCESS_IDS.Split(',');
            var result = 0;
            foreach (var g in groupNos)
            {
                foreach (var p in processIds)
                {
                    if (string.IsNullOrEmpty(g) || string.IsNullOrEmpty(p)) continue;
                    var processList = new DAL.BaseInfo.PROCESS_LIST().GetDataById(p, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
                    if (!processList.Any()) continue;
                    var process = processList.First();
                    result += PostAdd(new PACKAGE_FLOW_INFO_Entity
                    {
                        PACKAGE_NO = entity.PACKAGE_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        GROUP_NO = g,
                        PROCESS_ID = p,
                        PROC_SEQUENCE_NO = process.SEQUENCE_NO,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE,
                        PREVIOUS_PROCESS_ID = process.PREVIOUS_PROCESS_ID,
                        NEXT_PROCESS_ID = process.NEXT_PROCESS_ID,
                        PRODUCT_TYPE_ID = entity.PRODUCT_TYPE_ID,
                        PRODUCT_PROC_TYPE_ID = entity.PRODUCT_PROC_TYPE_ID
                    });
                }
            }
            return result > 0 ? 1 : 0;
        }

        public int PostAddBatchOneProcess(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var groupNos = entity.GROUP_NOS.Split(',');

            var result = 0;
            foreach (var g in groupNos)
            {
                if (string.IsNullOrEmpty(g)) continue;
                result += PostAdd(new PACKAGE_FLOW_INFO_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    GROUP_NO = g,
                    FACTORY_ID = entity.FACTORY_ID,
                    VERSION_NO = entity.VERSION_NO,
                    PROCESS_ID = entity.PROCESS_ID,
                    PROC_SEQUENCE_NO = entity.PROC_SEQUENCE_NO,
                    PREVIOUS_PROCESS_ID = entity.PREVIOUS_PROCESS_ID,
                    NEXT_PROCESS_ID = entity.NEXT_PROCESS_ID,
                    UPDATE_USER = entity.UPDATE_USER,
                    UPDATE_DATE = entity.UPDATE_DATE,
                    PKG_PROC_DESC = entity.PKG_PROC_DESC,
                    SUB_GROUP_NO = entity.SUB_GROUP_NO,
                    PRODUCT_TYPE_ID = entity.PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID = entity.PRODUCT_PROC_TYPE_ID

                });
            }
            return result > 0 ? 1 : 0;
        }
        #endregion

        #region 修改

        public int PostEdit(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_FLOW_INFO.PostEdit(entity);

            #region 批量修改

            var packageFlowInfo = new DAL.Package.PACKAGE_FLOW_INFO();
            if (string.IsNullOrEmpty(entity.GROUP_NOS)) return result;
            foreach (var g in entity.GROUP_NOS.Split(','))
            {
                packageFlowInfo.PostEdit(new PACKAGE_FLOW_INFO_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    FACTORY_ID = entity.FACTORY_ID,
                    GROUP_NO = g,
                    PROCESS_ID = entity.PROCESS_ID,
                    UPDATE_USER = entity.UPDATE_USER,
                    UPDATE_DATE = entity.UPDATE_DATE,
                    PROC_SEQUENCE_NO = entity.PROC_SEQUENCE_NO,
                    PREVIOUS_PROCESS_ID = entity.PREVIOUS_PROCESS_ID,
                    NEXT_PROCESS_ID = entity.NEXT_PROCESS_ID,
                    PKG_PROC_DESC = entity.PKG_PROC_DESC,
                    SUB_GROUP_NO = entity.SUB_GROUP_NO
                });
            }

            #endregion

            return result;
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            #region 工序明细

            #region 参数
            var PACKAGE_PARAM_SPEC_INFO = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
            var paramSpec = PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIDAndGroupNo(entity.PACKAGE_NO, entity.GROUP_NO, entity.VERSION_NO, entity.FACTORY_ID, entity.PROCESS_ID);
            foreach (var item in paramSpec)
            {
                PACKAGE_PARAM_SPEC_INFO.PostDelete(new PACKAGE_PARAM_SPEC_INFO_Entity
                {
                    PACKAGE_NO = item.PACKAGE_NO,
                    GROUP_NO = item.GROUP_NO,
                    VERSION_NO = item.VERSION_NO,
                    FACTORY_ID = item.FACTORY_ID,
                    PARAMETER_ID = item.PARAMETER_ID,
                    SPEC_TYPE = item.SPEC_TYPE
                });
            }
            #endregion

            #region 参数设定信息
            new DAL.Package.PACKAGE_PARAM_SETTING().DeleteByProcessIDAndGroupNo(new PACKAGE_PARAM_SETTING_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region 物料类型
            new DAL.Package.PACKAGE_PROC_MATERIAL_INFO().DeleteByProcessIDAndGroupNo(new PACKAGE_PROC_MATERIAL_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region 物料编号
            new DAL.Package.PACKAGE_PROC_PN_INFO().DeleteByProcessIdAndGroupNo(new PACKAGE_PROC_PN_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region 设备类型
            new DAL.Package.PACKAGE_PROC_EQUIP_CLASS_INFO().DeleteByProcessIDAndGroupNo(new PACKAGE_PROC_EQUIP_CLASS_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region 设备编号
            new DAL.Package.PACKAGE_PROC_EQUIP_INFO().DeleteByProcessIDAndGroupNo(new PACKAGE_PROC_EQUIP_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region 附图信息
            new DAL.Package.PACKAGE_ILLUSTRATION_INFO().DeleteByProcessIDAndGroupNo(new PACKAGE_ILLUSTRATION_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #region BOM信息
            new DAL.Package.PACKAGE_BOM_SPEC_INFO().DeleteByProcessIDAndGroupNo(new PACKAGE_BOM_SPEC_INFO_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                GROUP_NO = entity.GROUP_NO,
                FACTORY_ID = entity.FACTORY_ID,
                VERSION_NO = entity.VERSION_NO,
                PROCESS_ID = entity.PROCESS_ID
            });
            #endregion

            #endregion

            return _PACKAGE_FLOW_INFO.PostDelete(entity);
        }

        public int PostDelete_Batch(PACKAGE_FLOW_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            //先删除所选分组的所有工序
            if (!string.IsNullOrEmpty(entity.PROCESS_IDS))
            {
                foreach (var p in entity.PROCESS_IDS.Split(','))
                {
                    entity.PROCESS_ID = p;
                    PostDelete(entity);
                }
            }

            //再删除其它分组的工序
            if (!string.IsNullOrEmpty(entity.GROUP_NOS))
            {
                foreach (var g in entity.GROUP_NOS.Split(','))
                {
                    entity.GROUP_NO = g;
                    if (string.IsNullOrEmpty(entity.PROCESS_IDS)) continue;
                    foreach (var p in entity.PROCESS_IDS.Split(','))
                    {
                        entity.PROCESS_ID = p;
                        PostDelete(entity);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(entity.PROCESS_IDS)) return 1;
                foreach (var p in entity.PROCESS_IDS.Split(','))
                {
                    entity.PROCESS_ID = p;
                    PostDelete(entity);
                }
            }


            return 1;
        }

        #endregion



    }
}
