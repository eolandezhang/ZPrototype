using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace BLL.Package
{
    public class PACKAGE_BASE_INFO
    {
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        readonly DAL.Package.PACKAGE_BASE_INFO _PACKAGE_BASE_INFO = new DAL.Package.PACKAGE_BASE_INFO();
        readonly DAL.Package.PACKAGE_GROUPS _PACKAGE_GROUPS = new DAL.Package.PACKAGE_GROUPS();
        readonly DAL.Package.PACKAGE_DESIGN_INFO _PACKAGE_DESIGN_INFO = new DAL.Package.PACKAGE_DESIGN_INFO();
        readonly DAL.Package.PACKAGE_FLOW_INFO _PACKAGE_FLOW_INFO = new DAL.Package.PACKAGE_FLOW_INFO();
        readonly DAL.Package.PACKAGE_PROC_MATERIAL_INFO _PACKAGE_PROC_MATERIAL_INFO = new DAL.Package.PACKAGE_PROC_MATERIAL_INFO();
        readonly DAL.Package.PACKAGE_PROC_PN_INFO _PACKAGE_PROC_PN_INFO = new DAL.Package.PACKAGE_PROC_PN_INFO();
        readonly DAL.Package.PACKAGE_PROC_EQUIP_CLASS_INFO _PACKAGE_PROC_EQUIP_CLASS_INFO = new DAL.Package.PACKAGE_PROC_EQUIP_CLASS_INFO();
        readonly DAL.Package.PACKAGE_PROC_EQUIP_INFO _PACKAGE_PROC_EQUIP_INFO = new DAL.Package.PACKAGE_PROC_EQUIP_INFO();
        readonly DAL.Package.PACKAGE_ILLUSTRATION_INFO _PACKAGE_ILLUSTRATION_INFO = new DAL.Package.PACKAGE_ILLUSTRATION_INFO();
        readonly DAL.Package.PACKAGE_BOM_SPEC_INFO _PACKAGE_BOM_SPEC_INFO = new DAL.Package.PACKAGE_BOM_SPEC_INFO();
        readonly DAL.Package.PACKAGE_PARAM_SETTING _PACKAGE_PARAM_SETTING = new DAL.Package.PACKAGE_PARAM_SETTING();
        readonly DAL.Package.PACKAGE_PARAM_SPEC_INFO _PACKAGE_PARAM_SPEC_INFO = new DAL.Package.PACKAGE_PARAM_SPEC_INFO();
        readonly DAL.Package.PACKAGE_PROC_GRP _PACKAGE_PROC_GRP = new DAL.Package.PACKAGE_PROC_GRP();
        readonly DAL.Package.PACKAGE_PROC_GRP_LIST _PACKAGE_PROC_GRP_LIST = new DAL.Package.PACKAGE_PROC_GRP_LIST();

        readonly DAL.BaseInfo.PRODUCT_MODEL_LIST _PRODUCT_MODEL_LIST = new DAL.BaseInfo.PRODUCT_MODEL_LIST();
        readonly DAL.BaseInfo.MATERIAL_PN_LIST _MATERIAL_PN_LIST = new DAL.BaseInfo.MATERIAL_PN_LIST();
        readonly DAL.BaseInfo.PROJ_CODE_LIST _PROJ_CODE_LIST = new DAL.BaseInfo.PROJ_CODE_LIST();
        readonly DAL.BaseInfo.CUSTOMER_CODE_LIST _CUSTOMER_CODE_LIST = new DAL.BaseInfo.CUSTOMER_CODE_LIST();
        #region 查询

        public List<PACKAGE_BASE_INFO_Entity> GetData()
        {
            return _PACKAGE_BASE_INFO.GetData();
        }
        public PACKAGE_BASE_INFO_Entity GetData(string factoryId, string packageNo, string versionNo)
        {
            var baseInfoList = _PACKAGE_BASE_INFO.GetData().Where(x => x.FACTORY_ID == factoryId && x.PACKAGE_NO == packageNo && x.VERSION_NO == versionNo).ToList();
            return baseInfoList.Any() ? baseInfoList.First() : null;
        }
        public List<PACKAGE_BASE_INFO_Entity> GetData(decimal pageSize, decimal pageNumber, string queryStr)
        {
            var user = new Settings.Users().GetCurrentUser();
            if (user == null) return null;
            string sql;
            if (Settings.Permission.IsManager())
            {
                sql = " ";
            }
            else
            {
                sql = " AND ( (PREPARED_BY = '" + user.DESCRIPTION + "' ) OR (VALID_FLAG = '1' AND STATUS = '5' AND DELETE_FLAG='0')) ";
            }
            return _PACKAGE_BASE_INFO.GetData(pageSize, pageNumber, user.FACTORY_ID, sql + queryStr);
        }
        public List<PACKAGE_BASE_INFO_Entity> GetDataByFactoryId(string factoryId)
        {
            return _PACKAGE_BASE_INFO.GetDataByFactoryId(factoryId);
        }
        public List<PACKAGE_BASE_INFO_Entity> GetDataByPackageNo(string factoryId, string packageNo)
        {
            return _PACKAGE_BASE_INFO.GetDataByPackageNo(factoryId, packageNo);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_BASE_INFO_Entity entity)
        {
            if (!Settings.Permission.IsEditor()) return -1;

            bool validInput = true;
            validInput = validInput && _PRODUCT_MODEL_LIST.GetDataValidateId(entity.BATTERY_MODEL, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!string.IsNullOrEmpty(entity.BATTERY_PARTNO))
                validInput = validInput && _MATERIAL_PN_LIST.GetDataValidateId(entity.BATTERY_PARTNO, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, "GC-S");
            validInput = validInput && _PROJ_CODE_LIST.GetDataValidateId(entity.PROJECT_CODE, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!string.IsNullOrEmpty(entity.CUSTOMER_CODE))
                validInput = validInput && _CUSTOMER_CODE_LIST.GetDataValidateId(entity.CUSTOMER_CODE, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!validInput)
            {
                return -2;
            }

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            entity.PREPARED_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_BASE_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_BASE_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;
            bool validInput = true;
            validInput = validInput && _PRODUCT_MODEL_LIST.GetDataValidateId(entity.BATTERY_MODEL, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!string.IsNullOrEmpty(entity.BATTERY_PARTNO))
                validInput = validInput && _MATERIAL_PN_LIST.GetDataValidateId(entity.BATTERY_PARTNO, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID, "GC-S");
            validInput = validInput && _PROJ_CODE_LIST.GetDataValidateId(entity.PROJECT_CODE, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!string.IsNullOrEmpty(entity.CUSTOMER_CODE))
                validInput = validInput && _CUSTOMER_CODE_LIST.GetDataValidateId(entity.CUSTOMER_CODE, entity.FACTORY_ID, entity.PRODUCT_TYPE_ID, entity.PRODUCT_PROC_TYPE_ID);
            if (!validInput)
            {
                return -2;
            }
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_BASE_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_BASE_INFO_Entity entity)
        {
            var pkg = _PACKAGE_BASE_INFO.GetDataById(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO, "");
            if (!pkg.Any()) return 0;
            if (pkg.First().STATUS != "1") return 0;
            //if (new BLL.Package.Preview().HasBeginWf(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID)) return 0;//如果开始审批，则不能删除
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;
            #region 审批
            var _PACKAGE_WF_STEP = new DAL.Package.PACKAGE_WF_STEP();
            var pkgWfStepList = _PACKAGE_WF_STEP.GetDataByPkgId(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID);
            if (pkgWfStepList.Any())
            {
                var _PACKAGE_WF_STEP_AUDITOR = new DAL.Package.PACKAGE_WF_STEP_AUDITOR();
                foreach (var pkgWfStep in pkgWfStepList)
                {
                    var pkgWfStepAuditorList = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(pkgWfStep.PACKAGE_WF_STEP_ID);
                    foreach (var pkgWfStepAuditor in pkgWfStepAuditorList)
                    {
                        _PACKAGE_WF_STEP_AUDITOR.PostDelete(pkgWfStepAuditor);
                    }
                    _PACKAGE_WF_STEP.PostDelete(pkgWfStep);
                }
            }
            #endregion


            #region 大分组
            new DAL.Package.PACKAGE_PROC_GRP_LIST().DeleteByPackageId(new PACKAGE_PROC_GRP_LIST_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                FACTORY_ID = entity.FACTORY_ID
            });
            new DAL.Package.PACKAGE_PROC_GRP().DeleteByPackageId(new PACKAGE_PROC_GRP_Entity
            {
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO,
                FACTORY_ID = entity.FACTORY_ID
            });
            #endregion

            #region 工序明细

            #region 参数
            new DAL.Package.PACKAGE_PARAM_SPEC_INFO().DeleteByPackageId(new PACKAGE_PARAM_SPEC_INFO_Entity
            {
                FACTORY_ID = entity.FACTORY_ID,
                PACKAGE_NO = entity.PACKAGE_NO,
                VERSION_NO = entity.VERSION_NO
            });
            #endregion

            #region 参数设定信息
            new DAL.Package.PACKAGE_PARAM_SETTING().DeleteByPackageId(new PACKAGE_PARAM_SETTING_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });

            #endregion

            #region 物料类型
            new DAL.Package.PACKAGE_PROC_MATERIAL_INFO().DeleteByPackageId(new PACKAGE_PROC_MATERIAL_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 物料编号
            new DAL.Package.PACKAGE_PROC_PN_INFO().DeleteByPackageId(new PACKAGE_PROC_PN_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 设备类型
            new DAL.Package.PACKAGE_PROC_EQUIP_CLASS_INFO().DeleteByPackageId(new PACKAGE_PROC_EQUIP_CLASS_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 设备编号
            new DAL.Package.PACKAGE_PROC_EQUIP_INFO().DeleteByPackageId(new PACKAGE_PROC_EQUIP_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 附图信息
            new DAL.Package.PACKAGE_ILLUSTRATION_INFO().DeleteByPackageId(new PACKAGE_ILLUSTRATION_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region BOM信息
            new DAL.Package.PACKAGE_BOM_SPEC_INFO().DeleteByPackageId(new PACKAGE_BOM_SPEC_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #endregion

            #region 设计信息
            new DAL.Package.PACKAGE_DESIGN_INFO().DeleteByPackageId(new PACKAGE_DESIGN_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 工序信息
            new DAL.Package.PACKAGE_FLOW_INFO().DeleteByPackageId(new PACKAGE_FLOW_INFO_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            #region 分组信息
            new DAL.Package.PACKAGE_GROUPS().DeleteByPackageId(new PACKAGE_GROUPS_Entity
                        {
                            FACTORY_ID = entity.FACTORY_ID,
                            PACKAGE_NO = entity.PACKAGE_NO,
                            VERSION_NO = entity.VERSION_NO
                        });
            #endregion

            //基本信息
            var result = _PACKAGE_BASE_INFO.PostDelete(entity);
            return result;
        }

        #endregion

        #region 生成版本号
        public string GenerateVersion(string factoryId, string packageNo)
        {
            var list = _PACKAGE_BASE_INFO.GetDataByPackageNo(factoryId, packageNo);
            if (!list.Any()) return "A";
            foreach (var p in list)
            {
                p.VERSION_NUM_DIGITAL = Tools.Alpha2Num(p.VERSION_NO);
            }
            var maxVersionNo = list.Max(x => x.VERSION_NUM_DIGITAL);
            return Tools.Num2Alpha(maxVersionNo + 1);
        }
        #endregion

        #region 复制
        public int PostCopy(PACKAGE_BASE_INFO_Entity entity)
        {
            if (!Settings.Permission.IsEditor()) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            entity.PREPARED_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var packageList = _PACKAGE_BASE_INFO.GetDataById(entity.ORA_PACKAGE_NO, entity.FACTORY_ID, entity.ORA_VERSION_NO, "");
            if (!packageList.Any()) return 0;
            var package = packageList.First();
            entity.VALID_FLAG = "0";
            entity.STATUS = "1";
            entity.DELETE_FLAG = "0";
            #region 基本信息
            var result = _PACKAGE_BASE_INFO.PostAdd(entity);
            if (result == 0) return 0;

            #endregion

            #region 分组信息
            var PACKAGE_GROUPS = _PACKAGE_GROUPS.GetData(package.FACTORY_ID, entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, "");
            if (!PACKAGE_GROUPS.Any()) return 1;

            foreach (var item in PACKAGE_GROUPS)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_GROUPS.PostAdd(item);
            }

            #endregion

            #region 设计信息
            var PACKAGE_DESIGN_INFO = _PACKAGE_DESIGN_INFO.GetData(package.FACTORY_ID, entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, "");
            foreach (var item in PACKAGE_DESIGN_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_DESIGN_INFO.PostAdd(item);
            }
            #endregion

            #region 工序信息
            var PACKAGE_FLOW_INFO = _PACKAGE_FLOW_INFO.GetAllDataByPackageId(entity.ORA_PACKAGE_NO, package.FACTORY_ID, entity.ORA_VERSION_NO, "");
            if (!PACKAGE_FLOW_INFO.Any()) return 1;
            foreach (var item in PACKAGE_FLOW_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_FLOW_INFO.PostAdd(item);
            }
            #endregion

            #region 大分组
            var PACKAGE_PROC_GRP = _PACKAGE_PROC_GRP.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, entity.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_GRP)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_GRP.PostAdd(item);
            }
            var PACKAGE_PROC_GRP_LIST = _PACKAGE_PROC_GRP_LIST.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, entity.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_GRP_LIST)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_GRP_LIST.PostAdd(item);
            }
            #endregion


            #region 工序明细

            #region 物料类型
            var PACKAGE_PROC_MATERIAL_INFO = _PACKAGE_PROC_MATERIAL_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_MATERIAL_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_MATERIAL_INFO.PostAdd(item);
            }
            #endregion

            #region 物料编号
            var PACKAGE_PROC_PN_INFO = _PACKAGE_PROC_PN_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_PN_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_PN_INFO.PostAdd(item);
            }
            #endregion

            #region 设备类型
            var PACKAGE_PROC_EQUIP_CLASS_INFO = _PACKAGE_PROC_EQUIP_CLASS_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_EQUIP_CLASS_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_EQUIP_CLASS_INFO.PostAdd(item);
            }
            #endregion

            #region 设备编号
            var PACKAGE_PROC_EQUIP_INFO = _PACKAGE_PROC_EQUIP_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_PROC_EQUIP_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PROC_EQUIP_INFO.PostAdd(item);
            }
            #endregion

            #region 附图信息
            var PACKAGE_ILLUSTRATION_INFO = _PACKAGE_ILLUSTRATION_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_ILLUSTRATION_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_ILLUSTRATION_INFO.PostAdd(item);
                var img = _PACKAGE_ILLUSTRATION_INFO.Get_ILLUSTRATION_DATA(entity.ORA_PACKAGE_NO, item.GROUP_NO, entity.ORA_VERSION_NO, item.FACTORY_ID, item.PROCESS_ID);
                _PACKAGE_ILLUSTRATION_INFO.PostEdit_UploadImg(new PACKAGE_ILLUSTRATION_INFO_Entity
                {
                    PACKAGE_NO = item.PACKAGE_NO,
                    GROUP_NO = item.GROUP_NO,
                    VERSION_NO = item.VERSION_NO,
                    FACTORY_ID = item.FACTORY_ID,
                    PROCESS_ID = item.PROCESS_ID,
                    ILLUSTRATION_ID = item.ILLUSTRATION_ID,
                    UploadImg = img
                });
            }

            #endregion

            #region BOM信息
            var PACKAGE_BOM_SPEC_INFO = _PACKAGE_BOM_SPEC_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, package.FACTORY_ID, entity.ORA_VERSION_NO, "");
            foreach (var item in PACKAGE_BOM_SPEC_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_BOM_SPEC_INFO.PostAdd(item);
            }
            #endregion

            #region 参数设定
            var PACKAGE_PARAM_SETTING = _PACKAGE_PARAM_SETTING.GetDataByPackageId(entity.ORA_PACKAGE_NO, package.FACTORY_ID, entity.ORA_VERSION_NO, "");
            foreach (var item in PACKAGE_PARAM_SETTING)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PARAM_SETTING.PostAdd(item);
            }
            #endregion

            #region 参数

            #endregion
            var PACKAGE_PARAM_SPEC_INFO = _PACKAGE_PARAM_SPEC_INFO.GetDataByPackageId(entity.ORA_PACKAGE_NO, entity.ORA_VERSION_NO, package.FACTORY_ID, "");
            foreach (var item in PACKAGE_PARAM_SPEC_INFO)
            {
                item.PACKAGE_NO = entity.PACKAGE_NO;
                item.VERSION_NO = entity.VERSION_NO;
                _PACKAGE_PARAM_SPEC_INFO.PostAdd(item);
            }
            #endregion

            return 1;
        }
        #endregion

        #region 导出

        public int Export(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO)
        {
            if (!Settings.Permission.IsExporter())
            {
                if (!Settings.Permission.CheckPackageRight(PACKAGE_NO, FACTORY_ID, VERSION_NO)) return -1;
            }

            var PACKAGE_BASE_INFO_List = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (PACKAGE_BASE_INFO_List.Count == 0) return 0;
            var package_base_info = PACKAGE_BASE_INFO_List.First();
            var book = new HSSFWorkbook();

            IRow row;
            ICell cell;

            #region 单元格样式

            #region 上边框，垂直居上，水平居左,10

            ICellStyle style_BT = book.CreateCellStyle();
            style_BT.BorderBottom = BorderStyle.NONE;
            style_BT.BorderLeft = BorderStyle.NONE;
            style_BT.BorderRight = BorderStyle.NONE;
            style_BT.BorderTop = BorderStyle.THIN;
            style_BT.BottomBorderColor = HSSFColor.BLACK.index;
            style_BT.LeftBorderColor = HSSFColor.BLACK.index;
            style_BT.RightBorderColor = HSSFColor.BLACK.index;
            style_BT.TopBorderColor = HSSFColor.BLACK.index;
            style_BT.WrapText = true;
            style_BT.Alignment = HorizontalAlignment.LEFT;
            style_BT.VerticalAlignment = VerticalAlignment.TOP;


            #endregion

            #region 全边框，垂直居上，水平居左,10

            ICellStyle style = book.CreateCellStyle();
            style.BorderBottom = BorderStyle.THIN;
            style.BorderLeft = BorderStyle.THIN;
            style.BorderRight = BorderStyle.THIN;
            style.BorderTop = BorderStyle.THIN;
            style.BottomBorderColor = HSSFColor.BLACK.index;
            style.LeftBorderColor = HSSFColor.BLACK.index;
            style.RightBorderColor = HSSFColor.BLACK.index;
            style.TopBorderColor = HSSFColor.BLACK.index;
            style.WrapText = true;
            style.Alignment = HorizontalAlignment.LEFT;
            style.VerticalAlignment = VerticalAlignment.TOP;

            #region 10号黑色字体
            IFont font = book.CreateFont();
            font = book.CreateFont();
            font.FontName = "Times New Roman";
            font.FontHeightInPoints = 10;
            font.Boldweight = (short)FontBoldWeight.NORMAL;
            font.Color = HSSFColor.BLACK.index;
            #endregion

            style.SetFont(font);

            #endregion

            #region 全边框，垂直居中，水平居中,10

            ICellStyle style_Center_Center = book.CreateCellStyle();
            style_Center_Center.BorderBottom = BorderStyle.THIN;
            style_Center_Center.BorderLeft = BorderStyle.THIN;
            style_Center_Center.BorderRight = BorderStyle.THIN;
            style_Center_Center.BorderTop = BorderStyle.THIN;
            style_Center_Center.BottomBorderColor = HSSFColor.BLACK.index;
            style_Center_Center.LeftBorderColor = HSSFColor.BLACK.index;
            style_Center_Center.RightBorderColor = HSSFColor.BLACK.index;
            style_Center_Center.TopBorderColor = HSSFColor.BLACK.index;
            style_Center_Center.WrapText = true;
            style_Center_Center.Alignment = HorizontalAlignment.CENTER;
            style_Center_Center.VerticalAlignment = VerticalAlignment.CENTER;

            #region 10号黑色字体
            IFont font_Center_Center = book.CreateFont();
            font_Center_Center = book.CreateFont();
            font_Center_Center.FontName = "Times New Roman";
            font_Center_Center.FontHeightInPoints = 10;
            font_Center_Center.Boldweight = (short)FontBoldWeight.NORMAL;
            font_Center_Center.Color = HSSFColor.BLACK.index;
            #endregion

            style_Center_Center.SetFont(font_Center_Center);

            #endregion

            #region 左右边框，垂直居上，水平居左,10

            ICellStyle style_BLR_FLT = book.CreateCellStyle();
            style_BLR_FLT.BorderBottom = BorderStyle.NONE;
            style_BLR_FLT.BorderLeft = BorderStyle.THIN;
            style_BLR_FLT.BorderRight = BorderStyle.THIN;
            style_BLR_FLT.BorderTop = BorderStyle.NONE;
            style_BLR_FLT.BottomBorderColor = HSSFColor.BLACK.index;
            style_BLR_FLT.LeftBorderColor = HSSFColor.BLACK.index;
            style_BLR_FLT.RightBorderColor = HSSFColor.BLACK.index;
            style_BLR_FLT.TopBorderColor = HSSFColor.BLACK.index;
            style_BLR_FLT.WrapText = true;
            style_BLR_FLT.Alignment = HorizontalAlignment.LEFT;
            style_BLR_FLT.VerticalAlignment = VerticalAlignment.TOP;

            #region 10号黑色字体
            IFont font_BLR_FLT = book.CreateFont();
            font_BLR_FLT = book.CreateFont();
            font_BLR_FLT.FontName = "Times New Roman";
            font_BLR_FLT.FontHeightInPoints = 10;
            font_BLR_FLT.Boldweight = (short)FontBoldWeight.NORMAL;
            font_BLR_FLT.Color = HSSFColor.BLACK.index;
            #endregion

            style_BLR_FLT.SetFont(font_BLR_FLT);

            #endregion

            #region 左右下边框，垂直居上，水平居左,10

            ICellStyle style_BLRB_FLT = book.CreateCellStyle();
            style_BLRB_FLT.BorderBottom = BorderStyle.THIN;
            style_BLRB_FLT.BorderLeft = BorderStyle.THIN;
            style_BLRB_FLT.BorderRight = BorderStyle.THIN;
            style_BLRB_FLT.BorderTop = BorderStyle.NONE;
            style_BLRB_FLT.BottomBorderColor = HSSFColor.BLACK.index;
            style_BLRB_FLT.LeftBorderColor = HSSFColor.BLACK.index;
            style_BLRB_FLT.RightBorderColor = HSSFColor.BLACK.index;
            style_BLRB_FLT.TopBorderColor = HSSFColor.BLACK.index;
            style_BLRB_FLT.WrapText = true;
            style_BLRB_FLT.Alignment = HorizontalAlignment.LEFT;
            style_BLRB_FLT.VerticalAlignment = VerticalAlignment.TOP;

            #region 10号黑色字体
            IFont font_BLRB_FLT = book.CreateFont();
            font_BLRB_FLT = book.CreateFont();
            font_BLRB_FLT.FontName = "Times New Roman";
            font_BLRB_FLT.FontHeightInPoints = 10;
            font_BLRB_FLT.Boldweight = (short)FontBoldWeight.NORMAL;
            font_BLRB_FLT.Color = HSSFColor.BLACK.index;
            #endregion

            style_BLRB_FLT.SetFont(font_BLRB_FLT);

            #endregion

            #region 全边框，垂直居中，水平居左,10,粗体

            ICellStyle style_Bold_Center_Left = book.CreateCellStyle();
            style_Bold_Center_Left.BorderBottom = BorderStyle.THIN;
            style_Bold_Center_Left.BorderLeft = BorderStyle.THIN;
            style_Bold_Center_Left.BorderRight = BorderStyle.THIN;
            style_Bold_Center_Left.BorderTop = BorderStyle.THIN;
            style_Bold_Center_Left.BottomBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Left.LeftBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Left.RightBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Left.TopBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Left.WrapText = true;
            style_Bold_Center_Left.Alignment = HorizontalAlignment.LEFT;
            style_Bold_Center_Left.VerticalAlignment = VerticalAlignment.CENTER;
            style_Bold_Center_Left.FillForegroundColor = HSSFColor.GREY_25_PERCENT.index;
            style_Bold_Center_Left.FillPattern = FillPatternType.FINE_DOTS;
            style_Bold_Center_Left.FillBackgroundColor = HSSFColor.WHITE.index;

            #region 10号黑色字体
            IFont font_Bold_Center_Left = book.CreateFont();
            font_Bold_Center_Left = book.CreateFont();
            font_Bold_Center_Left.FontName = "Times New Roman";
            font_Bold_Center_Left.FontHeightInPoints = 10;
            font_Bold_Center_Left.Boldweight = (short)FontBoldWeight.BOLD;
            font_Bold_Center_Left.Color = HSSFColor.BLACK.index;
            #endregion

            style_Bold_Center_Left.SetFont(font_Bold_Center_Left);

            #endregion

            #region 全边框，垂直居中，水平居中,10,粗体

            ICellStyle style_Bold_Center_Center = book.CreateCellStyle();
            style_Bold_Center_Center.BorderBottom = BorderStyle.THIN;
            style_Bold_Center_Center.BorderLeft = BorderStyle.THIN;
            style_Bold_Center_Center.BorderRight = BorderStyle.THIN;
            style_Bold_Center_Center.BorderTop = BorderStyle.THIN;
            style_Bold_Center_Center.BottomBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Center.LeftBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Center.RightBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Center.TopBorderColor = HSSFColor.BLACK.index;
            style_Bold_Center_Center.WrapText = true;
            style_Bold_Center_Center.Alignment = HorizontalAlignment.CENTER;
            style_Bold_Center_Center.VerticalAlignment = VerticalAlignment.CENTER;
            //style_Bold_Center_Center.FillForegroundColor = HSSFColor.GREY_25_PERCENT.index;
            //style_Bold_Center_Center.FillPattern = FillPatternType.LEAST_DOTS;
            //style_Bold_Center_Center.FillBackgroundColor = HSSFColor.WHITE.index;

            #region 10号黑色字体
            IFont font_Bold_Center_Center = book.CreateFont();
            font_Bold_Center_Center = book.CreateFont();
            font_Bold_Center_Center.FontName = "Times New Roman";
            font_Bold_Center_Center.FontHeightInPoints = 10;
            font_Bold_Center_Center.Boldweight = (short)FontBoldWeight.BOLD;
            font_Bold_Center_Center.Color = HSSFColor.BLACK.index;
            #endregion

            style_Bold_Center_Center.SetFont(font_Bold_Center_Center);

            #endregion

            #region 全边框，垂直居中，水平居中，16，粗体
            ICellStyle style_BigTitle = book.CreateCellStyle();
            style_BigTitle.BorderBottom = BorderStyle.THIN;
            style_BigTitle.BorderLeft = BorderStyle.THIN;
            style_BigTitle.BorderRight = BorderStyle.THIN;
            style_BigTitle.BorderTop = BorderStyle.THIN;
            style_BigTitle.BottomBorderColor = HSSFColor.BLACK.index;
            style_BigTitle.LeftBorderColor = HSSFColor.BLACK.index;
            style_BigTitle.RightBorderColor = HSSFColor.BLACK.index;
            style_BigTitle.TopBorderColor = HSSFColor.BLACK.index;
            style_BigTitle.WrapText = true;
            style_BigTitle.Alignment = HorizontalAlignment.CENTER;
            style_BigTitle.VerticalAlignment = VerticalAlignment.CENTER;
            #region 大标题
            IFont font_BigTitle = book.CreateFont();
            font_BigTitle = book.CreateFont();
            font_BigTitle.FontName = "Times New Roman";
            font_BigTitle.FontHeightInPoints = 16;
            font_BigTitle.Boldweight = (short)FontBoldWeight.BOLD;
            font_BigTitle.Color = HSSFColor.BLACK.index;
            #endregion
            style_BigTitle.SetFont(font_BigTitle);
            #endregion


            #endregion

            #region Cover
            ISheet sheet = book.CreateSheet("Cover");
            sheet.DisplayGridlines = false;

            #region 列宽
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 20 * 256);
            #endregion

            int rownum = 0;
            #region 大标题

            row = sheet.CreateRow(rownum);
            row.Height = 2 * 256;

            #region 图片

            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            HSSFClientAnchor anchor;
            HSSFPicture picture;
            cell = row.CreateCell(0); cell.CellStyle = style;
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 0));
            anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 1, 2);
            anchor.AnchorType = 2;
            picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                book));
            picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

            #endregion

            #region 标题文字

            cell = row.CreateCell(1); cell.CellStyle = style_BigTitle;
            cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
            cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
            cell = row.CreateCell(3); cell.CellStyle = style_BigTitle;
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 1, 3));

            #endregion

            #region 文件编号

            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
            cell.SetCellValue("DOC NO.");
            cell = row.CreateCell(5); cell.CellStyle = style_Center_Center;
            cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

            #endregion

            #region 生效日期

            row = sheet.CreateRow(++rownum);
            row.Height = 2 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            for (int i = 1; i <= 3; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
            cell.SetCellValue("EFF.DATE");
            cell = row.CreateCell(5); cell.CellStyle = style_Center_Center;
            cell.SetCellValue(package_base_info.EFFECT_DATE);

            #endregion

            #endregion

            #region 基本信息

            row = sheet.CreateRow(++rownum);
            row.Height = 15 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            var sb = new StringBuilder();
            sb.AppendFormat("{0}：{1}", "厂别", package_base_info.FACTORY_ID);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "Package Type(试验类型)", package_base_info.PACKAGE_TYPE_ID);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "订单类型", package_base_info.ORDER_TYPE + package_base_info.SO_NO);
            sb.Append("\n");
            sb.Append("\n");
            sb.AppendFormat("{0}：{1}", "产品类型", package_base_info.PRODUCT_TYPE_ID);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "工艺类型", package_base_info.PRODUCT_PROC_TYPE_ID);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "电池类型", package_base_info.BATTERY_TYPE);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "品种", package_base_info.BATTERY_MODEL);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "层数", package_base_info.BATTERY_LAYERS);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "数量", package_base_info.BATTERY_QTY);
            sb.Append("\n");
            sb.Append("\n");
            sb.AppendFormat("{0}：{1}", "项目代码", package_base_info.PROJECT_CODE);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "电池编号", package_base_info.BATTERY_PARTNO);
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "客户代码", package_base_info.CUSTOMER_CODE);
            sb.Append("\n");
            sb.Append("\n");
            sb.AppendFormat("{0}：{1}", "出货日期", package_base_info.OUTPUT_TARGET_DATE);
            sb.Append("\n");
            sb.Append("\n");
            sb.AppendFormat("{0}：{1}", "是否紧急", package_base_info.IS_URGENT == "1" ? "YES" : "NO");
            sb.Append("     ");
            sb.AppendFormat("{0}：{1}", "紧急原因", package_base_info.REASON_FORURGENT);
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("分组信息：");
            #region 分组信息
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            foreach (var item in package_groups) { sb.AppendFormat("{0}：{1}   ", item.GROUP_NO, item.GROUP_QTY.ToString()); }
            sb.Append("\n");
            sb.Append("分组说明：");
            sb.Append(package_base_info.GROUPS_PURPOSE);
            #endregion
            cell.SetCellValue(sb.ToString());
            for (var i = 1; i <= 5; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));


            #endregion

            #region 设计信息

            foreach (var item in package_groups)
            {
                #region 标题
                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                cell.SetCellValue(item.GROUP_NO + " 组的设计信息");
                for (int i = 1; i <= 5; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left; }
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
                #endregion
                var package_design_info = _PACKAGE_DESIGN_INFO.GetDataById(item.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO);
                foreach (var d in package_design_info)
                {
                    #region 内容
                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("电池容量");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.CELL_CAP + "mAh");
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("起始电压");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.BEG_VOL + "V");
                    cell = row.CreateCell(4); cell.CellStyle = style;
                    cell.SetCellValue("截至电压");
                    cell = row.CreateCell(5); cell.CellStyle = style;
                    cell.SetCellValue(d.END_VOL + "V");

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极材料");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_STUFF_ID);
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极材料");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_STUFF_ID);
                    cell = row.CreateCell(4); cell.CellStyle = style;
                    cell.SetCellValue("隔离膜材料");
                    cell = row.CreateCell(5); cell.CellStyle = style;
                    cell.SetCellValue(d.SEPARATOR_ID);

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极配方");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_FORMULA_ID);
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极配方");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_FORMULA_ID);
                    cell = row.CreateCell(4); cell.CellStyle = style;
                    cell.SetCellValue("电解液配方");
                    cell = row.CreateCell(5); cell.CellStyle = style;
                    cell.SetCellValue(d.ELECTROLYTE_ID);

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极涂布重量");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_COATING_WEIGHT + "g/1540.25mm²");
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极涂布重量");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_COATING_WEIGHT + "g/1540.25mm²");
                    cell = row.CreateCell(4); cell.CellStyle = style;
                    cell.SetCellValue("注液量");
                    cell = row.CreateCell(5); cell.CellStyle = style;
                    cell.SetCellValue(d.INJECTION_QTY + "g");

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极压实密度");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_DENSITY + "g/cm³");
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极压实密度");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_DENSITY + "g/cm³");
                    cell = row.CreateCell(4); cell.CellStyle = style;
                    cell.SetCellValue("保液系数");
                    cell = row.CreateCell(5); cell.CellStyle = style;
                    cell.SetCellValue(d.LIQUID_PER.ToString());

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极集流体材料");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_FOIL_ID);
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极集流体材料");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_FOIL_ID);
                    for (int i = 4; i <= 5; i++) { cell = row.CreateCell(i); cell.CellStyle = style; }
                    sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 5));

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("阳极集流体厚度");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.ANODE_THICKNESS + "mm");
                    cell = row.CreateCell(2); cell.CellStyle = style;
                    cell.SetCellValue("阴极集流体厚度");
                    cell = row.CreateCell(3); cell.CellStyle = style;
                    cell.SetCellValue(d.CATHODE_THICKNESS + "mm");
                    for (int i = 4; i <= 5; i++) { cell = row.CreateCell(i); cell.CellStyle = style; }
                    sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 5));

                    row = sheet.CreateRow(++rownum);
                    row.Height = 1 * 256;
                    cell = row.CreateCell(0); cell.CellStyle = style;
                    cell.SetCellValue("补充说明");
                    cell = row.CreateCell(1); cell.CellStyle = style;
                    cell.SetCellValue(d.MODEL_DESC);
                    for (int i = 2; i <= 5; i++) { cell = row.CreateCell(i); cell.CellStyle = style; }
                    sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 5));
                    #endregion
                }
                row = sheet.CreateRow(++rownum);
            }



            #endregion

            #region 分组参数
            foreach (var g in package_groups)
            {
                #region 标题
                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                cell.SetCellValue(g.GROUP_NO + " 组的分组参数");
                for (int i = 1; i <= 5; i++)
                {
                    cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left;
                }
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
                row = sheet.CreateRow(++rownum);
                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("工序");
                cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("参数名");
                cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("目标值");
                cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("上限");
                cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("下限");
                cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("相关编号");
                #endregion

                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    bool pflag = true;
                    #region 产品参数，工艺参数
                    //没有定义参数类型的
                    var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, p.GROUP_NO, FACTORY_ID, VERSION_NO, p.PROCESS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1 AND A.IS_GROUP_PARAM='1' ");
                    //定义了参数类型的
                    var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIDAndGroupNoWithSetting(PACKAGE_NO, p.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID, " AND A.IS_GROUP_PARAM='1' AND SPEC_TYPE='OI' ");
                    var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                    foreach (var x in package_param_spec_info)
                    {
                        list.Add(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = x.PACKAGE_NO,
                            GROUP_NO = x.GROUP_NO,
                            FACTORY_ID = x.FACTORY_ID,
                            VERSION_NO = x.VERSION_NO,
                            PARAMETER_ID = x.PARAMETER_ID,
                            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                            SPEC_TYPE = x.SPEC_TYPE,
                            PARAM_UNIT = x.PARAM_UNIT,
                            TARGET = x.TARGET,
                            USL = x.USL,
                            LSL = x.LSL,
                            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                            CONTROL_METHOD = x.CONTROL_METHOD,
                            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                            PARAM_DESC = x.PARAM_DESC,
                            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                        });
                    }
                    package_param_setting.AddRange(list);
                    if (package_param_setting.Count > 0)
                    {
                        package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
                    }

                    foreach (var item in package_param_setting)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                        if (pflag)
                        {
                            cell.SetCellValue(p.PROCESS_DESC);
                        }
                        pflag = false;
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell.SetCellValue(item.PARAM_DESC);
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        cell.SetCellValue(item.TARGET);
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.USL);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        cell.SetCellValue(item.LSL);
                        cell = row.CreateCell(5); cell.CellStyle = style;
                        cell.SetCellValue("");
                    }


                    #endregion

                    #region 附图参数
                    var img = "";
                    var package_illustration_info = _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID);
                    foreach (var item in package_illustration_info)
                    {
                        img = item.ILLUSTRATION_ID + '\n';
                        //没有定义参数类型的
                        package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByIllustrationId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.ILLUSTRATION_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByIllustrationId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.ILLUSTRATION_ID, "");
                        list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);

                        foreach (var param in package_param_setting)
                        {
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            if (pflag)
                            {
                                cell.SetCellValue(p.PROCESS_DESC);
                            }
                            pflag = false;
                            cell = row.CreateCell(1); cell.CellStyle = style;
                            cell.SetCellValue(param.PARAM_DESC);
                            cell = row.CreateCell(2); cell.CellStyle = style;
                            cell.SetCellValue(param.TARGET);
                            cell = row.CreateCell(3); cell.CellStyle = style;
                            cell.SetCellValue(param.USL);
                            cell = row.CreateCell(4); cell.CellStyle = style;
                            cell.SetCellValue(param.LSL);
                            cell = row.CreateCell(5); cell.CellStyle = style;
                            cell.SetCellValue(img.TrimEnd('\n'));
                        }
                    }
                    #endregion

                }
                row = sheet.CreateRow(++rownum);
                for (int i = 0; i <= 5; i++)
                {
                    cell = row.CreateCell(i); cell.CellStyle = style_BT;
                }
            }
            #endregion

            #region 变更信息
            row = sheet.CreateRow(++rownum);
            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
            cell.SetCellValue("产品变更");
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row.Height = 7 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            cell.SetCellValue(package_base_info.PRODUCT_CHANGE_HL);
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row = sheet.CreateRow(++rownum);
            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
            cell.SetCellValue("工艺变更");
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row.Height = 7 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            cell.SetCellValue(package_base_info.PROCESS_CHANGE_HL);
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row = sheet.CreateRow(++rownum);
            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
            cell.SetCellValue("物料变更");
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row.Height = 7 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            cell.SetCellValue(package_base_info.MATERIAL_CHANGE_HL);
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row = sheet.CreateRow(++rownum);
            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
            cell.SetCellValue("其它变更");
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Left;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            row = sheet.CreateRow(++rownum);
            row.Height = 7 * 256;
            cell = row.CreateCell(0); cell.CellStyle = style;
            cell.SetCellValue(package_base_info.OTHER_CHANGE_HL);
            for (int i = 1; i < 6; i++)
            {
                cell = row.CreateCell(i); cell.CellStyle = style;
            }
            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 5));
            #endregion

            #endregion

            #region 工序及参数
            foreach (var item in package_groups)
            {
                sheet = book.CreateSheet("正文-" + item.GROUP_NO);
                sheet.DisplayGridlines = false;
                sheet.TabColorIndex = IndexedColors.GOLD.Index;
                rownum = 0;
                #region 列宽
                sheet.SetColumnWidth(0, 6 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 6 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);
                sheet.SetColumnWidth(13, 20 * 256);
                #endregion

                #region 大标题

                row = sheet.CreateRow(rownum);
                row.Height = 2 * 256;

                #region 图片

                cell = row.CreateCell(0);
                cell.CellStyle = style;

                patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2, 2);
                anchor.AnchorType = 3;
                picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                    HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                    book));
                picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

                #endregion

                #region 标题文字

                cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
                cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
                for (int i = 3; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 11));

                #endregion

                #region 文件编号

                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("DOC NO.");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

                #endregion

                #region 生效日期

                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style;
                cell = row.CreateCell(1); cell.CellStyle = style;
                for (int i = 2; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("EFF.DATE");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(package_base_info.EFFECT_DATE);

                #endregion

                #endregion

                #region 标题
                row = sheet.CreateRow(++rownum);
                row.Height = 3 * 128;

                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("组别");
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("工序");
                cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("控制点");
                cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("规格");
                for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("频率/抽样数量");
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("检查和控制方法");
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("工序说明");

                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                row = sheet.CreateRow(++rownum);
                row.Height = 3 * 128;

                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("编号");
                cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("工序名");
                cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("产品参数");
                cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("工序参数");
                cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("类型");
                cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("目标值");
                cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("上限值");
                cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("下限值");
                for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                #endregion

                #region 内容
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(item.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    row = sheet.CreateRow(++rownum);
                    cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                    cell.SetCellValue(p.GROUP_NO);
                    cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                    cell.SetCellValue(p.PROCESS_ID);
                    cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                    cell.SetCellValue(p.PROCESS_NAME);
                    for (int i = 3; i < 11; i++)
                    {
                        cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                    }
                    cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                    cell.SetCellValue(p.PKG_PROC_DESC == null ? "" : p.PKG_PROC_DESC);
                    cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                    cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                    //没有定义参数类型的
                    var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, item.GROUP_NO, FACTORY_ID, VERSION_NO, p.PROCESS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                    //定义了参数类型的
                    var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIDAndGroupNoWithSetting(PACKAGE_NO, item.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID, "");
                    var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                    foreach (var x in package_param_spec_info)
                    {
                        list.Add(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = x.PACKAGE_NO,
                            GROUP_NO = x.GROUP_NO,
                            FACTORY_ID = x.FACTORY_ID,
                            VERSION_NO = x.VERSION_NO,
                            PARAMETER_ID = x.PARAMETER_ID,
                            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                            SPEC_TYPE = x.SPEC_TYPE,
                            PARAM_UNIT = x.PARAM_UNIT,
                            TARGET = x.TARGET,
                            USL = x.USL,
                            LSL = x.LSL,
                            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                            CONTROL_METHOD = x.CONTROL_METHOD,
                            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                            PARAM_DESC = x.PARAM_DESC,
                            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                        });
                    }
                    package_param_setting.AddRange(list);
                    if (package_param_setting.Count > 0)
                    {
                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                    }
                    sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting.Count, 11, 13));
                    foreach (var s in package_param_setting)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                        cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                        cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                        switch (s.PARAM_TYPE_ID)
                        {
                            case "PROCESS":
                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);
                                break;
                            case "PRODUCT":
                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                break;
                        }


                        cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                        string SPEC_TYPE = "";
                        switch (s.SPEC_TYPE)
                        {
                            case "":
                                SPEC_TYPE = ""; break;
                            case "FAI":
                                SPEC_TYPE = "首件"; break;
                            case "PMI":
                                SPEC_TYPE = "过程"; break;
                            case "OI":
                                SPEC_TYPE = "出货"; break;
                            default:
                                SPEC_TYPE = ""; break;
                        }
                        cell.SetCellValue(SPEC_TYPE);
                        cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                        cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                        cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                        cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue(s.SAMPLING_FREQUENCY);
                        cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue(s.CONTROL_METHOD);
                        cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                        cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                        cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                        cell.SetCellValue("");
                    }
                    row = sheet.CreateRow(++rownum);
                    for (int i = 0; i <= 13; i++)
                    {
                        cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                    }

                }

                #endregion
            }
            #endregion

            #region 物料

            foreach (var g in package_groups)
            {
                sheet = book.CreateSheet("物料-" + g.GROUP_NO);
                sheet.DisplayGridlines = false;
                sheet.TabColorIndex = IndexedColors.CORAL.Index;
                rownum = 0;
                #region 列宽
                sheet.SetColumnWidth(0, 6 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 6 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);
                sheet.SetColumnWidth(13, 20 * 256);
                #endregion

                #region 大标题

                row = sheet.CreateRow(rownum);
                row.Height = 2 * 256;

                #region 图片

                cell = row.CreateCell(0);
                cell.CellStyle = style;

                patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2, 2);
                anchor.AnchorType = 3;
                picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                    HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                    book));
                picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

                #endregion

                #region 标题文字

                cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
                cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
                for (int i = 3; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 11));

                #endregion

                #region 文件编号

                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("DOC NO.");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

                #endregion

                #region 生效日期

                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style;
                cell = row.CreateCell(1); cell.CellStyle = style;
                for (int i = 2; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("EFF.DATE");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(package_base_info.EFFECT_DATE);

                #endregion

                #endregion

                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    #region 物料类型
                    var package_proc_material_info = _PACKAGE_PROC_MATERIAL_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);

                    foreach (var mt in package_proc_material_info)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("类型编号");
                        cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Left;
                        cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("类型名称");
                        cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));
                        cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("物料编号");
                        cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 6));
                        cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("物料编号名称");
                        cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 7, 8));

                        var m = new List<PACKAGE_PROC_MATERIAL_INFO_Entity>();
                        m.Add(mt);
                        var pn = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID).Where(x => x.MATERIAL_TYPE_ID == mt.MATERIAL_TYPE_ID).ToList();//找到此物料类型的编号
                        foreach (var item in pn)
                        {
                            m.Add(new PACKAGE_PROC_MATERIAL_INFO_Entity
                            {
                                MATERIAL_PN_ID = item.MATERIAL_PN_ID,
                                MATERIAL_PN_DESC = item.MATERIAL_PN_DESC
                            });
                        }
                        foreach (var m1 in m)
                        {
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style;
                            cell.SetCellValue(m1.MATERIAL_TYPE_ID);
                            cell = row.CreateCell(1); cell.CellStyle = style;
                            cell = row.CreateCell(2); cell.CellStyle = style;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                            cell = row.CreateCell(3); cell.CellStyle = style;
                            cell.SetCellValue(m1.MATERIAL_TYPE_DESC);
                            cell = row.CreateCell(4); cell.CellStyle = style;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));
                            cell = row.CreateCell(5); cell.CellStyle = style;
                            cell.SetCellValue(m1.MATERIAL_PN_ID);
                            cell = row.CreateCell(6); cell.CellStyle = style;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 6));
                            cell = row.CreateCell(7); cell.CellStyle = style;
                            cell.SetCellValue(m1.MATERIAL_PN_DESC);
                            cell = row.CreateCell(8); cell.CellStyle = style;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 7, 8));
                        }


                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByMaterialTypeId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, mt.MATERIAL_TYPE_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialTypeId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, mt.MATERIAL_TYPE_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        //package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                        if (package_param_setting.Count > 0)
                        {
                            #region 标题
                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("组别");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("控制点");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("规格");
                            for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                            cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("频率/抽样数量");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                            cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("检查和控制方法");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                            cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序说明");

                            cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("编号");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序名");
                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数编号");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数名称");
                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("类型");
                            cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("目标值");
                            cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("上限值");
                            cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("下限值");
                            for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            #endregion

                            #region 物料类型参数
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.GROUP_NO);
                            cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_ID);
                            cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_NAME);
                            for (int i = 3; i < 11; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                            }
                            cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PKG_PROC_DESC);
                            cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                            cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting.Count, 11, 13));
                            foreach (var s in package_param_setting)
                            {
                                row = sheet.CreateRow(++rownum);
                                cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");


                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAMETER_ID);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);


                                cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                cell.SetCellValue(SPEC_TYPE);
                                cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.SAMPLING_FREQUENCY);
                                cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.CONTROL_METHOD);
                                cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                            }
                            row = sheet.CreateRow(++rownum);
                            for (int i = 0; i < 14; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                            }
                            #endregion
                        }
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                    }



                    #endregion

                    #region 物料编号
                    var package_proc_pn_info = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
                    List<PACKAGE_PROC_PN_INFO_Entity> lst = new List<PACKAGE_PROC_PN_INFO_Entity>();
                    foreach (var item in package_proc_pn_info)
                    {
                        if (!package_proc_material_info.Exists(x => x.MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID))
                        {
                            lst.Add(item);
                        }
                    }
                    foreach (var item in lst)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("物料编号");
                        cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Left;
                        cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("物料编号名称");
                        cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style;
                        cell.SetCellValue(item.MATERIAL_PN_ID);
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.MATERIAL_PN_DESC);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));


                        //没有定义参数类型的
                        var package_param_setting1 = _PACKAGE_PARAM_SETTING.GetDataByMaterialPN(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.MATERIAL_PN_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info1 = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialPNId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.MATERIAL_PN_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info1)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting1.AddRange(list);

                        var paramlst = (from x in package_param_setting1
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting1 = paramlst;

                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                        if (package_param_setting1.Count > 0)
                        {
                            #region 标题
                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("组别");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("控制点");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("规格");
                            for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                            cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("频率/抽样数量");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                            cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("检查和控制方法");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                            cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序说明");

                            cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("编号");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序名");
                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数编号");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数名称");
                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("类型");
                            cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("目标值");
                            cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("上限值");
                            cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("下限值");
                            for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            #endregion

                            #region 物料类型参数
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.GROUP_NO);
                            cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_ID);
                            cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_NAME);
                            for (int i = 3; i < 11; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                            }
                            cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PKG_PROC_DESC);
                            cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                            cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting1.Count, 11, 13));
                            foreach (var s in package_param_setting1)
                            {
                                row = sheet.CreateRow(++rownum);
                                cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");


                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAMETER_ID);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);


                                cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                cell.SetCellValue(SPEC_TYPE);
                                cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.SAMPLING_FREQUENCY);
                                cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.CONTROL_METHOD);
                                cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                            }
                            row = sheet.CreateRow(++rownum);
                            for (int i = 0; i < 14; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                            }
                            #endregion
                        }
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                    }
                    #endregion
                }
            }

            #endregion

            #region 设备
            foreach (var g in package_groups)
            {
                sheet = book.CreateSheet("设备-" + g.GROUP_NO);
                sheet.DisplayGridlines = false;
                sheet.TabColorIndex = IndexedColors.AQUA.Index;
                rownum = 0;
                #region 列宽
                sheet.SetColumnWidth(0, 6 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 6 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);
                sheet.SetColumnWidth(13, 20 * 256);
                #endregion

                #region 大标题

                row = sheet.CreateRow(rownum);
                row.Height = 2 * 256;

                #region 图片

                cell = row.CreateCell(0);
                cell.CellStyle = style;

                patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2, 2);
                anchor.AnchorType = 3;
                picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                    HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                    book));
                picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

                #endregion

                #region 标题文字

                cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
                cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
                for (int i = 3; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 11));

                #endregion

                #region 文件编号

                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("DOC NO.");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

                #endregion

                #region 生效日期

                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style;
                cell = row.CreateCell(1); cell.CellStyle = style;
                for (int i = 2; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("EFF.DATE");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(package_base_info.EFFECT_DATE);

                #endregion

                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    var package_proc_equip_class_info = _PACKAGE_PROC_EQUIP_CLASS_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID, "");
                    var package_proc_equip_info = _PACKAGE_PROC_EQUIP_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
                    #region 设备类型

                    foreach (var item in package_proc_equip_class_info)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("设备类型编号");
                        cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Left;
                        cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("设备类型编号名称");
                        cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style;
                        cell.SetCellValue(item.EQUIPMENT_CLASS_ID);
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.EQUIPMENT_CLASS_DESC);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentClass(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_CLASS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentClass(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_CLASS_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;

                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                        if (package_param_setting.Count > 0)
                        {
                            #region 标题
                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("组别");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("控制点");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("规格");
                            for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                            cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("频率/抽样数量");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                            cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("检查和控制方法");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                            cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序说明");

                            cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("编号");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序名");
                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数编号");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数名称");
                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("类型");
                            cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("目标值");
                            cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("上限值");
                            cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("下限值");
                            for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            #endregion

                            #region 物料类型参数
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.GROUP_NO);
                            cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_ID);
                            cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_NAME);
                            for (int i = 3; i < 11; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                            }
                            cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PKG_PROC_DESC);
                            cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                            cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting.Count, 11, 13));
                            foreach (var s in package_param_setting)
                            {
                                row = sheet.CreateRow(++rownum);
                                cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");


                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAMETER_ID);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);


                                cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                cell.SetCellValue(SPEC_TYPE);
                                cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.SAMPLING_FREQUENCY);
                                cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.CONTROL_METHOD);
                                cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                            }
                            row = sheet.CreateRow(++rownum);
                            for (int i = 0; i < 14; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                            }
                            #endregion
                        }
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                    }
                    #endregion

                    #region 设备编号
                    foreach (var item in package_proc_equip_info)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("设备编号");
                        cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Left;
                        cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("设备编号名称");
                        cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style;
                        cell.SetCellValue(item.EQUIPMENT_ID);
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.EQUIPMENT_DESC);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentInfo(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentInfo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;

                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                        if (package_param_setting.Count > 0)
                        {
                            #region 标题
                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("组别");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("控制点");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("规格");
                            for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                            cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("频率/抽样数量");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                            cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("检查和控制方法");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                            cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序说明");

                            cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("编号");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序名");
                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数编号");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数名称");
                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("类型");
                            cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("目标值");
                            cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("上限值");
                            cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("下限值");
                            for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            #endregion

                            #region 设备参数
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.GROUP_NO);
                            cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_ID);
                            cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_NAME);
                            for (int i = 3; i < 11; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                            }
                            cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PKG_PROC_DESC);
                            cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                            cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting.Count, 11, 13));
                            foreach (var s in package_param_setting)
                            {
                                row = sheet.CreateRow(++rownum);
                                cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");


                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAMETER_ID);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);


                                cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                cell.SetCellValue(SPEC_TYPE);
                                cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.SAMPLING_FREQUENCY);
                                cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.CONTROL_METHOD);
                                cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                            }
                            row = sheet.CreateRow(++rownum);
                            for (int i = 0; i < 14; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                            }
                            #endregion
                        }
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region 附图
            foreach (var g in package_groups)
            {
                sheet = book.CreateSheet("附图-" + g.GROUP_NO);
                sheet.DisplayGridlines = false;
                sheet.TabColorIndex = IndexedColors.BLUE.Index;
                rownum = 0;
                #region 列宽
                sheet.SetColumnWidth(0, 6 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 6 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);
                sheet.SetColumnWidth(13, 20 * 256);
                #endregion

                #region 大标题

                row = sheet.CreateRow(rownum);
                row.Height = 2 * 256;

                #region 图片

                cell = row.CreateCell(0);
                cell.CellStyle = style;

                patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2, 2);
                anchor.AnchorType = 3;
                picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                    HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                    book));
                picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

                #endregion

                #region 标题文字

                cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
                cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
                for (int i = 3; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 11));

                #endregion

                #region 文件编号

                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("DOC NO.");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

                #endregion

                #region 生效日期

                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style;
                cell = row.CreateCell(1); cell.CellStyle = style;
                for (int i = 2; i <= 11; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("EFF.DATE");
                cell = row.CreateCell(13); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(package_base_info.EFFECT_DATE);

                #endregion

                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    var package_illustration_info = _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID);


                    foreach (var item in package_illustration_info)
                    {
                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("图片类型编号");
                        cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Left;
                        cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Left;
                        cell.SetCellValue("名称");
                        cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Left;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style;
                        cell.SetCellValue(item.ILLUSTRATION_ID);
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.ILLUSTRATION_DESC);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));


                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0);
                        if (item.ILLUSTRATION_DATA != null)
                        {
                            patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                            anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, rownum, 2, rownum + 2);
                            anchor.AnchorType = 3;
                            picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImageByte(
                                item.ILLUSTRATION_DATA,
                                book));
                            picture.LineStyle = LineStyle.None;
                            picture.Resize();
                        }

                        for (int i = 1; i < (item.IMG_LENGTH == 0 ? 20 : item.IMG_LENGTH); i++)
                        {
                            row = sheet.CreateRow(++rownum);
                        }


                        //没有定义参数类型的
                        var package_param_setting1 = _PACKAGE_PARAM_SETTING.GetDataByIllustrationId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.ILLUSTRATION_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info1 = _PACKAGE_PARAM_SPEC_INFO.GetDataByIllustrationId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.ILLUSTRATION_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info1)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting1.AddRange(list);

                        var paramlst = (from x in package_param_setting1
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting1 = paramlst;

                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                        if (package_param_setting1.Count > 0)
                        {
                            #region 标题
                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("组别");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 0, 0));

                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 1, 2));

                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("控制点");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));

                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("规格");
                            for (int i = 6; i <= 8; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 5, 8));

                            cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("频率/抽样数量");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 9, 9));

                            cell = row.CreateCell(10); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("检查和控制方法");
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 10, 10));

                            cell = row.CreateCell(11); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序说明");

                            cell = row.CreateCell(12); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(13); cell.CellStyle = style_Bold_Center_Center;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1, 11, 13));

                            row = sheet.CreateRow(++rownum);
                            row.Height = 3 * 128;

                            cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                            cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("编号");
                            cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("工序名");
                            cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数编号");
                            cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("参数名称");
                            cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("类型");
                            cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("目标值");
                            cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("上限值");
                            cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                            cell.SetCellValue("下限值");
                            for (int i = 9; i <= 13; i++) { cell = row.CreateCell(i); cell.CellStyle = style_Bold_Center_Center; }
                            #endregion

                            #region 物料类型参数
                            row = sheet.CreateRow(++rownum);
                            cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.GROUP_NO);
                            cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_ID);
                            cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PROCESS_NAME);
                            for (int i = 3; i < 11; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLR_FLT;
                            }
                            cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                            cell.SetCellValue(p.PKG_PROC_DESC);
                            cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                            cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                            sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum + 1 + package_param_setting1.Count, 11, 13));
                            foreach (var s in package_param_setting1)
                            {
                                row = sheet.CreateRow(++rownum);
                                cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(1); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(2); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");


                                cell = row.CreateCell(3); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAMETER_ID);
                                cell = row.CreateCell(4); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.PARAM_DESC);


                                cell = row.CreateCell(5); cell.CellStyle = style_BLR_FLT;
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                cell.SetCellValue(SPEC_TYPE);
                                cell = row.CreateCell(6); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(7); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(8); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT));
                                cell = row.CreateCell(9); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.SAMPLING_FREQUENCY);
                                cell = row.CreateCell(10); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue(s.CONTROL_METHOD);
                                cell = row.CreateCell(11); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(12); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                                cell = row.CreateCell(13); cell.CellStyle = style_BLR_FLT;
                                cell.SetCellValue("");
                            }
                            row = sheet.CreateRow(++rownum);
                            for (int i = 0; i < 14; i++)
                            {
                                cell = row.CreateCell(i); cell.CellStyle = style_BLRB_FLT;
                            }
                            #endregion
                        }
                        row = sheet.CreateRow(++rownum);
                        for (int i = 0; i < 14; i++)
                        {
                            cell = row.CreateCell(i);
                        }
                    }
                }
            }
            #endregion

            #region BOM信息
            foreach (var g in package_groups)
            {
                sheet = book.CreateSheet("BOM-" + g.GROUP_NO);
                sheet.DisplayGridlines = false;
                sheet.TabColorIndex = IndexedColors.GREEN.Index;
                rownum = 0;
                #region 列宽
                sheet.SetColumnWidth(0, 6 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 20 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 20 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                sheet.SetColumnWidth(6, 20 * 256);
                sheet.SetColumnWidth(7, 20 * 256);
                sheet.SetColumnWidth(8, 20 * 256);
                sheet.SetColumnWidth(9, 20 * 256);
                sheet.SetColumnWidth(10, 20 * 256);
                sheet.SetColumnWidth(11, 20 * 256);
                sheet.SetColumnWidth(12, 20 * 256);
                sheet.SetColumnWidth(13, 20 * 256);
                #endregion

                #region 大标题

                row = sheet.CreateRow(rownum);
                row.Height = 2 * 256;

                #region 图片

                cell = row.CreateCell(0);
                cell.CellStyle = style;

                patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2, 2);
                anchor.AnchorType = 3;
                picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(
                    HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png",
                    book));
                picture.LineStyle = LineStyle.None;//原始大小：picture.Resize();

                #endregion

                #region 标题文字

                cell = row.CreateCell(2); cell.CellStyle = style_BigTitle;
                cell.SetCellValue("ENGINEERING PACKAGE\n工程试验指示");
                for (int i = 3; i <= 7; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 7));

                #endregion

                #region 文件编号

                cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("DOC NO.");
                cell = row.CreateCell(9); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(string.Format("{0}-{1}", package_base_info.PACKAGE_NO, package_base_info.VERSION_NO));

                #endregion

                #region 生效日期

                row = sheet.CreateRow(++rownum);
                row.Height = 2 * 256;
                cell = row.CreateCell(0); cell.CellStyle = style;
                cell = row.CreateCell(1); cell.CellStyle = style;
                for (int i = 2; i <= 7; i++) { cell = row.CreateCell(i); cell.CellStyle = style_BigTitle; }
                cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("EFF.DATE");
                cell = row.CreateCell(9); cell.CellStyle = style_Center_Center;
                cell.SetCellValue(package_base_info.EFFECT_DATE);

                #endregion

                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                row = sheet.CreateRow(++rownum);
                cell = row.CreateCell(0); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("父件P/N");
                cell = row.CreateCell(1); cell.CellStyle = style_Bold_Center_Center;
                cell = row.CreateCell(2); cell.CellStyle = style_Bold_Center_Center;
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                cell = row.CreateCell(3); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("子件P/N");
                cell = row.CreateCell(4); cell.CellStyle = style_Bold_Center_Center;
                sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));
                cell = row.CreateCell(5); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("父件数量");
                cell = row.CreateCell(6); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("子件数量");
                cell = row.CreateCell(7); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("IQC来料");
                cell = row.CreateCell(8); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("替代件");
                cell = row.CreateCell(9); cell.CellStyle = style_Bold_Center_Center;
                cell.SetCellValue("同步日期");
                foreach (var p in package_flow_info)
                {
                    var package_bom_spec_info = _PACKAGE_BOM_SPEC_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, FACTORY_ID, p.PROCESS_ID, VERSION_NO);


                    foreach (var item in package_bom_spec_info)
                    {


                        row = sheet.CreateRow(++rownum);
                        cell = row.CreateCell(0); cell.CellStyle = style;
                        cell.SetCellValue(item.P_PART_ID);
                        cell = row.CreateCell(1); cell.CellStyle = style;
                        cell = row.CreateCell(2); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 0, 2));
                        cell = row.CreateCell(3); cell.CellStyle = style;
                        cell.SetCellValue(item.C_PART_ID);
                        cell = row.CreateCell(4); cell.CellStyle = style;
                        sheet.AddMergedRegion(new CellRangeAddress(rownum, rownum, 3, 4));
                        cell = row.CreateCell(5); cell.CellStyle = style;
                        cell.SetCellValue(item.P_PART_QTY.ToString());
                        cell = row.CreateCell(6); cell.CellStyle = style;
                        cell.SetCellValue(item.C_PART_QTY.ToString());
                        cell = row.CreateCell(7); cell.CellStyle = style;
                        cell.SetCellValue(item.IS_IQC_MATERIAL == "1" ? "是" : "否");
                        cell = row.CreateCell(8); cell.CellStyle = style;
                        cell.SetCellValue(item.IS_SUBSTITUTE == "1" ? "是" : "否");
                        cell = row.CreateCell(9); cell.CellStyle = style;
                        cell.SetCellValue(item.SYNC_DATE);
                    }
                }
            }
            #endregion

            #region 输出
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            HttpContext.Current.Response.AddHeader(
                "Content-Disposition",
                string.Format("attachment; filename={0}-{1}.xls",
                GetToExcelName(package_base_info.PACKAGE_NO),
                GetToExcelName(package_base_info.VERSION_NO)
                ));

            HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            ms.WriteTo(HttpContext.Current.Response.OutputStream);
            book = null;

            ms.Close();
            ms.Dispose();
            #endregion

            return 1;
        }

        public static int LoadImage(string path, HSSFWorkbook wb)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                return wb.AddPicture(buffer, PictureType.PNG);
            }
            catch (Exception)
            {
                FileStream file = new FileStream(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/MILogo.png", FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                return wb.AddPicture(buffer, PictureType.PNG);
            }
        }
        public static int LoadImageByte(byte[] bt, HSSFWorkbook wb)
        {
            try
            {
                //FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] buffer = bt;
                return wb.AddPicture(buffer, PictureType.PNG);
            }
            catch (Exception)
            {
                FileStream file = new FileStream(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/Logo.png", FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                return wb.AddPicture(buffer, PictureType.PNG);
            }
        }
        public static string GetToExcelName(string fileName)
        {
            string UserAgent = HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower();
            if (UserAgent.IndexOf("firefox") == -1)
                fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            return fileName;
        }

        #endregion

        #region 预览

        #region Cover

        public string Cover(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var sb = new StringBuilder();
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
            #region 大标题
            sb.Append("<tr>");
            sb.Append("<td rowspan=\"2\">");
            sb.Append("<img src=\"/Images/Logo.png\" />");
            sb.Append("</td>");
            sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:18px;font-weight:bold;\">");
            sb.Append("ENGINEERING PACKAGE<br />工程试验指示");
            sb.Append("</td>");
            sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
            sb.Append("DOC NO.");
            sb.Append("</td>");
            sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
            sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
            sb.Append("EFF.DATE");
            sb.Append("</td>");
            sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
            sb.Append(pkg.EFFECT_DATE);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            #endregion
            sb.Append("<td colspan=\"4\" style=\"padding:10px;\">");
            #region 基本信息
            sb.AppendFormat("{0}：{1}", "厂别", pkg.FACTORY_ID);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "试验类型", pkg.PACKAGE_TYPE_ID);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "订单类型", pkg.ORDER_TYPE);
            sb.Append("<br />");
            sb.Append("<br />");
            sb.AppendFormat("{0}：{1}", "产品类型", pkg.PRODUCT_TYPE_ID);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "工艺类型", pkg.PRODUCT_PROC_TYPE_ID);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "电池类型", pkg.BATTERY_TYPE);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "品种", pkg.BATTERY_MODEL);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "层数", pkg.BATTERY_LAYERS);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "数量", pkg.BATTERY_QTY);
            sb.Append("<br />");
            sb.Append("<br />");
            sb.AppendFormat("{0}：{1}", "项目代码", pkg.PROJECT_CODE);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "电池编号", pkg.BATTERY_PARTNO);
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "客户代码", pkg.CUSTOMER_CODE);
            sb.Append("<br />");
            sb.Append("<br />");
            sb.AppendFormat("{0}：{1}", "出货日期", pkg.OUTPUT_TARGET_DATE);
            sb.Append("<br />");
            sb.Append("<br />");
            sb.AppendFormat("{0}：{1}", "是否紧急", pkg.IS_URGENT == "1" ? "YES" : "NO");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.AppendFormat("{0}：{1}", "紧急原因", pkg.REASON_FORURGENT);
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("分组信息：");
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            foreach (var item in package_groups) { sb.AppendFormat("{0}：{1}   ", item.GROUP_NO, item.GROUP_QTY.ToString()); }
            #endregion
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            sb.Append("<br />");
            #region 设计信息
            foreach (var item in package_groups)
            {
                var package_design_info = _PACKAGE_DESIGN_INFO.GetDataById(item.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO);
                foreach (var d in package_design_info)
                {
                    sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                    sb.Append("<tr>");
                    sb.Append("<td colspan=\"6\" style=\"text-align:left;padding:5px;background-color:#efefef;\">");
                    sb.Append(d.GROUP_NO + "组的设计信息");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; width:100px;padding:5px;\">");
                    sb.Append("电池容量");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; width:193px;padding:5px;\">");
                    sb.Append(d.CELL_CAP + "mAh");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; width:100px;padding:5px;\">");
                    sb.Append("起始电压");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; width:193px;padding:5px;\">");
                    sb.Append(d.BEG_VOL + "V");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; width:100px;padding:5px;\">");
                    sb.Append("截至电压");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; width:193px;padding:5px;\">");
                    sb.Append(d.END_VOL + "V");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("阳极材料");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append(d.ANODE_STUFF_ID);
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("阴极材料");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.CATHODE_STUFF_ID);
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("隔离膜材料");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append(d.SEPARATOR_ID);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("阳极配方");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.ANODE_FORMULA_ID);
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px; \">");
                    sb.Append("阴极配方");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px; \">");
                    sb.Append(d.CATHODE_FORMULA_ID);
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("电解液配方");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append(d.ELECTROLYTE_ID);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("阳极涂布重量");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.ANODE_COATING_WEIGHT + "g/1540.25mm²");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; \">");
                    sb.Append("阴极涂布重量");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append(d.CATHODE_COATING_WEIGHT + "g/1540.25mm²");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("注液量");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.INJECTION_QTY + "g");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("阳极压实密度");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.ANODE_DENSITY + "g/cm³");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("阴极压实密度");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.CATHODE_DENSITY + "g/cm³");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("保液系数");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px; \">");
                    sb.Append(d.LIQUID_PER);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("阳极集流体材料");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append(d.ANODE_FOIL_ID);
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px;\">");
                    sb.Append("阴极集流体材料");
                    sb.Append("</td>");
                    sb.Append("<td colspan=\"4\" style=\"text-align:left;padding:5px; \">");
                    sb.Append(d.CATHODE_FOIL_ID);
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("阳极集流体厚度");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px; \">");
                    sb.Append(d.ANODE_THICKNESS + "mm");
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align:left;padding:5px; \">");
                    sb.Append("阴极集流体厚度");
                    sb.Append("</td>");
                    sb.Append("<td colspan=\"4\"  style=\"text-align:left;padding:5px; \">");
                    sb.Append(d.CATHODE_FOIL_ID + "mm");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:left; padding:5px;\">");
                    sb.Append("补充说明");
                    sb.Append("</td>");
                    sb.Append("<td colspan=\"5\" style=\"text-align:left; padding:5px;\">");
                    sb.Append(d.MODEL_DESC);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                }
            }
            #endregion
            sb.Append("<br />");
            #region 分组参数

            foreach (var g in package_groups)
            {
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td colspan=\"6\" style=\"text-align:left;padding:5px;background-color:#efefef;\">");
                sb.Append(g.GROUP_NO + "组的分组参数");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                sb.Append("工序");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:180px;\">");
                sb.Append("参数名");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                sb.Append("目标值");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                sb.Append("上限");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                sb.Append("下限");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                sb.Append("相关编号");
                sb.Append("</td>");
                sb.Append("</tr>");

                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    bool pflag = true;
                    #region 产品参数，工艺参数
                    //没有定义参数类型的
                    var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, p.GROUP_NO, FACTORY_ID, VERSION_NO, p.PROCESS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1 AND A.IS_GROUP_PARAM='1' ");
                    //定义了参数类型的
                    var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIDAndGroupNoWithSetting(PACKAGE_NO, p.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID, " AND A.IS_GROUP_PARAM='1' AND SPEC_TYPE='OI' ");
                    var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                    foreach (var x in package_param_spec_info)
                    {
                        list.Add(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = x.PACKAGE_NO,
                            GROUP_NO = x.GROUP_NO,
                            FACTORY_ID = x.FACTORY_ID,
                            VERSION_NO = x.VERSION_NO,
                            PARAMETER_ID = x.PARAMETER_ID,
                            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                            SPEC_TYPE = x.SPEC_TYPE,
                            PARAM_UNIT = x.PARAM_UNIT,
                            TARGET = x.TARGET,
                            USL = x.USL,
                            LSL = x.LSL,
                            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                            CONTROL_METHOD = x.CONTROL_METHOD,
                            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                            PARAM_DESC = x.PARAM_DESC,
                            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                        });
                    }
                    package_param_setting.AddRange(list);
                    if (package_param_setting.Count > 0)
                    {
                        package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
                    }
                    foreach (var item in package_param_setting)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                        if (pflag)
                        {
                            sb.Append(p.PROCESS_DESC);
                        }
                        pflag = false;
                        sb.Append("</td>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                        sb.Append(item.PARAM_DESC);
                        sb.Append("</td>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                        sb.Append(item.TARGET);
                        sb.Append("</td>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                        sb.Append(item.USL);
                        sb.Append("</td>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                        sb.Append(item.LSL);
                        sb.Append("</td>");
                        sb.Append("<td style=\"text-align:left; padding:5px;width:110px;\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }

                    #endregion
                    #region 附图参数
                    var img = "";
                    var package_illustration_info = _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID);
                    foreach (var item in package_illustration_info)
                    {
                        img = item.ILLUSTRATION_ID + '\n';
                        //没有定义参数类型的
                        package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByIllustrationId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.ILLUSTRATION_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByIllustrationId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.ILLUSTRATION_ID, "");
                        list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
                        foreach (var param in package_param_setting)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                            if (pflag)
                            {
                                sb.Append(p.PROCESS_DESC);
                            }
                            pflag = false;
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                            sb.Append(param.PARAM_DESC);
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                            sb.Append(param.TARGET);
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                            sb.Append(param.USL);
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:140px;\">");
                            sb.Append(param.LSL);
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:left; padding:5px;width:110px;\">");
                            sb.Append(img.TrimEnd('\n'));
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }

                    #endregion
                }
                sb.Append("</table>");
                sb.Append("<br />");
            }

            #endregion

            #region 变更信息
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:945px;\">");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;background-color:#efefef;\">");
            sb.Append("产品变更");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;\">");
            sb.Append(pkg.PRODUCT_CHANGE_HL);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<br />");
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:945px;\">");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;background-color:#efefef;\">");
            sb.Append("工艺变更");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;\">");
            sb.Append(pkg.PROCESS_CHANGE_HL);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<br />");
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:945px;\">");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;background-color:#efefef;\">");
            sb.Append("物料变更");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;\">");
            sb.Append(pkg.MATERIAL_CHANGE_HL);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<br />");
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:945px;\">");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;background-color:#efefef;\">");
            sb.Append("其它变更");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  style=\"text-align:left;padding:5px;\">");
            sb.Append(pkg.OTHER_CHANGE_HL);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<br />");
            #endregion
            return sb.ToString();
        }

        #endregion
        #region 正文
        public string Parameter(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            var sb = new StringBuilder();
            foreach (var item in package_groups)
            {
                #region 标题
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\">");
                sb.Append("<img src=\"/Images/Logo.png\" />");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:24px;\">");
                sb.Append(item.GROUP_NO + "组");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append("DOC NO.");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append("EFF.DATE");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.EFFECT_DATE);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br />");
                #endregion

                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                #region 标题
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("组别");
                sb.Append("</td>");
                sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                sb.Append("工序");
                sb.Append("</td>");
                sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                sb.Append("控制点");
                sb.Append("</td>");
                sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                sb.Append("规格");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                sb.Append("频率/抽样数量");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                sb.Append("检查和控制方法");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                sb.Append("工序说明");
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("编号");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("工序名");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("产品参数");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("工序参数");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("类型");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("目标值");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("上限值");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                sb.Append("下限值");
                sb.Append("</td>");
                sb.Append("</tr>");
                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(item.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    sb.Append("<tr>");
                    sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                    sb.Append(p.GROUP_NO);
                    sb.Append("</td>");
                    sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                    sb.Append(p.PROCESS_ID);
                    sb.Append("</td>");
                    sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                    sb.Append(p.PROCESS_NAME);
                    sb.Append("</td>");
                    sb.Append("<td colspan=\"8\">");
                    //没有定义参数类型的
                    var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, item.GROUP_NO, FACTORY_ID, VERSION_NO, p.PROCESS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                    //定义了参数类型的
                    var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByProcessIDAndGroupNoWithSetting(PACKAGE_NO, item.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID, "");
                    var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                    foreach (var x in package_param_spec_info)
                    {
                        list.Add(new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = x.PACKAGE_NO,
                            GROUP_NO = x.GROUP_NO,
                            FACTORY_ID = x.FACTORY_ID,
                            VERSION_NO = x.VERSION_NO,
                            PARAMETER_ID = x.PARAMETER_ID,
                            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                            SPEC_TYPE = x.SPEC_TYPE,
                            PARAM_UNIT = x.PARAM_UNIT,
                            TARGET = x.TARGET,
                            USL = x.USL,
                            LSL = x.LSL,
                            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                            CONTROL_METHOD = x.CONTROL_METHOD,
                            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                            PARAM_DESC = x.PARAM_DESC,
                            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                        });
                    }
                    package_param_setting.AddRange(list);
                    if (package_param_setting.Count > 0)
                    {
                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                    }
                    sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                    foreach (var s in package_param_setting)
                    {
                        switch (s.PARAM_TYPE_ID)
                        {
                            case "PROCESS":
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");

                                break;
                            case "PRODUCT":
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append("&nbsp;");
                                sb.Append("</td>");

                                break;
                        }
                        string SPEC_TYPE = "";
                        switch (s.SPEC_TYPE)
                        {
                            case "":
                                SPEC_TYPE = ""; break;
                            case "FAI":
                                SPEC_TYPE = "首件"; break;
                            case "PMI":
                                SPEC_TYPE = "过程"; break;
                            case "OI":
                                SPEC_TYPE = "出货"; break;
                            default:
                                SPEC_TYPE = ""; break;
                        }
                        sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                        sb.Append(SPEC_TYPE + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                        sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                        sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                        sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                        sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                        sb.Append(s.CONTROL_METHOD + "&nbsp;");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("<td>");
                    sb.Append(p.PKG_PROC_DESC);
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
            }


            return sb.ToString();
        }
        #endregion
        #region 物料
        public string Material(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            var sb = new StringBuilder();
            foreach (var g in package_groups)
            {
                #region 标题
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\">");
                sb.Append("<img src=\"/Images/Logo.png\" />");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:24px;\">");
                sb.Append(g.GROUP_NO + "组");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append("DOC NO.");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append("EFF.DATE");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.EFFECT_DATE);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br />");

                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    #region 物料类型
                    var package_proc_material_info = _PACKAGE_PROC_MATERIAL_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
                    foreach (var mt in package_proc_material_info)
                    {
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                        #region 标题
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("类型编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("类型名称");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("物料编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("物料编号名称");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        #endregion
                        var m = new List<PACKAGE_PROC_MATERIAL_INFO_Entity>();
                        m.Add(mt);
                        var pn = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID).Where(x => x.MATERIAL_TYPE_ID == mt.MATERIAL_TYPE_ID).ToList();//找到此物料类型的编号
                        foreach (var item in pn)
                        {
                            m.Add(new PACKAGE_PROC_MATERIAL_INFO_Entity
                            {
                                MATERIAL_PN_ID = item.MATERIAL_PN_ID,
                                MATERIAL_PN_DESC = item.MATERIAL_PN_DESC
                            });
                        }
                        foreach (var m1 in m)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"padding:5px;\">");
                            sb.Append(m1.MATERIAL_TYPE_ID);
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px;\">");
                            sb.Append(m1.MATERIAL_TYPE_DESC);
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px;\">");
                            sb.Append(m1.MATERIAL_PN_ID);
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px;\">");
                            sb.Append(m1.MATERIAL_PN_DESC);
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("<br />");


                        #region 参数
                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByMaterialTypeId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, mt.MATERIAL_TYPE_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialTypeId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, mt.MATERIAL_TYPE_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                        if (package_param_setting.Any())
                        {
                            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                            #region 标题
                            sb.Append("<tr>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("组别");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                            sb.Append("工序");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("控制点");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("规格");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("频率/抽样数量");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("检查和控制方法");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("工序说明");
                            sb.Append("</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("工序名");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数名称");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("类型");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("目标值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("上限值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("下限值");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            #endregion

                            #region 参数
                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                            sb.Append(p.GROUP_NO);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_ID);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_NAME);
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"8\">");

                            sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                            foreach (var s in package_param_setting)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAMETER_ID);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                                sb.Append(SPEC_TYPE + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                                sb.Append(s.CONTROL_METHOD + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("<td>");
                            sb.Append(p.PKG_PROC_DESC);
                            sb.Append("</td>");

                            sb.Append("</tr>");
                            #endregion
                            sb.Append("</table>");
                        }

                        #endregion

                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");

                    }
                    #endregion

                    #region 物料编号
                    var package_proc_pn_info = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
                    List<PACKAGE_PROC_PN_INFO_Entity> lst = new List<PACKAGE_PROC_PN_INFO_Entity>();
                    foreach (var item in package_proc_pn_info)
                    {
                        if (!package_proc_material_info.Exists(x => x.MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID))
                        {
                            lst.Add(item);
                        }
                    }
                    foreach (var item in lst)
                    {
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("类型编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("类型名称");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.MATERIAL_PN_ID);
                        sb.Append("</td>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.MATERIAL_PN_DESC);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("<br />");

                        #region 参数
                        //没有定义参数类型的
                        var package_param_setting1 = _PACKAGE_PARAM_SETTING.GetDataByMaterialPN(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.MATERIAL_PN_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info1 = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialPNId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.MATERIAL_PN_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info1)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting1.AddRange(list);

                        var paramlst = (from x in package_param_setting1
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting1 = paramlst;
                        if (package_param_setting1.Any())
                        {
                            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                            #region 标题
                            sb.Append("<tr>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("组别");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                            sb.Append("工序");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("控制点");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("规格");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("频率/抽样数量");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("检查和控制方法");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("工序说明");
                            sb.Append("</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("工序名");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数名称");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("类型");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("目标值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("上限值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("下限值");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            #endregion

                            #region 参数
                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                            sb.Append(p.GROUP_NO);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_ID);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_NAME);
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"8\">");

                            sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                            foreach (var s in package_param_setting1)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAMETER_ID);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                                sb.Append(SPEC_TYPE + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                                sb.Append(s.CONTROL_METHOD + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("<td>");
                            sb.Append(p.PKG_PROC_DESC);
                            sb.Append("</td>");

                            sb.Append("</tr>");
                            #endregion
                            sb.Append("</table>");
                        }
                        #endregion
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                    }
                    #endregion
                }
            }
            return sb.ToString();
        }
        #endregion
        #region 设备
        public string Equipment(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            var sb = new StringBuilder();
            foreach (var g in package_groups)
            {
                #region 标题
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\">");
                sb.Append("<img src=\"/Images/Logo.png\" />");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:24px;\">");
                sb.Append(g.GROUP_NO + "组");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append("DOC NO.");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append("EFF.DATE");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.EFFECT_DATE);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br />");
                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    var package_proc_equip_class_info = _PACKAGE_PROC_EQUIP_CLASS_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID, "");
                    var package_proc_equip_info = _PACKAGE_PROC_EQUIP_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
                    #region 设备类型
                    foreach (var item in package_proc_equip_class_info)
                    {
                        #region 编号
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("设备类型编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("设备类型编号名称");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.EQUIPMENT_CLASS_ID);
                        sb.Append("</td>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.EQUIPMENT_CLASS_DESC);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion

                        sb.Append("<br />");

                        #region 参数
                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentClass(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_CLASS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentClass(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_CLASS_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                        if (package_param_setting.Any())
                        {
                            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                            #region 标题
                            sb.Append("<tr>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("组别");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                            sb.Append("工序");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("控制点");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("规格");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("频率/抽样数量");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("检查和控制方法");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("工序说明");
                            sb.Append("</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("工序名");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数名称");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("类型");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("目标值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("上限值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("下限值");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            #endregion

                            #region 参数
                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                            sb.Append(p.GROUP_NO);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_ID);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_NAME);
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"8\">");

                            sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                            foreach (var s in package_param_setting)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAMETER_ID);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                                sb.Append(SPEC_TYPE + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                                sb.Append(s.CONTROL_METHOD + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("<td>");
                            sb.Append(p.PKG_PROC_DESC);
                            sb.Append("</td>");

                            sb.Append("</tr>");
                            #endregion
                            sb.Append("</table>");
                        }
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        #endregion
                    }
                    #endregion
                    #region 设备编号
                    foreach (var item in package_proc_equip_info)
                    {
                        #region 编号
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("设备编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("设备编号名称");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.EQUIPMENT_ID);
                        sb.Append("</td>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.EQUIPMENT_DESC);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion

                        sb.Append("<br />");

                        #region 参数
                        //没有定义参数类型的
                        var package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentInfo(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentInfo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting.AddRange(list);

                        var paramlst = (from x in package_param_setting
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting = paramlst;
                        if (package_param_setting.Any())
                        {
                            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                            #region 标题
                            sb.Append("<tr>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("组别");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                            sb.Append("工序");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("控制点");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("规格");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("频率/抽样数量");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("检查和控制方法");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("工序说明");
                            sb.Append("</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("工序名");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数名称");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("类型");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("目标值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("上限值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("下限值");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            #endregion

                            #region 参数
                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                            sb.Append(p.GROUP_NO);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_ID);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_NAME);
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"8\">");

                            sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                            foreach (var s in package_param_setting)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAMETER_ID);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                                sb.Append(SPEC_TYPE + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                                sb.Append(s.CONTROL_METHOD + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("<td>");
                            sb.Append(p.PKG_PROC_DESC);
                            sb.Append("</td>");

                            sb.Append("</tr>");
                            #endregion
                            sb.Append("</table>");
                            sb.Append("<br />");
                            sb.Append("<br />");
                            sb.Append("<br />");
                            sb.Append("<br />");
                            sb.Append("<br />");
                            sb.Append("<br />");
                        }
                        #endregion

                    }
                    #endregion
                }

            }
            return sb.ToString();
        }
        #endregion
        #region 附图
        public string Illustration(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            var sb = new StringBuilder();
            foreach (var g in package_groups)
            {
                #region 标题
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\">");
                sb.Append("<img src=\"/Images/Logo.png\" />");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:24px;\">");
                sb.Append(g.GROUP_NO + "组");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append("DOC NO.");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append("EFF.DATE");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.EFFECT_DATE);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br />");
                #endregion
                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    var package_illustration_info = _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, p.PROCESS_ID);
                    foreach (var item in package_illustration_info)
                    {
                        #region 编号
                        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"width:150px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("图片类型编号");
                        sb.Append("</td>");
                        sb.Append("<td style=\"width:350px;background-color:#FFFF99; text-align:center;padding:5px;\">");
                        sb.Append("名称");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.ILLUSTRATION_ID);
                        sb.Append("</td>");
                        sb.Append("<td style=\"padding:5px;\">");
                        sb.Append(item.ILLUSTRATION_DESC);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion

                        sb.Append("<br />");


                        #region 参数
                        //没有定义参数类型的
                        var package_param_setting1 = _PACKAGE_PARAM_SETTING.GetDataByIllustrationId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.ILLUSTRATION_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1");
                        //定义了参数类型的
                        var package_param_spec_info1 = _PACKAGE_PARAM_SPEC_INFO.GetDataByIllustrationId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.ILLUSTRATION_ID, "");
                        var list = new List<PACKAGE_PARAM_SETTING_Entity>();
                        foreach (var x in package_param_spec_info1)
                        {
                            list.Add(new PACKAGE_PARAM_SETTING_Entity
                            {
                                PACKAGE_NO = x.PACKAGE_NO,
                                GROUP_NO = x.GROUP_NO,
                                FACTORY_ID = x.FACTORY_ID,
                                VERSION_NO = x.VERSION_NO,
                                PARAMETER_ID = x.PARAMETER_ID,
                                PARAM_TYPE_ID = x.PARAM_TYPE_ID,
                                SPEC_TYPE = x.SPEC_TYPE,
                                PARAM_UNIT = x.PARAM_UNIT,
                                TARGET = x.TARGET,
                                USL = x.USL,
                                LSL = x.LSL,
                                SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
                                CONTROL_METHOD = x.CONTROL_METHOD,
                                PARAM_ORDER_NO = x.PARAM_ORDER_NO,
                                PARAM_DESC = x.PARAM_DESC,
                                DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
                            });
                        }
                        package_param_setting1.AddRange(list);

                        var paramlst = (from x in package_param_setting1
                                        orderby x.PARAMETER_ID, x.DISP_ORDER_IN_SC, x.SPEC_TYPE descending
                                        select x).ToList();
                        package_param_setting1 = paramlst;
                        if (package_param_setting1.Any())
                        {
                            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\" style=\"width:1500px;\">");
                            #region 标题
                            sb.Append("<tr>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("组别");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center;padding: 2px 0;background-color:#efefef;\">");
                            sb.Append("工序");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"2\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("控制点");
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"4\"  style=\"text-align:center;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("规格");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\" style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("频率/抽样数量");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("检查和控制方法");
                            sb.Append("</td>");
                            sb.Append("<td rowspan=\"2\"  style=\"text-align:center;vertical-align:middle;padding:2px 0;width:150px;background-color:#efefef;\">");
                            sb.Append("工序说明");
                            sb.Append("</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;width:100px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("工序名");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数编号");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("参数名称");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:50px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("类型");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("目标值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("上限值");
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:center;width:150px;padding:2px 0;background-color:#efefef;\">");
                            sb.Append("下限值");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            #endregion

                            #region 参数
                            sb.Append("<tr>");
                            sb.Append("<td  style=\"text-align:center;padding:2px 0;\">");
                            sb.Append(p.GROUP_NO);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_ID);
                            sb.Append("</td>");
                            sb.Append("<td  style=\"text-align:left;padding:2px 0;\">");
                            sb.Append(p.PROCESS_NAME);
                            sb.Append("</td>");
                            sb.Append("<td colspan=\"8\">");

                            sb.Append("<table class=\"tbl_border_right\" cellpadding=\"0\" cellspacing=\"0\">");
                            foreach (var s in package_param_setting1)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAMETER_ID);
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.PARAM_DESC);
                                sb.Append("</td>");
                                string SPEC_TYPE = "";
                                switch (s.SPEC_TYPE)
                                {
                                    case "":
                                        SPEC_TYPE = ""; break;
                                    case "FAI":
                                        SPEC_TYPE = "首件"; break;
                                    case "PMI":
                                        SPEC_TYPE = "过程"; break;
                                    case "OI":
                                        SPEC_TYPE = "出货"; break;
                                    default:
                                        SPEC_TYPE = ""; break;
                                }
                                sb.Append("<td style=\"width:48px;padding:2px 0; text-align:center;\">");
                                sb.Append(SPEC_TYPE + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.TARGET + (string.IsNullOrEmpty(s.TARGET) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.USL + (string.IsNullOrEmpty(s.USL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.LSL + (string.IsNullOrEmpty(s.LSL) ? "" : s.PARAM_UNIT) + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;\">");
                                sb.Append(s.SAMPLING_FREQUENCY + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("<td style=\"width:144px;padding:2px 0;border-right:none;\">");
                                sb.Append(s.CONTROL_METHOD + "&nbsp;");
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");
                            sb.Append("<td>");
                            sb.Append(p.PKG_PROC_DESC);
                            sb.Append("</td>");

                            sb.Append("</tr>");
                            #endregion
                            sb.Append("</table>");
                        }
                        sb.Append("<br />");

                        #endregion
                        sb.Append("<img src=\"PACKAGE_ILLUSTRATION_INFO_ShowImg.ashx?ILLUSTRATION_ID=" + item.ILLUSTRATION_ID + "&FACTORY_ID=" + item.FACTORY_ID + "&PACKAGE_NO=" + item.PACKAGE_NO + "&VERSION_NO=" + item.VERSION_NO + "&GROUP_NO=" + item.GROUP_NO + "&PROCESS_ID=" + item.PROCESS_ID + "\" />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                        sb.Append("<br />");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion
        #region BOM
        public string Bom(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var pkgList = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!pkgList.Any()) return "";
            var pkg = pkgList.First();
            var package_groups = _PACKAGE_GROUPS.GetData(FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
            var sb = new StringBuilder();
            foreach (var g in package_groups)
            {
                #region 标题
                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td rowspan=\"2\">");
                sb.Append("<img src=\"/Images/Logo.png\" />");
                sb.Append("</td>");
                sb.Append("<td rowspan=\"2\" style=\"width:600px;text-align:center;vertical-align:middle;font-size:24px;\">");
                sb.Append(g.GROUP_NO + "组");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append("DOC NO.");
                sb.Append("</td>");
                sb.Append("<td style=\"width:140px;text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.PACKAGE_NO + "-" + pkg.VERSION_NO);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append("EFF.DATE");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center;vertical-align:middle;\">");
                sb.Append(pkg.EFFECT_DATE);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<br />");
                #endregion

                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" class=\"tbl\">");
                sb.Append("<tr>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:140px;background-color:#efefef;\">");
                sb.Append("父件P/N");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:140px;background-color:#efefef;\">");
                sb.Append("子件P/N");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:100px;background-color:#efefef;\">");
                sb.Append("父件数量");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:100px;background-color:#efefef;\">");
                sb.Append("子件数量");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:100px;background-color:#efefef;\">");
                sb.Append("IQC来料");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:100px;background-color:#efefef;\">");
                sb.Append("替代件");
                sb.Append("</td>");
                sb.Append("<td  style=\"text-align:center;padding:2px;width:140px;background-color:#efefef;\">");
                sb.Append("同步日期");
                sb.Append("</td>");
                sb.Append("</tr>");

                var package_flow_info = _PACKAGE_FLOW_INFO.GetData(g.GROUP_NO, FACTORY_ID, PACKAGE_NO, VERSION_NO, "");
                foreach (var p in package_flow_info)
                {
                    var package_bom_spec_info = _PACKAGE_BOM_SPEC_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, FACTORY_ID, p.PROCESS_ID, VERSION_NO);
                    foreach (var item in package_bom_spec_info)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td  style=\"text-align:center;padding:2px;\">");
                        sb.Append(item.P_PART_ID);
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.C_PART_ID);
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.P_PART_QTY.ToString());
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.C_PART_QTY.ToString());
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.IS_IQC_MATERIAL == "1" ? "是" : "否");
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.IS_SUBSTITUTE == "1" ? "是" : "否");
                        sb.Append("</td>");
                        sb.Append("<td  style=\"text-align:left;padding:2px ;\">");
                        sb.Append(item.SYNC_DATE);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
                sb.Append("<br />");
            }
            return sb.ToString();
        }
        #endregion

        #endregion
    }

}
#region 移除的分组参数

