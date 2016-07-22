using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_PARAM_SETTING
    {
        readonly DAL.Package.PACKAGE_PARAM_SETTING _PACKAGE_PARAM_SETTING = new DAL.Package.PACKAGE_PARAM_SETTING();
        readonly DAL.Package.PACKAGE_PARAM_SPEC_INFO _PACKAGE_PARAM_SPEC_INFO = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_PARAM_SETTING_Entity> GetData()
        {
            return _PACKAGE_PARAM_SETTING.GetData();
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PARAM_SETTING.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, FACTORY_ID, VERSION_NO, PROCESS_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentClass(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string EQUIPMENT_CLASS_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByEquipmentClass(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO, EQUIPMENT_CLASS_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentInfo(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string EQUIPMENT_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByEquipmentInfo(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO, EQUIPMENT_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByIllustrationId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string ILLUSTRATION_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByIllustrationId(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO, ILLUSTRATION_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByMaterialTypeId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string MATERIAL_TYPE_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByMaterialTypeId(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO, MATERIAL_TYPE_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByMaterialPN(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string MATERIAL_PN_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByMaterialPN(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO, MATERIAL_PN_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_PARAM_SETTING_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var result = _PACKAGE_PARAM_SETTING.PostAdd(entity);
            if (result == 0)
            {
                return result;
            }
            #region 增加参数

            //pkg参数
            var DAL_package_param_spec_info = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
            //参数
            var DAL_parameter_list = new DAL.BaseInfo.PARAMETER_LIST();
            var parameterList = DAL_parameter_list.GetDataById(entity.PARAMETER_ID, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);

            if (parameterList.Any())
            {
                var parameter = parameterList.First();
                if (entity.IS_FIRST_CHECK_PARAM == "1")//首件
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//首件
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "FAI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
                if (entity.IS_PROC_MON_PARAM == "1")//过程
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//过程
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "PMI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
                if (entity.IS_OUTPUT_PARAM == "1")//出货
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//出货
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "OI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
            }

            #endregion

            return result;
        }

        public int PostAddBatch(PACKAGE_PARAM_SETTING_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            foreach (var g in entity.GROUPS.Split(','))
            {
                foreach (var p in entity.PARAMS.Split(','))
                {
                    var param = new DAL.BaseInfo.PARAMETER_LIST().GetDataById(p, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
                    if (!param.Any()) continue;
                    var _param = param.First();
                    var paramTypeId = _param.PARAM_TYPE_ID;

                    var IS_SC_PARAM = "0";
                    decimal DISP_ORDER_IN_SC = 0;
                    switch (entity.TYPE)
                    {
                        case "PRODUCT_PROCESS":
                            var processParamInfoList = new DAL.BaseInfo.PROCESS_PARAM_INFO().GetData(entity.PROCESS_ID, p, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID);

                            if (processParamInfoList.Any())
                            {
                                IS_SC_PARAM = processParamInfoList.First().IS_SC_PARAM;
                                DISP_ORDER_IN_SC = processParamInfoList.First().DISP_ORDER_IN_SC;
                            }
                            break;
                        case "EQUIP_CLASS":
                            var equipmentClassParamList = new DAL.BaseInfo.EQUIPMENT_CLASS_PARAM_INFO().GetDataById(entity.EQUIPMENT_CLASS_ID, p, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID, "");
                            if (equipmentClassParamList.Any())
                            {
                                IS_SC_PARAM = equipmentClassParamList.First().IS_SC_PARAM;
                                DISP_ORDER_IN_SC = equipmentClassParamList.First().DISP_ORDER_NO;
                            }
                            break;
                        case "EQUIP_INFO":
                            var equipmentParamList = new DAL.BaseInfo.EQUIPMENT_PARAM_INFO().GetDataById(entity.EQUIPMENT_ID, p, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID, "");
                            if (equipmentParamList.Any())
                            {
                                IS_SC_PARAM = equipmentParamList.First().IS_SC_PARAM;
                                DISP_ORDER_IN_SC = equipmentParamList.First().DISP_ORDER_NO;
                            }
                            break;
                        case "IMG":
                            var illustrationParamList = new DAL.BaseInfo.ILLUSTRATION_PARAM_INFO().GetDataById(entity.ILLUSTRATION_ID, p, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, entity.FACTORY_ID, "");
                            if (illustrationParamList.Any())
                            {
                                DISP_ORDER_IN_SC = illustrationParamList.First().PARAM_ORDER_NO;
                            }
                            break;
                    }

                    if (entity.IS_FIRST_CHECK_PARAM == "1" ||
                entity.IS_OUTPUT_PARAM == "1" ||
                entity.IS_PROC_MON_PARAM == "1"
                )
                    {
                        _param.TARGET = string.Empty;
                        _param.USL = string.Empty;
                        _param.LSL = string.Empty;
                    }
                    PostAdd(new PACKAGE_PARAM_SETTING_Entity
                    {
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = g,
                        FACTORY_ID = entity.FACTORY_ID,
                        VERSION_NO = entity.VERSION_NO,
                        PRODUCT_TYPE_ID = entity.PRODUCT_TYPE_ID,
                        PRODUCT_PROC_TYPE_ID = entity.PRODUCT_PROC_TYPE_ID,
                        PARAMETER_ID = p,
                        PROCESS_ID = entity.PROCESS_ID,
                        PARAM_TYPE_ID = paramTypeId,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE,
                        PARAM_IO = _param.PARAM_IO,
                        IS_GROUP_PARAM = _param.IS_GROUP_PARAM,
                        IS_FIRST_CHECK_PARAM = _param.IS_FIRST_CHECK_PARAM,
                        IS_PROC_MON_PARAM = _param.IS_PROC_MON_PARAM,
                        IS_OUTPUT_PARAM = _param.IS_OUTPUT_PARAM,
                        TARGET = _param.TARGET,
                        PARAM_UNIT = _param.PARAM_UNIT,
                        PARAM_DATATYPE = _param.PARAM_DATATYPE,
                        USL = _param.USL,
                        LSL = _param.LSL,
                        SAMPLING_FREQUENCY = _param.SAMPLING_FREQUENCY,
                        CONTROL_METHOD = _param.CONTROL_METHOD,
                        IS_SC_PARAM = IS_SC_PARAM,
                        DISP_ORDER_IN_SC = DISP_ORDER_IN_SC
                    });
                }
            }
            return 1;
        }

        public int PostAddBatchAddOne(PACKAGE_PARAM_SETTING_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            foreach (var g in entity.GROUPS.Split(','))
            {
                var param = new DAL.BaseInfo.PARAMETER_LIST().GetDataById(entity.PARAMETER_ID, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
                if (!param.Any()) continue;
                var _param = param.First();
                var PARAM_TYPE_ID = _param.PARAM_TYPE_ID;
                if (entity.IS_FIRST_CHECK_PARAM == "1" ||
                entity.IS_OUTPUT_PARAM == "1" ||
                entity.IS_PROC_MON_PARAM == "1"
                )
                {
                    entity.TARGET = string.Empty;
                    entity.USL = string.Empty;
                    entity.LSL = string.Empty;
                }
                PostAdd(new PACKAGE_PARAM_SETTING_Entity
                {
                    PACKAGE_NO = entity.PACKAGE_NO,
                    GROUP_NO = g,
                    FACTORY_ID = entity.FACTORY_ID,
                    VERSION_NO = entity.VERSION_NO,
                    PARAMETER_ID = entity.PARAMETER_ID,
                    PARAM_TYPE_ID = PARAM_TYPE_ID,
                    PROCESS_ID = entity.PROCESS_ID,
                    PROC_TASK_ID = entity.PROC_TASK_ID,
                    DISP_ORDER_IN_SC = entity.DISP_ORDER_IN_SC,
                    PARAM_IO = entity.PARAM_IO,
                    IS_GROUP_PARAM = entity.IS_GROUP_PARAM,
                    IS_FIRST_CHECK_PARAM = entity.IS_FIRST_CHECK_PARAM,
                    IS_PROC_MON_PARAM = entity.IS_PROC_MON_PARAM,
                    IS_OUTPUT_PARAM = entity.IS_OUTPUT_PARAM,
                    PARAM_UNIT = entity.PARAM_UNIT,
                    PARAM_DATATYPE = entity.PARAM_DATATYPE,
                    TARGET = entity.TARGET,
                    USL = entity.USL,
                    LSL = entity.LSL,
                    ILLUSTRATION_ID = entity.ILLUSTRATION_ID,
                    UPDATE_USER = entity.UPDATE_USER,
                    UPDATE_DATE = entity.UPDATE_DATE,
                    SAMPLING_FREQUENCY = entity.SAMPLING_FREQUENCY,
                    CONTROL_METHOD = entity.CONTROL_METHOD,
                    IS_SC_PARAM = entity.IS_SC_PARAM,
                    PRODUCT_TYPE_ID = entity.PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID = entity.PRODUCT_PROC_TYPE_ID
                });
            }
            return 1;
        }
        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SETTING_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            if (entity.IS_FIRST_CHECK_PARAM == "1" ||
                entity.IS_OUTPUT_PARAM == "1" ||
                entity.IS_PROC_MON_PARAM == "1"
                )
            {
                entity.TARGET = string.Empty;
                entity.USL = string.Empty;
                entity.LSL = string.Empty;
            }
            var result = _PACKAGE_PARAM_SETTING.PostEdit(entity);
            BatchAddParamSpecInfo(entity);

            #region 批量修改

            if (string.IsNullOrEmpty(entity.GROUPS)) return result;
            var groups = entity.GROUPS.Split(',');
            foreach (var g in groups.Where(x => x != entity.GROUP_NO))
            {
                entity.GROUP_NO = g;
                ////不存在则新增
                //var param = _PACKAGE_PARAM_SETTING.GetDataById(entity.PACKAGE_NO,entity.GROUP_NO,entity.FACTORY_ID,entity.VERSION_NO,entity.PARAMETER_ID,"");
                //if (!param.Any()) PostAdd(entity);
                ////存在则编辑

                if (entity.IS_FIRST_CHECK_PARAM == "1" ||
                entity.IS_OUTPUT_PARAM == "1" ||
                entity.IS_PROC_MON_PARAM == "1"
                )
                {
                    entity.TARGET = string.Empty;
                    entity.USL = string.Empty;
                    entity.LSL = string.Empty;
                }

                _PACKAGE_PARAM_SETTING.PostEdit(entity);
                BatchAddParamSpecInfo(entity);
            }

            #endregion

            return result;
        }

        public void BatchAddParamSpecInfo(PACKAGE_PARAM_SETTING_Entity entity)
        {

            //pkg参数
            var DAL_package_param_spec_info = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
            //参数
            var DAL_parameter_list = new DAL.BaseInfo.PARAMETER_LIST();
            var parameterList = DAL_parameter_list.GetDataById(entity.PARAMETER_ID, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);

            if (parameterList.Any())
            {
                var parameter = parameterList.First();
                if (entity.IS_FIRST_CHECK_PARAM == "1")//首件
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//首件
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "FAI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
                else
                {
                    DAL_package_param_spec_info.PostDelete(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//首件
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "FAI"
                    });
                }
                if (entity.IS_PROC_MON_PARAM == "1")//过程
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//过程
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "PMI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
                else
                {
                    DAL_package_param_spec_info.PostDelete(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//过程
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "PMI"
                    });
                }
                if (entity.IS_OUTPUT_PARAM == "1")//出货
                {
                    DAL_package_param_spec_info.PostAdd(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//出货
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "OI",
                        PARAM_UNIT = entity.PARAM_UNIT,
                        TARGET = parameter.TARGET,
                        USL = parameter.USL,
                        LSL = parameter.LSL,
                        UPDATE_USER = entity.UPDATE_USER,
                        UPDATE_DATE = entity.UPDATE_DATE
                    });
                }
                else
                {
                    DAL_package_param_spec_info.PostDelete(new PACKAGE_PARAM_SPEC_INFO_Entity
                    {//出货
                        PACKAGE_NO = entity.PACKAGE_NO,
                        GROUP_NO = entity.GROUP_NO,
                        VERSION_NO = entity.VERSION_NO,
                        FACTORY_ID = entity.FACTORY_ID,
                        PARAMETER_ID = entity.PARAMETER_ID,
                        SPEC_TYPE = "OI"
                    });
                }
            }
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PARAM_SETTING_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            var result = _PACKAGE_PARAM_SETTING.PostDelete(entity);
            if (result > 0)
            {
                _PACKAGE_PARAM_SPEC_INFO.PostDeleteByParamId(new PACKAGE_PARAM_SPEC_INFO_Entity
                {
                    FACTORY_ID = entity.FACTORY_ID,
                    PACKAGE_NO = entity.PACKAGE_NO,
                    VERSION_NO = entity.VERSION_NO,
                    GROUP_NO = entity.GROUP_NO,
                    PARAMETER_ID = entity.PARAMETER_ID
                });
            }
            return result;
        }

        #endregion



    }
}
