using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_ILLUSTRATION_INFO
    {
        readonly DAL.Package.PACKAGE_ILLUSTRATION_INFO _PACKAGE_ILLUSTRATION_INFO = new DAL.Package.PACKAGE_ILLUSTRATION_INFO();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetData()
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetData();
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        }

        public IDataReader GetDataById(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return _PACKAGE_ILLUSTRATION_INFO.GetDataById(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_ILLUSTRATION_INFO_Entity entity)
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
                    var result = _PACKAGE_ILLUSTRATION_INFO.PostAdd(entity);
                    if (result <= 0) continue;
                    _PACKAGE_ILLUSTRATION_INFO.PostEdit_UpdateByIllustrationId(entity);
                    //批量新增
                    var ILLUSTRATION_PARAM_INFO = new DAL.BaseInfo.ILLUSTRATION_PARAM_INFO().GetDataByImgId(entity.ILLUSTRATION_ID, package.PRODUCT_TYPE_ID, package.PRODUCT_PROC_TYPE_ID, package.FACTORY_ID, "");
                    var PACKAGE_PARAM_SETTING = new PACKAGE_PARAM_SETTING();
                    BLL.Package.PACKAGE_PARAM_SETTING packageParamSetting = new PACKAGE_PARAM_SETTING();
                    foreach (var p in ILLUSTRATION_PARAM_INFO)
                    {
                        PACKAGE_PARAM_SETTING_Entity pkgParamSettingEntity=new PACKAGE_PARAM_SETTING_Entity
                        {
                            PACKAGE_NO = entity.PACKAGE_NO,
                            GROUP_NO = entity.GROUP_NO,
                            FACTORY_ID = entity.FACTORY_ID,
                            VERSION_NO = entity.VERSION_NO,
                            PARAMETER_ID = p.PARAMETER_ID,
                            PARAM_TYPE_ID = p.PARAM_TYPE_ID,
                            PROCESS_ID = entity.PROCESS_ID,
                            PARAM_IO = p.PARAM_IO,
                            PRODUCT_TYPE_ID=entity.PRODUCT_TYPE_ID,
                            PRODUCT_PROC_TYPE_ID=entity.PRODUCT_PROC_TYPE_ID,
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
                            CONTROL_METHOD = p.CONTROL_METHOD//,
                            //IS_SC_PARAM = p.IS_SC_PARAM
                        };
                        PACKAGE_PARAM_SETTING.PostAdd(pkgParamSettingEntity);
                        packageParamSetting.BatchAddParamSpecInfo(pkgParamSettingEntity);
                    }
                }
            }
            //else
            //{
            //    var result = _PACKAGE_ILLUSTRATION_INFO.PostAdd(entity);

            //    if (result > 0)
            //    {
            //        //用基础表的图片,更新Package的图片
            //        _PACKAGE_ILLUSTRATION_INFO.PostEdit_UpdateByIllustrationId(entity);
            //        //批量添加参数
            //        //BatchAddIllustrationParamInfo(entity);
            //    }



            //}


            return 1;
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            _PACKAGE_ILLUSTRATION_INFO.PostEdit(entity);
            //_PACKAGE_ILLUSTRATION_INFO.PostEdit_UpdateByIllustrationId(entity);
            if (string.IsNullOrEmpty(entity.GROUPS)) return 1;
            foreach (var g in entity.GROUPS.Split(','))
            {
                entity.GROUP_NO = g;
                _PACKAGE_ILLUSTRATION_INFO.PostEdit(entity);
                //_PACKAGE_ILLUSTRATION_INFO.PostEdit_UpdateByIllustrationId(entity);
            }

            return 1;
        }
        //基础表的图片，更新到package中
        public int PostEdit_UploadImg(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            _PACKAGE_ILLUSTRATION_INFO.PostEdit_UploadImg(entity);

            if (!string.IsNullOrEmpty(entity.GROUPS))
            {
                foreach (var g in entity.GROUPS.Split(','))
                {
                    entity.GROUP_NO = g;
                    _PACKAGE_ILLUSTRATION_INFO.PostEdit_UploadImg(entity);
                }
            }

            return 1;
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            if (!Settings.Permission.CheckPackageRight(entity.PACKAGE_NO, entity.FACTORY_ID, entity.VERSION_NO)) return -1;

            #region 删除参数
            //取得所有参数
            var PACKAGE_PARAM_SETTING = new DAL.Package.PACKAGE_PARAM_SETTING().GetDataByIllustrationId(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID, entity.GROUP_NO, entity.ILLUSTRATION_ID, "");
            //依次删除
            var bllPackageParamSetting = new PACKAGE_PARAM_SETTING();
            foreach (var p in PACKAGE_PARAM_SETTING)
            {
                bllPackageParamSetting.PostDelete(p);
            }
            #endregion
            return _PACKAGE_ILLUSTRATION_INFO.PostDelete(entity);
        }

        #endregion



    }
}