//#region 物料参数

//#region 物料类型
//var package_proc_material_info = _PACKAGE_PROC_MATERIAL_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
//foreach (var mt in package_proc_material_info)
//{
//    string material = "";
//    var pn = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID).Where(x => x.MATERIAL_TYPE_ID == mt.MATERIAL_TYPE_ID).ToList();//找到此物料类型的编号
//    material += mt.MATERIAL_TYPE_ID + "\n";
//    foreach (var item in pn)
//    {
//        material += item.MATERIAL_PN_ID + "\n";
//    }
//    //没有定义参数类型的
//    package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByMaterialTypeId(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, mt.MATERIAL_TYPE_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1  AND A.IS_GROUP_PARAM='1'");
//    //定义了参数类型的
//    package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialTypeId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, mt.MATERIAL_TYPE_ID, " AND A.IS_GROUP_PARAM='1'  AND SPEC_TYPE='OI' ");

//    list = new List<PACKAGE_PARAM_SETTING_Entity>();
//    foreach (var x in package_param_spec_info)
//    {
//        list.Add(new PACKAGE_PARAM_SETTING_Entity
//        {
//            PACKAGE_NO = x.PACKAGE_NO,
//            GROUP_NO = x.GROUP_NO,
//            FACTORY_ID = x.FACTORY_ID,
//            VERSION_NO = x.VERSION_NO,
//            PARAMETER_ID = x.PARAMETER_ID,
//            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
//            SPEC_TYPE = x.SPEC_TYPE,
//            PARAM_UNIT = x.PARAM_UNIT,
//            TARGET = x.TARGET,
//            USL = x.USL,
//            LSL = x.LSL,
//            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
//            CONTROL_METHOD = x.CONTROL_METHOD,
//            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
//            PARAM_DESC = x.PARAM_DESC,
//            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
//        });
//    }
//    package_param_setting.AddRange(list);
//    if (package_param_setting.Count > 0)
//    {
//        package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
//    }
//    foreach (var item in package_param_setting)
//    {
//        row = sheet.CreateRow(++rownum);
//        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
//        if (pflag)
//        {
//            cell.SetCellValue(p.PROCESS_DESC);
//        }
//        pflag = false;
//        cell = row.CreateCell(1); cell.CellStyle = style;
//        cell.SetCellValue(item.PARAM_DESC);
//        cell = row.CreateCell(2); cell.CellStyle = style;
//        cell.SetCellValue(item.TARGET);
//        cell = row.CreateCell(3); cell.CellStyle = style;
//        cell.SetCellValue(item.USL);
//        cell = row.CreateCell(4); cell.CellStyle = style;
//        cell.SetCellValue(item.LSL);
//        cell = row.CreateCell(5); cell.CellStyle = style;
//        cell.SetCellValue(material.TrimEnd('\n'));
//    }

