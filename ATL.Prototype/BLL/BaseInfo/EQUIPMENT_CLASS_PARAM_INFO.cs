using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class EQUIPMENT_CLASS_PARAM_INFO
    {
        DAL.BaseInfo.EQUIPMENT_CLASS_PARAM_INFO _EQUIPMENT_CLASS_PARAM_INFO = new DAL.BaseInfo.EQUIPMENT_CLASS_PARAM_INFO();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetData()
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.GetData();
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetDataById(string EQUIPMENT_CLASS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.GetDataById(EQUIPMENT_CLASS_ID, PARAMETER_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }

        public List<EQUIPMENT_CLASS_PARAM_INFO_Entity> GetDataByClassId(string EQUIPMENT_CLASS_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.GetDataByClassId(EQUIPMENT_CLASS_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID, queryStr);
        }
         
        public bool GetDataValidateId(string EQUIPMENT_CLASS_ID, string PARAMETER_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string FACTORY_ID)
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.GetDataValidateId(EQUIPMENT_CLASS_ID, PARAMETER_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_CLASS_PARAM_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_CLASS_PARAM_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_CLASS_PARAM_INFO_Entity entity)
        {
            return _EQUIPMENT_CLASS_PARAM_INFO.PostDelete(entity);
        }

        #endregion



    }
}
