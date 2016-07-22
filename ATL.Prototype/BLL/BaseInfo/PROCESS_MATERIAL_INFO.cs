using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PROCESS_MATERIAL_INFO
    {
        DAL.BaseInfo.PROCESS_MATERIAL_INFO _PROCESS_MATERIAL_INFO = new DAL.BaseInfo.PROCESS_MATERIAL_INFO();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PROCESS_MATERIAL_INFO_Entity> GetData()
        {
            return _PROCESS_MATERIAL_INFO.GetData();
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_MATERIAL_INFO.GetData(pageSize, pageNumber);
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataByProcessId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _PROCESS_MATERIAL_INFO.GetDataByProcessId(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataByProcessIdAndCategoryId(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string queryStr)
        {
            return _PROCESS_MATERIAL_INFO.GetDataByProcessIdAndCategoryId(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, MATERIAL_CATEGORY_ID, queryStr);
        }

        public List<PROCESS_MATERIAL_INFO_Entity> GetDataQuery(string PROCESS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string MATERIAL_CATEGORY_ID, string MATERIAL_TYPE_ID, string MATERIAL_TYPE_NAME, string MATERIAL_TYPE_DESC, string queryStr)
        {
            return _PROCESS_MATERIAL_INFO.GetDataQuery(PROCESS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, MATERIAL_CATEGORY_ID, MATERIAL_TYPE_ID, MATERIAL_TYPE_NAME, MATERIAL_TYPE_DESC, queryStr);
        }

        public bool GetDataValidateId(string PROCESS_ID, string MATERIAL_TYPE_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return _PROCESS_MATERIAL_INFO.GetDataValidateId(PROCESS_ID, MATERIAL_TYPE_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_MATERIAL_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PROCESS_MATERIAL_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_MATERIAL_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PROCESS_MATERIAL_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_MATERIAL_INFO_Entity entity)
        {
            return _PROCESS_MATERIAL_INFO.PostDelete(entity);
        }

        #endregion



    }
}
