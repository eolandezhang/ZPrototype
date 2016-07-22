using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class RECIPE_TYPE_LIST
    {
        DAL.BaseInfo.RECIPE_TYPE_LIST _RECIPE_TYPE_LIST = new DAL.BaseInfo.RECIPE_TYPE_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<RECIPE_TYPE_LIST_Entity> GetData()
        {
            return _RECIPE_TYPE_LIST.GetData();
        }

        public List<RECIPE_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _RECIPE_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataById(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _RECIPE_TYPE_LIST.GetDataById(RECIPE_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<RECIPE_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _RECIPE_TYPE_LIST.GetDataByFactoryId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public bool GetDataValidateId(string RECIPE_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _RECIPE_TYPE_LIST.GetDataValidateId(RECIPE_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(RECIPE_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _RECIPE_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(RECIPE_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _RECIPE_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(RECIPE_TYPE_LIST_Entity entity)
        {
            return _RECIPE_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