//}
//#endregion

//#region 物料编号
//var materialpn = "";
//var package_proc_pn_info = _PACKAGE_PROC_PN_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
//List<PACKAGE_PROC_PN_INFO_Entity> lst = new List<PACKAGE_PROC_PN_INFO_Entity>();
//foreach (var item in package_proc_pn_info)
//{
//    if (!package_proc_material_info.Exists(x => x.MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID))
//    {
//        materialpn += item.MATERIAL_PN_ID + '\n';
//        lst.Add(item);
//    }
//}

//foreach (var item in lst)
//{
//    //没有定义参数类型的
//    package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByMaterialPN(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.MATERIAL_PN_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1  AND A.IS_GROUP_PARAM='1'");
//    //定义了参数类型的
//    package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByMaterialPNId(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.MATERIAL_PN_ID, " AND A.IS_GROUP_PARAM='1'  AND SPEC_TYPE='OI' ");
//    list = new List<PACKAGE_PARAM_SETTING_Entity>();
//    foreach (var x in package_param_spec_info)
//    {
//        list.Add(new PACKAGE_PARAM_SETTING_Entity
//        {
//            PACKAGE_NO = x.PACKAGE_NO,
//            GROUP_NO = x.GROUP_NO,
//            FACTORY_ID = x.FACTORY_ID,
//            VERSION_NO = x.VERSION_NO,
//            PARAMETER_ID = x.PARAMETER_ID,
//            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
//            SPEC_TYPE = x.SPEC_TYPE,
//            PARAM_UNIT = x.PARAM_UNIT,
//            TARGET = x.TARGET,
//            USL = x.USL,
//            LSL = x.LSL,
//            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
//            CONTROL_METHOD = x.CONTROL_METHOD,
//            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
//            PARAM_DESC = x.PARAM_DESC,
//            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
//        });
//    }
//    package_param_setting.AddRange(list);

