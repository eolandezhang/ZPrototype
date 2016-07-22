using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PRODUCT_MODEL_LIST
    {
        DAL.BaseInfo.PRODUCT_MODEL_LIST _PRODUCT_MODEL_LIST = new DAL.BaseInfo.PRODUCT_MODEL_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PRODUCT_MODEL_LIST_Entity> GetData()
        {
            return _PRODUCT_MODEL_LIST.GetData();
        }

        public List<PRODUCT_MODEL_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PRODUCT_MODEL_LIST.GetData(pageSize, pageNumber);
        }

        public List<PRODUCT_MODEL_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PRODUCT_MODEL_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public bool GetDataValidateId(string PRODUCT_MODEL_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PRODUCT_MODEL_LIST.GetDataValidateId(PRODUCT_MODEL_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }
        #endregion

        #region 新增

        public int PostAdd(PRODUCT_MODEL_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PRODUCT_MODEL_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PRODUCT_MODEL_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PRODUCT_MODEL_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PRODUCT_MODEL_LIST_Entity entity)
        {
            return _PRODUCT_MODEL_LIST.PostDelete(entity);
        }

        #endregion



    }
}
