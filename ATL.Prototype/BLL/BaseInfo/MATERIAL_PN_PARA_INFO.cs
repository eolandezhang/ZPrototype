using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class MATERIAL_PN_PARA_INFO
    {
        DAL.BaseInfo.MATERIAL_PN_PARA_INFO _MATERIAL_PN_PARA_INFO = new DAL.BaseInfo.MATERIAL_PN_PARA_INFO();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<MATERIAL_PN_PARA_INFO_Entity> GetData()
        {
            return _MATERIAL_PN_PARA_INFO.GetData();
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_PN_PARA_INFO.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetDataById(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string PARAMETER_ID, string queryStr)
        {
            return _MATERIAL_PN_PARA_INFO.GetDataById(MATERIAL_PN_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, PARAMETER_ID, queryStr);
        }

        public List<MATERIAL_PN_PARA_INFO_Entity> GetDataByPN(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID,  string queryStr)
        {
            return _MATERIAL_PN_PARA_INFO.GetDataByPN(MATERIAL_PN_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID,  queryStr);
        }

        public bool GetDataValidateId(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string PARAMETER_ID)
        {
            return _MATERIAL_PN_PARA_INFO.GetDataValidateId(MATERIAL_PN_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, PARAMETER_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_PN_PARA_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_PN_PARA_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_PN_PARA_INFO_Entity entity)
        {
            return _MATERIAL_PN_PARA_INFO.PostDelete(entity);
        }

        #endregion



    }
}
