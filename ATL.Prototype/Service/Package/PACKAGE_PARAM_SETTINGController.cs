using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Package
{
    public class PACKAGE_PARAM_SETTINGController : ApiController
    {
        BLL.Package.PACKAGE_PARAM_SETTING _PACKAGE_PARAM_SETTING = new BLL.Package.PACKAGE_PARAM_SETTING();
        #region 查询

        public List<PACKAGE_PARAM_SETTING_Entity> GetData()
        {
            return _PACKAGE_PARAM_SETTING.GetData();
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_PARAM_SETTING.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string FACTORY_ID, string VERSION_NO, string PROCESS_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByProcessIdAndGroupNo(PACKAGE_NO, GROUP_NO, FACTORY_ID, VERSION_NO, PROCESS_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentClass(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO,  string EQUIPMENT_CLASS_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByEquipmentClass(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO,  EQUIPMENT_CLASS_ID, queryStr);
        }

        public List<PACKAGE_PARAM_SETTING_Entity> GetDataByEquipmentInfo(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string GROUP_NO, string EQUIPMENT_ID, string queryStr)
        {
            return _PACKAGE_PARAM_SETTING.GetDataByEquipmentInfo(PACKAGE_NO, VERSION_NO, FACTORY_ID, GROUP_NO,  EQUIPMENT_ID, queryStr);
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
            return _PACKAGE_PARAM_SETTING.PostAdd(entity);
        }

        public int PostAddBatch(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return _PACKAGE_PARAM_SETTING.PostAddBatch(entity);
        }

        public int PostAddBatchAddOne(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return _PACKAGE_PARAM_SETTING.PostAddBatchAddOne(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return _PACKAGE_PARAM_SETTING.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_PARAM_SETTING_Entity entity)
        {
            return _PACKAGE_PARAM_SETTING.PostDelete(entity);
        }

        #endregion



    }
}