//    package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);
//    foreach (var param in package_param_setting)
//    {
//        row = sheet.CreateRow(++rownum);
//        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
//        if (pflag)
//        {
//            cell.SetCellValue(p.PROCESS_DESC);
//        }
//        pflag = false;
//        cell = row.CreateCell(1); cell.CellStyle = style;
//        cell.SetCellValue(param.PARAM_DESC);
//        cell = row.CreateCell(2); cell.CellStyle = style;
//        cell.SetCellValue(param.TARGET);
//        cell = row.CreateCell(3); cell.CellStyle = style;
//        cell.SetCellValue(param.USL);
//        cell = row.CreateCell(4); cell.CellStyle = style;
//        cell.SetCellValue(param.LSL);
//        cell = row.CreateCell(5); cell.CellStyle = style;
//        cell.SetCellValue(materialpn.TrimEnd('\n'));
//    }

//}


//#endregion

//#endregion

//#region 设备参数

//#region 设备类型

//var package_proc_equip_class_info = _PACKAGE_PROC_EQUIP_CLASS_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID, "");
//foreach (var item in package_proc_equip_class_info)
//{
//    //没有定义参数类型的
//    package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentClass(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_CLASS_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1  AND A.IS_GROUP_PARAM='1'");
//    //定义了参数类型的
//    package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentClass(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_CLASS_ID, " AND A.IS_GROUP_PARAM='1' AND SPEC_TYPE='OI' ");
//    list = new List<PACKAGE_PARAM_SETTING_Entity>();
//    foreach (var x in package_param_spec_info)
//    {
//        list.Add(new PACKAGE_PARAM_SETTING_Entity
//        {
//            PACKAGE_NO = x.PACKAGE_NO,
//            GROUP_NO = x.GROUP_NO,
//            FACTORY_ID = x.FACTORY_ID,
//            VERSION_NO = x.VERSION_NO,
//            PARAMETER_ID = x.PARAMETER_ID,
//            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
//            SPEC_TYPE = x.SPEC_TYPE,
//            PARAM_UNIT = x.PARAM_UNIT,
//            TARGET = x.TARGET,
//            USL = x.USL,
//            LSL = x.LSL,
//            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
//            CONTROL_METHOD = x.CONTROL_METHOD,
//            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
//            PARAM_DESC = x.PARAM_DESC,
//            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
//        });
//    }
//    package_param_setting.AddRange(list);

