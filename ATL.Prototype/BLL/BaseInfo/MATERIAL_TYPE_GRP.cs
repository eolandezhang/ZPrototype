using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class MATERIAL_TYPE_GRP
    {
        DAL.BaseInfo.MATERIAL_TYPE_GRP _MATERIAL_TYPE_GRP = new DAL.BaseInfo.MATERIAL_TYPE_GRP();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<MATERIAL_TYPE_GRP_Entity> GetData()
        {
            return _MATERIAL_TYPE_GRP.GetData();
        }

        public List<MATERIAL_TYPE_GRP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_TYPE_GRP.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_TYPE_GRP_Entity> GetDataById(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_TYPE_GRP.GetDataById(MATERIAL_TYPE_GRP_NUM, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public bool GetDataValidateId(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _MATERIAL_TYPE_GRP.GetDataValidateId(MATERIAL_TYPE_GRP_NUM, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_TYPE_GRP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_TYPE_GRP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_TYPE_GRP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_TYPE_GRP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_TYPE_GRP_Entity entity)
        {
            return _MATERIAL_TYPE_GRP.PostDelete(entity);
        }

        #endregion



    }
}
