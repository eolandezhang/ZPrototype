using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class CUSTOMER_CODE_LIST
    {
        DAL.BaseInfo.CUSTOMER_CODE_LIST _CUSTOMER_CODE_LIST = new DAL.BaseInfo.CUSTOMER_CODE_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<CUSTOMER_CODE_LIST_Entity> GetData()
        {
            return _CUSTOMER_CODE_LIST.GetData();
        }

        public List<CUSTOMER_CODE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _CUSTOMER_CODE_LIST.GetData(pageSize, pageNumber);
        }

        public List<CUSTOMER_CODE_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _CUSTOMER_CODE_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public bool GetDataValidateId(string CUSTOMER_CODE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _CUSTOMER_CODE_LIST.GetDataValidateId(CUSTOMER_CODE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(CUSTOMER_CODE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _CUSTOMER_CODE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(CUSTOMER_CODE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _CUSTOMER_CODE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(CUSTOMER_CODE_LIST_Entity entity)
        {
            return _CUSTOMER_CODE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