//    package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);

//    foreach (var param in package_param_setting)
//    {
//        row = sheet.CreateRow(++rownum);
//        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
//        if (pflag)
//        {
//            cell.SetCellValue(p.PROCESS_DESC);
//        }
//        pflag = false;
//        cell = row.CreateCell(1); cell.CellStyle = style;
//        cell.SetCellValue(param.PARAM_DESC);
//        cell = row.CreateCell(2); cell.CellStyle = style;
//        cell.SetCellValue(param.TARGET);
//        cell = row.CreateCell(3); cell.CellStyle = style;
//        cell.SetCellValue(param.USL);
//        cell = row.CreateCell(4); cell.CellStyle = style;
//        cell.SetCellValue(param.LSL);
//        cell = row.CreateCell(5); cell.CellStyle = style;
//        cell.SetCellValue(item.EQUIPMENT_CLASS_ID);
//    }
//}
//#endregion

//#region 设备编号
//var package_proc_equip_info = _PACKAGE_PROC_EQUIP_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, p.PROCESS_ID, FACTORY_ID);
//foreach (var item in package_proc_equip_info)
//{
//    //没有定义参数类型的
//    package_param_setting = _PACKAGE_PARAM_SETTING.GetDataByEquipmentInfo(PACKAGE_NO, VERSION_NO, FACTORY_ID, g.GROUP_NO, item.EQUIPMENT_ID, " AND A.IS_FIRST_CHECK_PARAM!=1 AND A.IS_PROC_MON_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1 AND A.IS_OUTPUT_PARAM!=1  AND A.IS_GROUP_PARAM='1'");
//    //定义了参数类型的
//    package_param_spec_info = _PACKAGE_PARAM_SPEC_INFO.GetDataByEquipmentInfo(PACKAGE_NO, g.GROUP_NO, VERSION_NO, FACTORY_ID, item.EQUIPMENT_ID, " AND A.IS_GROUP_PARAM='1' AND SPEC_TYPE='OI' ");
//    list = new List<PACKAGE_PARAM_SETTING_Entity>();
//    foreach (var x in package_param_spec_info)
//    {
//        list.Add(new PACKAGE_PARAM_SETTING_Entity
//        {
//            PACKAGE_NO = x.PACKAGE_NO,
//            GROUP_NO = x.GROUP_NO,
//            FACTORY_ID = x.FACTORY_ID,
//            VERSION_NO = x.VERSION_NO,
//            PARAMETER_ID = x.PARAMETER_ID,
//            PARAM_TYPE_ID = x.PARAM_TYPE_ID,
//            SPEC_TYPE = x.SPEC_TYPE,
//            PARAM_UNIT = x.PARAM_UNIT,
//            TARGET = x.TARGET,
//            USL = x.USL,
//            LSL = x.LSL,
//            SAMPLING_FREQUENCY = x.SAMPLING_FREQUENCY,
//            CONTROL_METHOD = x.CONTROL_METHOD,
//            PARAM_ORDER_NO = x.PARAM_ORDER_NO,
//            PARAM_DESC = x.PARAM_DESC,
//            DISP_ORDER_IN_SC = x.DISP_ORDER_IN_SC
//        });
//    }
//    package_param_setting.AddRange(list);

//    package_param_setting.Sort((x, y) => (int)x.DISP_ORDER_IN_SC - (int)y.DISP_ORDER_IN_SC);

//    foreach (var param in package_param_setting)
//    {
//        row = sheet.CreateRow(++rownum);
//        cell = row.CreateCell(0); cell.CellStyle = style_BLR_FLT;
//        if (pflag)
//        {
//            cell.SetCellValue(p.PROCESS_DESC);
//        }
//        pflag = false;
//        cell = row.CreateCell(1); cell.CellStyle = style;
//        cell.SetCellValue(param.PARAM_DESC);
//        cell = row.CreateCell(2); cell.CellStyle = style;
//        cell.SetCellValue(param.TARGET);
//        cell = row.CreateCell(3); cell.CellStyle = style;
//        cell.SetCellValue(param.USL);
//        cell = row.CreateCell(4); cell.CellStyle = style;
//        cell.SetCellValue(param.LSL);
//        cell = row.CreateCell(5); cell.CellStyle = style;
//        cell.SetCellValue(item.EQUIPMENT_ID);
//    }
//}
//#endregion

//#endregion

#endregion
